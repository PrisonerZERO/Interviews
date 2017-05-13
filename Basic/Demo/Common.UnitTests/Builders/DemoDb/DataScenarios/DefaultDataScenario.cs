//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.UnitTests
{
    using Common.Data;
    using Common.Models.DemoDb;
    using Process.BankAccounts;

    //<notes>
    //  In large projects creating test data got cumbersome, so I often create data-scenarios to help.  I find it also "cleans up" the code quite a bit.
    //</notes>

    /// <summary>
    /// A concrete Data Scenario class
    /// </summary>
    public class DefaultDataScenario : DataScenarioBase
    {
        #region <Methods>

        #region public

        public void Load(IDemoDbUnitOfWork unitOfWork)
        {
            BankAccount record = CreateNew(unitOfWork, AccountType.Checking, "Robert M. Jordan", 50.00M, 2.6M);

            unitOfWork.BankAccounts.Add(record);
        }

        #endregion

        #endregion
    }
}
