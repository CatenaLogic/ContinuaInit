ContinuaInit
============

![ContinuaInit](design/logo/logo_64.png)

ContinuaInit initializes several variables in a configuration for you. This console application will automatically integrate with Continua CI and set several variables based on the current context.

This application assumes you are using GitFlow.

## Usage

The usage is simple:

    ContinuaInitializer.exe -b [branchname] -v [version] -ci [true|false]

## Variables being set

    Note that if a variable is not found, Continua CI will ignore them

###PublishType

If the branch is *master*, the PublishType will be set to *Official*. Otherwise the PublishType will be set to *Nightly*.


###IsOfficialBuild

*true* if the branch *master*, otherwise *false* 


###IsCiBuild

*true* if the branch does not equal *master*, otherwise *false*


###DisplayVersion

Will be set to the version provided by the command line. Then it will apply one of the following values:

*nightly* => when nightly build

*ci* => when CI build 


# Icon #

Flask by Mark Caron from The Noun Project