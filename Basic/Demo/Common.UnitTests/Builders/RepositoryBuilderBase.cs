//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.UnitTests
{
    using System;

    /// <summary>
    /// A base for all Mock Repository Builder classes
    /// </summary>
    public class RepositoryBuilderBase
    {
        #region <Constructors>

        public RepositoryBuilderBase()
        {
            Init();
        }

        #endregion

        #region <Properties>

        #region protected

        protected string ExecutedBy { get; set; }

        protected DateTime ExecutedDate { get; set; }

        protected DateTime StartedOn { get; set; }

        protected DateTime? FinishedOn { get; set; }

        #endregion

        #endregion

        #region <Methods>

        #region private

        private void Init()
        {
            ExecutedBy = @"mock\TestyMcTesterson";
            ExecutedDate = DateTime.UtcNow;
            StartedOn = DateTime.UtcNow;
            FinishedOn = null;
        }

        #endregion

        #endregion
    }
}
