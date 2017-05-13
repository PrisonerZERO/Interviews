//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Process.BankAccounts
{
    using Microsoft.Practices.Unity;

    /// <summary>
    /// A factory for IoC Dependency Injection
    /// </summary>
    public class DependencyInjectionSingletonFactory
    {
        #region <Fields & Constants>

        private static DependencyInjectionSingletonFactory instace;

        #endregion

        #region <Properties>

        public static DependencyInjectionSingletonFactory Instance
        {
            get
            {
                if (instace == null)
                    instace = new DependencyInjectionSingletonFactory();

                return instace;
            }
        }

        public IUnityContainer DependencyInjector { get; private set; }

        #endregion

        #region <Methods>

        private DependencyInjectionSingletonFactory()
        {
            DependencyInjector = UnityConfiguration.ConfigureUnity();
        }

        #endregion
    }
}
