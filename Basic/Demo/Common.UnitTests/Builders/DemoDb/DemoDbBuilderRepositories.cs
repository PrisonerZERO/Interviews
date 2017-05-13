//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.UnitTests
{
    using Common.Data;
    using Moq;

    //<notes>
    //  In large projects the builder class would get rather large, which is why I break it into partials.
    //</notes>

    /// <summary>
    /// A Mock Builder class containing mock Unit of Work and Repository instances for DemoDb database
    /// </summary>
    public partial class DemoDbBuilder
    {
        #region <Repositories>

        #region dbo

        public BankAccountRepositoryBuilder BankAccountRepositoryBuilder { get; set; }

        public BankAccountTypeRepositoryBuilder BankAccountTypeRepositoryBuilder { get; set; }

        #endregion

        #endregion

        #region <Methods>

        #region public

        public Mock<IDemoDbUnitOfWork> CreateMock()
        {
            var unitOfWork = new Mock<IDemoDbUnitOfWork>();

            // DBO Tables
            var bankAccountRepository = BankAccountRepositoryBuilder.CreateMock();
            var bankAccountTypeRepository = BankAccountTypeRepositoryBuilder.CreateMock();

            unitOfWork.SetupAllProperties();

            // DBO Tables
            unitOfWork.SetupGet(x => x.BankAccounts).Returns(bankAccountRepository.Object);
            unitOfWork.SetupGet(x => x.BankAccountTypes).Returns(bankAccountTypeRepository.Object);

            return unitOfWork;
        }

        #endregion

        #region private

        private void InitializeRepositories(bool autoSeed)
        {
            // DBO Tables
            BankAccountRepositoryBuilder = new BankAccountRepositoryBuilder(autoSeed);
            BankAccountTypeRepositoryBuilder = new BankAccountTypeRepositoryBuilder(autoSeed);
        }

        #endregion

        #endregion
    }
}
