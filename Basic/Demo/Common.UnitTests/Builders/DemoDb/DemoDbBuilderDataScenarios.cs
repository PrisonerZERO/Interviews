//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.UnitTests
{
    using Common.Data;
    using System;

    //<notes>
    //  In large projects the builder class would get rather large, which is why I break it into partials.
    //</notes>

    /// <summary>
    /// A Mock Builder class containing mock Unit of Work and Repository instances for the DemoDb database
    /// </summary>
    public partial class DemoDbBuilder
    {
        #region <Fields & Constants>

        private DefaultDataScenario _defaultDataScenario = new DefaultDataScenario();
        private OverdraftsDataScenario _overdraftsDataScenario = new OverdraftsDataScenario();

        #endregion

        #region <Properties>

        protected DefaultDataScenario DefaultDataScenario { get { return _defaultDataScenario; } }
        protected OverdraftsDataScenario OverdraftsDataScenario { get { return _overdraftsDataScenario; } }

        #endregion

        #region <Methods>

        #region public

        public void LoadDataScenario(IDemoDbUnitOfWork unitOfWork, DataScenarios dataScenario)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("UnitOfWork");

            if (dataScenario == DataScenarios.Default)
                DefaultDataScenario.Load(unitOfWork);

            if (dataScenario == DataScenarios.Overdrafts)
                OverdraftsDataScenario.Load(unitOfWork);
        }

        #endregion

        #endregion
    }
}
