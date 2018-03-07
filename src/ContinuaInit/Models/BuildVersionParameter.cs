// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildVersionParameter.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2018 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Models
{
    using Catel;

    public class BuildVersionParameter : IParameter
    {
        public BuildVersionParameter(string value)
        {
            Argument.IsNotNull(() => value);

            Value = value;
        }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}