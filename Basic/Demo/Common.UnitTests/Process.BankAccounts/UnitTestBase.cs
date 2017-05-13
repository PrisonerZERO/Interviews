//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.UnitTests.BankAccounts
{
    using Common.Data;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Process.BankAccounts;

    /// <summary>
    /// A base for all Unit Test classes
    /// </summary>
    public class UnitTestBase
    {
        #region <Methods>

        #region public

        [TestInitialize()]
        public virtual void Initialize()
        {
            Builder = new DemoDbBuilder(true);
            UnitOfWork = Builder.CreateMock().Object;

            // Shims all Mocks Particular to this test
            DependencyInjectionSingletonFactory.Instance.DependencyInjector.RegisterInstance(UnitOfWork);
        }

        [TestCleanup()]
        public virtual void Cleanup()
        {
            Builder = null;
            UnitOfWork = null;
        }

        #endregion

        #endregion

        #region <Properties>

        protected DemoDbBuilder Builder { get; set; }

        protected IDemoDbUnitOfWork UnitOfWork { get; set; }

        #endregion
    }
}
