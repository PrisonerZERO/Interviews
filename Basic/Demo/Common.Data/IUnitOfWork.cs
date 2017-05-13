//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Data
{
    /// <summary>
    /// An interface for Unit Of Work
    /// </summary>
    public interface IUnitOfWork
    {
        #region <Properties>

        IDbContext DbContext { get; set; }

        #endregion

        #region <Methods>

        void SaveChanges();

        void RefreshAll();

        #endregion
    }
}
