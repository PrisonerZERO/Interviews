//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Process.BankAccounts
{
    using Common.Data;
    using Common.Diagnostics;
    using Common.Framework;
    using Common.Models.DemoDb;
    using Microsoft.Practices.Unity;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>A concrete Account Applications</summary>
    public class BankAccountApplication : IAccountApplication
    {
        #region <Fields & Constants>

        private string StartingBalanceInvalidFormat = "A Starting Balance of {0} is invalid.";
        private string AnnualPercentageRateInvalidFormat = "The Annual Percentage Rate of {0} is invalid.";

        #endregion

        #region <Properties>

        [Dependency]
        protected IDemoDbUnitOfWork UnitOfWork { get; set; }

        #endregion

        #region <Methods>

        #region public

        /// <exception cref="ArgumentNullException">Non-Existent "OwnerFullName" argument throws this exception.</exception>
        /// <exception cref="ArgumentException">Invalid "StartingBalance" argument throws this exception.</exception>
        /// <exception cref="ArgumentException">Invalid "AnnualPercentageRate" argument throws this exception.</exception>
        public BankAccount CreateNew(AccountType bankAccountType, string ownerFullName, decimal startingBalance, decimal annualPercentageRate, string executedBy)
        {
            TraceHandler.TraceIn(TraceLevel.Info);

            if (string.IsNullOrWhiteSpace(ownerFullName))
                throw new ArgumentNullException("Owner Full Name");

            if (startingBalance < 0.0M)
                throw new ArgumentException(string.Format(StartingBalanceInvalidFormat, startingBalance));

            if (annualPercentageRate <= 0.0M)
                throw new ArgumentException(string.Format(AnnualPercentageRateInvalidFormat, annualPercentageRate));

            var account = new BankAccount();

            try
            {
                BankAccountType accountType = GetAccountType(bankAccountType);

                account.AnnualPercentageRate = annualPercentageRate;
                account.Balance = startingBalance;
                account.BankAccountTypeId = accountType.BankAccountTypeId;
                account.OwnerFullName = ownerFullName;
                account.ExecutedByName = executedBy;
                account.ExecutedDatetime = DateTime.UtcNow;

                UnitOfWork.BankAccounts.Add(account);
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                TraceHandler.TraceError(ex);
            }
            finally
            {
                TraceHandler.TraceOut();
            }

            return account;
        }

        public IEnumerable<BankAccount> FindOverdrafts(IAccountAlgorithm overdraftAlgorithm)
        {
            TraceHandler.TraceIn(TraceLevel.Info);

            var accounts = new List<BankAccount>();

            try
            {
                var entities = UnitOfWork.BankAccounts.GetAll().ToList();

                entities.ForEach(e => {

                    IAlgorithmResult calculation = overdraftAlgorithm.Calculate(e.Balance);

                    if (calculation.Result)
                        accounts.Add(e);
                });
            }
            catch (Exception ex)
            {
                TraceHandler.TraceError(ex);
            }
            finally
            {
                TraceHandler.TraceOut();
            }

            return accounts.AsEnumerable();
        }

        #endregion

        #region private

        private BankAccountType GetAccountType(AccountType bankAccountType)
        {
            var name = bankAccountType.ToStringValue();

            // In this case I am going to assume all accounts are properly mapped -> First()
            return UnitOfWork.BankAccountTypes.GetAll().Where(a => a.BankAccountTypeName == name).First();
        }

        #endregion

        #endregion
    }
}
