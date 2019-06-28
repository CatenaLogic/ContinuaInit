#l "tools-variables.cake"

#addin "nuget:?package=Cake.FileHelpers&version=3.0.0"

using System.Xml.Linq;

//-------------------------------------------------------------

private string GetToolsNuGetRepositoryUrls(string projectName)
{
    // Allow per project overrides via "NuGetRepositoryUrlFor[ProjectName]"
    return GetProjectSpecificConfigurationValue(projectName, "ToolsNuGetRepositoryUrlsFor", NuGetRepositoryUrl);
}

//-------------------------------------------------------------

private string GetToolsNuGetRepositoryApiKeys(string projectName)
{
    // Allow per project overrides via "NuGetRepositoryApiKeyFor[ProjectName]"
    return GetProjectSpecificConfigurationValue(projectName, "ToolsNuGetRepositoryApiKeysFor", NuGetRepositoryApiKey);
}

//-------------------------------------------------------------

private void ValidateToolsInput()
{
    // No validation required (yet)
}

//-------------------------------------------------------------

private bool HasTools()
{
    return Tools != null && Tools.Count > 0;
}

//-------------------------------------------------------------

private async Task PrepareForToolsAsync()
{
    if (!HasTools())
    {
        return;
    }

    // Check whether projects should be processed, `.ToList()` 
    // is required to prevent issues with foreach
    foreach (var tool in Tools.ToList())
    {
        if (!ShouldProcessProject(tool))
        {
            Tools.Remove(tool);
        }
    }

    if (IsLocalBuild && Target.ToLower().Contains("packagelocal"))
    {
        foreach (var tool in Tools)
        {
            var cacheDirectory = Environment.ExpandEnvironmentVariables(string.Format("%userprofile%/.nuget/packages/{0}/{1}", tool, VersionNuGet));

            Information("Checking for existing local NuGet cached version at '{0}'", cacheDirectory);

            var retryCount = 3;

            while (retryCount > 0)
            {
                if (!DirectoryExists(cacheDirectory))
                {
                    break;
                }

                Information("Deleting already existing NuGet cached version from '{0}'", cacheDirectory);
                
                DeleteDirectory(cacheDirectory, new DeleteDirectorySettings()
                {
                    Force = true,
                    Recursive = true
                });

                await System.Threading.Tasks.Task.Delay(1000);

                retryCount--;
            }            
        }
    }
}

//-------------------------------------------------------------

private void UpdateInfoForTools()
{
    if (!HasTools())
    {
        return;
    }

    foreach (var tool in Tools)
    {
        Information("Updating version for tool '{0}'", tool);

        var projectFileName = GetProjectFileName(tool);

        TransformConfig(projectFileName, new TransformationCollection 
        {
            { "Project/PropertyGroup/PackageVersion", VersionNuGet }
        });
    }
}

//-------------------------------------------------------------

private void BuildTools()
{
    if (!HasTools())
    {
        return;
    }
    
    foreach (var tool in Tools)
    {
        LogSeparator("Building tool '{0}'", tool);

        var projectFileName = GetProjectFileName(tool);
        
        var msBuildSettings = new MSBuildSettings {
            Verbosity = Verbosity.Quiet,
            //Verbosity = Verbosity.Diagnostic,
            ToolVersion = MSBuildToolVersion.Default,
            Configuration = ConfigurationName,
            MSBuildPlatform = MSBuildPlatform.x86, // Always require x86, see platform for actual target platform
            PlatformTarget = PlatformTarget.MSIL
        };

        ConfigureMsBuild(msBuildSettings, tool);
        
        // Note: we need to set OverridableOutputPath because we need to be able to respect
        // AppendTargetFrameworkToOutputPath which isn't possible for global properties (which
        // are properties passed in using the command line)
        var outputDirectory = string.Format("{0}/{1}/", OutputRootDirectory, tool);
        Information("Output directory: '{0}'", outputDirectory);
        msBuildSettings.WithProperty("OverridableOutputPath", outputDirectory);
        msBuildSettings.WithProperty("PackageOutputPath", OutputRootDirectory);

        // SourceLink specific stuff
        var repositoryUrl = RepositoryUrl;
        if (!SourceLinkDisabled && !IsLocalBuild && !string.IsNullOrWhiteSpace(repositoryUrl))
        {       
            Information("Repository url is specified, enabling SourceLink to commit '{0}/commit/{1}'", repositoryUrl, RepositoryCommitId);

            // TODO: For now we are assuming everything is git, we might need to change that in the future
            // See why we set the values at https://github.com/dotnet/sourcelink/issues/159#issuecomment-427639278
            msBuildSettings.WithProperty("EnableSourceLink", "true");
            msBuildSettings.WithProperty("EnableSourceControlManagerQueries", "false");
            msBuildSettings.WithProperty("PublishRepositoryUrl", "true");
            msBuildSettings.WithProperty("RepositoryType", "git");
            msBuildSettings.WithProperty("RepositoryUrl", repositoryUrl);
            msBuildSettings.WithProperty("RevisionId", RepositoryCommitId);

            // For SourceLink to work, the .csproj should contain something like this:
            // <PackageReference Include="Microsoft.SourceLink.GitHub" Version="1.0.0-beta-63127-02" PrivateAssets="all" />
            var projectFileContents = System.IO.File.ReadAllText(projectFileName);
            if (!projectFileContents.Contains("Microsoft.SourceLink.GitHub"))
            {
                Warning("No SourceLink reference found, automatically injecting SourceLink package reference now");

                //const string MSBuildNS = (XNamespace) "http://schemas.microsoft.com/developer/msbuild/2003";

                var xmlDocument = XDocument.Parse(projectFileContents);
                var projectElement = xmlDocument.Root;

                // Item group with package reference
                var referencesItemGroup = new XElement("ItemGroup");
                var sourceLinkPackageReference = new XElement("PackageReference");
                sourceLinkPackageReference.Add(new XAttribute("Include", "Microsoft.SourceLink.GitHub"));
                sourceLinkPackageReference.Add(new XAttribute("Version", "1.0.0-beta-63127-02"));
                sourceLinkPackageReference.Add(new XAttribute("PrivateAssets", "all"));

                referencesItemGroup.Add(sourceLinkPackageReference);
                projectElement.Add(referencesItemGroup);

                // Item group with source root
                // <SourceRoot Include="{repository root}" RepositoryUrl="{repository url}"/>
                var sourceRootItemGroup = new XElement("ItemGroup");
                var sourceRoot = new XElement("SourceRoot");

                // Required to end with a \
                var sourceRootValue = RootDirectory;
                if (!sourceRootValue.EndsWith("\\"))
                {
                    sourceRootValue += "\\";
                };

                sourceRoot.Add(new XAttribute("Include", sourceRootValue));
                sourceRoot.Add(new XAttribute("RepositoryUrl", repositoryUrl));

                // Note: since we are not allowing source control manager queries (we don't want to require a .git directory),
                // we must specify the additional information below
                sourceRoot.Add(new XAttribute("SourceControl", "git"));
                sourceRoot.Add(new XAttribute("RevisionId", RepositoryCommitId));

                sourceRootItemGroup.Add(sourceRoot);
                projectElement.Add(sourceRootItemGroup);

                xmlDocument.Save(projectFileName);

                // Restore packages again for the dynamic package
                RestoreNuGetPackages(projectFileName);
            }
        }

        MSBuild(projectFileName, msBuildSettings);
    }
}

//-------------------------------------------------------------

private void PackageTools()
{
    if (!HasTools())
    {
        return;
    }

    foreach (var tool in Tools)
    {
        LogSeparator("Packaging tool '{0}'", tool);

        var projectDirectory = string.Format("./src/{0}", tool);
        var projectFileName = string.Format("{0}/{1}.csproj", projectDirectory, tool);
        var outputDirectory = string.Format("{0}/{1}/", OutputRootDirectory, tool);
        Information("Output directory: '{0}'", outputDirectory);

        // Step 1: remove intermediate files to ensure we have the same results on the build server, somehow NuGet 
        // targets tries to find the resource assemblies in [ProjectName]\obj\Release\net46\de\[ProjectName].resources.dll',
        // we won't run a clean on the project since it will clean out the actual output (which we still need for packaging)

        Information("Cleaning intermediate files for tool '{0}'", tool);

        var binFolderPattern = string.Format("{0}/bin/{1}/**.dll", projectDirectory, ConfigurationName);

        Information("Deleting 'bin' directory contents using '{0}'", binFolderPattern);

        var binFiles = GetFiles(binFolderPattern);
        DeleteFiles(binFiles);

        var objFolderPattern = string.Format("{0}/obj/{1}/**.dll", projectDirectory, ConfigurationName);

        Information("Deleting 'bin' directory contents using '{0}'", objFolderPattern);

        var objFiles = GetFiles(objFolderPattern);
        DeleteFiles(objFiles);

        Information(string.Empty);

        // Step 2: Go packaging!
        Information("Using 'msbuild' to package '{0}'", tool);

        var msBuildSettings = new MSBuildSettings {
            Verbosity = Verbosity.Quiet,
            //Verbosity = Verbosity.Diagnostic,
            ToolVersion = MSBuildToolVersion.Default,
            Configuration = ConfigurationName,
            MSBuildPlatform = MSBuildPlatform.x86, // Always require x86, see platform for actual target platform
            PlatformTarget = PlatformTarget.MSIL
        };

        ConfigureMsBuild(msBuildSettings, tool, "pack");

        // Note: we need to set OverridableOutputPath because we need to be able to respect
        // AppendTargetFrameworkToOutputPath which isn't possible for global properties (which
        // are properties passed in using the command line)
        msBuildSettings.WithProperty("OverridableOutputPath", outputDirectory);
        msBuildSettings.WithProperty("PackageOutputPath", OutputRootDirectory);
        msBuildSettings.WithProperty("ConfigurationName", ConfigurationName);
        msBuildSettings.WithProperty("PackageVersion", VersionNuGet);

        // SourceLink specific stuff
        var repositoryUrl = RepositoryUrl;
        if (!IsLocalBuild && !string.IsNullOrWhiteSpace(repositoryUrl))
        {       
            Information("Repository url is specified, adding commit specific data to package");

            // TODO: For now we are assuming everything is git, we might need to change that in the future
            // See why we set the values at https://github.com/dotnet/sourcelink/issues/159#issuecomment-427639278
            msBuildSettings.WithProperty("PublishRepositoryUrl", "true");
            msBuildSettings.WithProperty("RepositoryType", "git");
            msBuildSettings.WithProperty("RepositoryUrl", repositoryUrl);
            msBuildSettings.WithProperty("RevisionId", RepositoryCommitId);
        }
        
        // Fix for .NET Core 3.0, see https://github.com/dotnet/core-sdk/issues/192, it
        // uses obj/release instead of [outputdirectory]
        msBuildSettings.WithProperty("DotNetPackIntermediateOutputPath", outputDirectory);
        
        msBuildSettings.WithProperty("NoBuild", "true");
        msBuildSettings.Targets.Add("Pack");

        MSBuild(projectFileName, msBuildSettings);

        LogSeparator();
    }

    var codeSign = (!IsCiBuild && !IsLocalBuild && !string.IsNullOrWhiteSpace(CodeSignCertificateSubjectName));
    if (codeSign)
    {
        // For details, see https://docs.microsoft.com/en-us/nuget/create-packages/sign-a-package
        // nuget sign MyPackage.nupkg -CertificateSubjectName <MyCertSubjectName> -Timestamper <TimestampServiceURL>
        var filesToSign = GetFiles(string.Format("{0}/*.nupkg", OutputRootDirectory));

        foreach (var fileToSign in filesToSign)
        {
            Information("Signing NuGet package '{0}' using certificate subject '{1}'", fileToSign, CodeSignCertificateSubjectName);

            var exitCode = StartProcess(NuGetExe, new ProcessSettings
            {
                Arguments = string.Format("sign \"{0}\" -CertificateSubjectName \"{1}\" -Timestamper \"{2}\"", fileToSign, CodeSignCertificateSubjectName, CodeSignTimeStampUri)
            });

            Information("Signing NuGet package exited with '{0}'", exitCode);
        }
    }
}

//-------------------------------------------------------------

private void DeployTools()
{
    if (!HasTools())
    {
        return;
    }

    foreach (var tool in Tools)
    {
        if (!ShouldDeployProject(tool))
        {
            Information("Tool '{0}' should not be deployed", tool);
            continue;
        }

        LogSeparator("Deploying tool '{0}'", tool);

        var packageToPush = string.Format("{0}/{1}.{2}.nupkg", OutputRootDirectory, tool, VersionNuGet);
        var nuGetRepositoryUrls = GetToolNuGetRepositoryUrls(tool);
        var nuGetRepositoryApiKeys = GetToolNuGetRepositoryApiKeys(tool);

        var nuGetServers = GetNuGetServers(nuGetRepositoryUrls, nuGetRepositoryApiKeys);
        if (nuGetServers.Count == 0)
        {
            throw new Exception("No NuGet repositories specified, as a protection mechanism this must *always* be specified to make sure packages aren't accidentally deployed to the default public NuGet feed");
        }

        foreach (var nuGetServer in nuGetServers)
        {
            Information("Pushing to '{0}'", nuGetServer);

            NuGetPush(packageToPush, new NuGetPushSettings
            {
                Source = nuGetRepositoryUrl,
                ApiKey = nuGetRepositoryApiKey
            });
        }
    }
}

//-------------------------------------------------------------

Task("UpdateInfoForTools")
    .IsDependentOn("Clean")
    .Does(() =>
{
    UpdateSolutionAssemblyInfo();
    UpdateInfoForTools();
});

//-------------------------------------------------------------

Task("BuildTools")
    .IsDependentOn("UpdateInfoForTools")
    .Does(() =>
{
    BuildTools();
});

//-------------------------------------------------------------

Task("PackageTools")
    .IsDependentOn("BuildTools")
    .Does(() =>
{
    PackageTools();
});

//-------------------------------------------------------------

Task("DeployTools")
    .IsDependentOn("PackageTools")
    .Does(() =>
{
    DeployTools();
});