//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.UnitTests.BankAccounts
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Process.BankAccounts;
    using System.Linq;

    /// <summary>
    /// A concrete Unit Test class
    /// </summary>
    [TestClass]
    public class IsOverdraftAlgorithmUnitTests : UnitTestBase
    {
        #region <Methods>

        #region public

        [TestInitialize()]
        public override void Initialize()
        {
            Builder = new DemoDbBuilder(true);
            UnitOfWork = Builder.CreateMock().Object;
        }

        #endregion

        #endregion

        #region <Tests>

        [TestMethod]
        public void IsOverdraftAlgorithm_Detects_NoOverdrafts()
        {
            int expectedCount = 0;
            int actualCount = 0;

            // -----
            // ARRANGE
            Builder.LoadDataScenario(UnitOfWork, DataScenarios.Default);

            var algorithm = new IsOverdraftAlgorithm();
            var entities = UnitOfWork.BankAccounts.GetAll().ToList();

            // -----
            // ACT
            entities.ForEach(e => {

                IAlgorithmResult calculation = algorithm.Calculate(e.Balance);

                if (calculation.Result)
                    actualCount ++;
            });

            // -----
            // ASSERT
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void IsOverdraftAlgorithm_Detects_Overdrafts()
        {
            int expectedCount = 1;
            int actualCount = 0;

            // -----
            // ARRANGE
            Builder.LoadDataScenario(UnitOfWork, DataScenarios.Default);
            Builder.LoadDataScenario(UnitOfWork, DataScenarios.Overdrafts);

            var algorithm = new IsOverdraftAlgorithm();
            var entities = UnitOfWork.BankAccounts.GetAll().ToList();

            // -----
            // ACT
            entities.ForEach(e => {

                IAlgorithmResult calculation = algorithm.Calculate(e.Balance);

                if (calculation.Result)
                    actualCount++;
            });

            // -----
            // ASSERT
            Assert.AreEqual(expectedCount, actualCount);
        }

        #endregion
    }
}
