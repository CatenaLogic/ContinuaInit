// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContinuaInitException.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit
{
    using System;

    public class ContinuaInitException : Exception
    {
        public ContinuaInitException(string message)
            : base(message)
        {
        }
    }
}