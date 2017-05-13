//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Process.BankAccounts
{
    using Common.Framework;

    /// <summary>
    /// Indicates the type of account
    /// </summary>
    public enum AccountType
    {
        [StringValue("Checking Account")]
        Checking,

        [StringValue("Savings Account")]
        Savings
    }
}
