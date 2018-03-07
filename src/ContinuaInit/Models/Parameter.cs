// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parameter.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Models
{
    using Catel;

    public class Parameter
    {
        public Parameter(string name, object value = null)
        {
            Argument.IsNotNull(() => name);

            Name = name;

            if (value != null)
            {
                Value = value.ToString();
            }
        }

        public string Name { get; private set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Name} => {Value}";
        }
    }
}