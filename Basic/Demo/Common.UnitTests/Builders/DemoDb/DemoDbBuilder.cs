//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.UnitTests
{
    using Process.BankAccounts;
    using Common.Data;
    using Microsoft.Practices.Unity;
    using System;

    //<notes>
    //  In large projects the builder class would get rather large, which is why I break it into partials.
    //</notes>

    /// <summary>
    /// A Mock Builder class containing mock Unit of Work and Repository instances for DemoDb database
    /// </summary>
    public partial class DemoDbBuilder
    {
        #region <Constructors>

        public DemoDbBuilder()
        {
            InitializeRepositories(true);
        }

        public DemoDbBuilder(bool autoSeed)
        {
            InitializeRepositories(autoSeed);
        }

        #endregion

        #region <Methods>

        #region public

        public T CreateInstance<T>(IDemoDbUnitOfWork unitOfWork, bool byFullName = false)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("UnitOfWork");

            // FORCE: Same instance to be shared across all classes
            DependencyInjectionSingletonFactory.Instance.DependencyInjector.RegisterInstance(unitOfWork);

            var genericType = typeof(T);
            var name = (!byFullName) ? genericType.Name : genericType.FullName;

            return DependencyInjectionSingletonFactory.Instance.DependencyInjector.Resolve<T>(name);
        }

        #endregion

        #endregion
    }
}
