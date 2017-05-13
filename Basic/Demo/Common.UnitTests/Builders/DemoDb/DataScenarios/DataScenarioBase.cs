//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.UnitTests
{
    using Common.Data;
    using Common.Framework;
    using Common.Models.DemoDb;
    using Process.BankAccounts;
    using System.Linq;

    ///<notes>
    ///  In large projects creating test data got cumbersome, so I often create data-scenarios to help.  I find it also "cleans up" the code quite a bit.
    ///</notes>

    /// <summary>
    /// A concrete-base for Data Scenario classes
    /// </summary>
    public class DataScenarioBase
    {
        #region <Methods>

        #region protected

        protected BankAccount CreateNew(IDemoDbUnitOfWork unitOfWork, AccountType bankAccountType, string ownerFullName, decimal startingBalance, decimal annualPercentageRate)
        {
            var entity = new BankAccount();

            entity.AnnualPercentageRate = annualPercentageRate;
            entity.Balance = startingBalance;
            entity.BankAccountTypeId = GetAccountType(unitOfWork, bankAccountType).BankAccountTypeId;
            entity.OwnerFullName = ownerFullName;

            return entity;
        }

        protected BankAccountType GetAccountType(IDemoDbUnitOfWork unitOfWork, AccountType bankAccountType)
        {
            var name = bankAccountType.ToStringValue();

            // In this case I am going to assume all accounts are properly mapped -> First()
            return unitOfWork.BankAccountTypes.GetAll().Where(a => a.BankAccountTypeName == name).First();
        }

        #endregion

        #endregion
    }
}
