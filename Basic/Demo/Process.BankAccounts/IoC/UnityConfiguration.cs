//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Process.BankAccounts
{
    using Common.Data;
    using Microsoft.Practices.Unity;
    using System;
    using System.Linq;

    /// <summary>
    /// A factory for IoC Dependency Injection Configuration definitions
    /// </summary>
    public static class UnityConfiguration
    {
        #region <Methods>

        #region public

        public static UnityContainer ConfigureUnity()
        {
            // MAPPING NOTES:
            //  - InjectionConstructor      = Aids in choosing the correct constructor (version)
            //  - PerThreadLifetimeManager  = Holds the instance for the lifetinme of the thread (i.e. singleton-style)
            //  - "NamedTypes"              = Allows you to map a specific type to a common base

            var unityContainer = new UnityContainer();

            // --------
            // UNIT OF WORK (Order Matters Here) 
            // --------
            unityContainer.RegisterType<IDbContext, DemoDbContext>(new PerThreadLifetimeManager(), new InjectionConstructor());
            unityContainer.RegisterType<IDbContext, DemoDbContext>(typeof(DemoDbContext).Name, new PerThreadLifetimeManager(), new InjectionConstructor());
            unityContainer.RegisterType<IDemoDbUnitOfWork, DemoDbUnitOfWork>();

            // --------
            // REPOSITORIES (By Default) 
            // --------
            unityContainer.RegisterType(typeof(IRepository<>), typeof(GenericRepository<>), new InjectionConstructor(new ResolvedParameter<IDbContext>()));

            // --------
            // APPLICATIONS
            // --------
            RegisterDynamically(typeof(IAccountApplication), unityContainer);

            // --------
            // STRATEGIES
            // --------
            RegisterDynamically(typeof(IAccountAlgorithm), unityContainer);

            return unityContainer;
        }

        #endregion

        #region private

        private static void RegisterDynamically(Type typeOfInterface, UnityContainer unityContainer)
        {
            var typesOfClasses = AppDomain.CurrentDomain.GetAssemblies()
                 .SelectMany(s => s.GetTypes())
                 .Where(p => p.GetInterfaces().Contains(typeOfInterface));

            foreach (var typeClass in typesOfClasses)
            {
                unityContainer.RegisterType(typeOfInterface, typeClass, typeClass.Name);
            }
        }

        #endregion

        #endregion
    }
}
