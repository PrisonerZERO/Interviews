//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.UnitTests.BankAccounts
{
    using Common.Framework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Process.BankAccounts;
    using System;
    using System.Linq;

    /// <summary>
    /// A concrete Unit Test class
    /// </summary>
    [TestClass]
    public class BankAccountApplicationUnitTests : UnitTestBase
    {
        #region <Tests>

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BankAccountApplication_CreateNew_Invalid_OwnerFullName()
        {
            // -----
            // ARRANGE
            var application = Builder.CreateInstance<BankAccountApplication>(UnitOfWork);

            // -----
            // ACT
            var account = application.CreateNew(AccountType.Checking, null, 10000000.00M, 3.00M, Builder.ExecutedBy);

            // -----
            // ASSERT - See attribute
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BankAccountApplication_CreateNew_Invalid_StartingBalance()
        {
            // -----
            // ARRANGE
            var application = Builder.CreateInstance<BankAccountApplication>(UnitOfWork);

            // -----
            // ACT
            var account = application.CreateNew(AccountType.Checking, "Scrooge McDuck", -100.00M, 3.00M, Builder.ExecutedBy);

            // -----
            // ASSERT - See attribute
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BankAccountApplication_CreateNew_Invalid_AnnualPercentageRate()
        {
            // -----
            // ARRANGE
            var application = Builder.CreateInstance<BankAccountApplication>(UnitOfWork);

            // -----
            // ACT
            var account = application.CreateNew(AccountType.Checking, "Scrooge McDuck", 10000000.00M, -3.00M, Builder.ExecutedBy);

            // -----
            // ASSERT - See attribute
        }

        [TestMethod]
        public void BankAccountApplication_CreatesNew_CheckingAccount()
        {
            AccountType expectedAccountType = AccountType.Checking;
            AccountType actualAccountType = AccountType.Savings; //<-- Set as an Invalid Type

            int expectedCount = 0;
            int actualCount = 0;

            // -----
            // ARRANGE
            Builder.LoadDataScenario(UnitOfWork, DataScenarios.Default);

            expectedCount = UnitOfWork.BankAccounts.GetAll().Count();
            expectedCount++;

            // -----
            // ACT
            var application = Builder.CreateInstance<BankAccountApplication>(UnitOfWork);
            var account = application.CreateNew(AccountType.Checking, "Scrooge McDuck", 10000000.00M, 3.00M, Builder.ExecutedBy);
            var accountType = UnitOfWork.BankAccountTypes.GetAll().Where(x => x.BankAccountTypeName == AccountType.Checking.ToStringValue()).SingleOrDefault();

            actualCount = UnitOfWork.BankAccounts.GetAll().Count();
            actualAccountType = (AccountType)accountType.BankAccountTypeName.ToEnumValue(typeof(AccountType));

            // -----
            // ASSERT
            Assert.AreEqual(expectedCount, actualCount);
            Assert.IsNotNull(accountType);
            Assert.AreEqual(expectedAccountType, actualAccountType);
        }

        [TestMethod]
        public void BankAccountApplication_CreatesNew_SavingsAccount()
        {
            AccountType expectedAccountType = AccountType.Savings;
            AccountType actualAccountType = AccountType.Checking; //<-- Set as an Invalid Type

            int expectedCount = 0;
            int actualCount = 0;

            // -----
            // ARRANGE
            Builder.LoadDataScenario(UnitOfWork, DataScenarios.Default);

            expectedCount = UnitOfWork.BankAccounts.GetAll().Count();
            expectedCount++;

            // -----
            // ACT
            var application = Builder.CreateInstance<BankAccountApplication>(UnitOfWork);
            var account = application.CreateNew(AccountType.Savings, "Scrooge McDuck", 10000000.00M, 3.00M, Builder.ExecutedBy);
            var accountType = UnitOfWork.BankAccountTypes.GetAll().Where(x => x.BankAccountTypeName == AccountType.Savings.ToStringValue()).SingleOrDefault();

            actualCount = UnitOfWork.BankAccounts.GetAll().Count();
            actualAccountType = (AccountType)accountType.BankAccountTypeName.ToEnumValue(typeof(AccountType));

            // -----
            // ASSERT
            Assert.AreEqual(expectedCount, actualCount);
            Assert.IsNotNull(accountType);
            Assert.AreEqual(expectedAccountType, actualAccountType);
        }

        [TestMethod]
        public void BankAccountApplication_Detects_NoOverdrafts()
        {
            int expectedCount = 0;
            int actualCount = 0;

            // -----
            // ARRANGE
            Builder.LoadDataScenario(UnitOfWork, DataScenarios.Default);

            var algorithm = new IsOverdraftAlgorithm();
            var application = Builder.CreateInstance<BankAccountApplication>(UnitOfWork);

            // -----
            // ACT
            var entities = application.FindOverdrafts(algorithm);

            actualCount = entities.Count();

            // -----
            // ASSERT
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void BankAccountApplication_Detects_Overdrafts()
        {
            int expectedCount = 1;
            int actualCount = 0;

            // -----
            // ARRANGE
            Builder.LoadDataScenario(UnitOfWork, DataScenarios.Default);
            Builder.LoadDataScenario(UnitOfWork, DataScenarios.Overdrafts);

            var algorithm = new IsOverdraftAlgorithm();
            var application = Builder.CreateInstance<BankAccountApplication>(UnitOfWork);

            // -----
            // ACT
            var entities = application.FindOverdrafts(algorithm);

            actualCount = entities.Count();

            // -----
            // ASSERT
            Assert.AreEqual(expectedCount, actualCount);
        }

        #endregion
    }
}
