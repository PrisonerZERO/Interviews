//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.UnitTests
{
    using Common.Data;
    using Moq;
    using System.Collections.Generic;
    using System.Data.Entity;

    /// <summary>
    /// An interface for Mock Repository Builder classes
    /// </summary>
    public interface IRepositoryBuilder<TEntity> where TEntity : class
    {
        #region <Properties>

        List<TEntity> Entities { get; }

        EntityState EntityState { get; }

        #endregion

        #region <Methods>

        Mock<IRepository<TEntity>> CreateMock();

        #endregion
    }
}
