//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Process.BankAccounts
{
    using Common.Models.DemoDb;
    using System.Collections.Generic;

    /// <summary>An interface for Account Applications</summary>
    public interface IAccountApplication
    {
        #region <Methods>

        BankAccount CreateNew(AccountType bankAccountType, string ownerFullName, decimal startingBalance, decimal annualPercentageRate, string executedBy);

        IEnumerable<BankAccount> FindOverdrafts(IAccountAlgorithm algorithm);

        #endregion
    }
}
