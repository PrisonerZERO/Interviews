//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.UnitTests
{
    using Common.Data;
    using Common.Models.DemoDb;
    using Moq;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// A concrete Mock Repository Builder class
    /// </summary>
    public class BankAccountTypeRepositoryBuilder
    {
        #region <Constructor>

        public BankAccountTypeRepositoryBuilder()
        {
            Initialize(true);
        }

        public BankAccountTypeRepositoryBuilder(bool autoSeed)
        {
            Initialize(autoSeed);
        }

        #endregion

        #region <Properties>

        public List<BankAccountType> Entities { get; private set; }

        public EntityState EntityState { get; private set; }

        #endregion

        #region <Methods>

        public Mock<IRepository<BankAccountType>> CreateMock()
        {
            var repository = new Mock<IRepository<BankAccountType>>();

            repository.SetupAllProperties();

            repository.Setup(x => x.GetActive()).Returns(this.Entities.AsQueryable());
            repository.Setup(x => x.GetAll()).Returns(this.Entities.AsQueryable());
            repository.Setup(x => x.GetById(It.IsAny<object>())).Returns((object id) => { return this.Entities.Where(e => e.BankAccountTypeId == (int)id).FirstOrDefault(); });
            repository.Setup(x => x.Find(new object[] { It.IsAny<string>() })).Returns((object id) => { return this.Entities.Where(e => e.BankAccountTypeId == (int)id).FirstOrDefault(); });
            repository.Setup(x => x.Add(It.IsAny<BankAccountType>())).Callback<BankAccountType>(x => { this.Entities.Add(x); }).Returns((BankAccountType entity) => { return Entities.First(y => y.BankAccountTypeId == entity.BankAccountTypeId); });
            repository.Setup(x => x.AddRange(It.IsAny<IEnumerable<BankAccountType>>())).Callback<IEnumerable<BankAccountType>>(x => { this.Entities.AddRange(x); }).Verifiable();
            repository.Setup(x => x.Update(It.IsAny<BankAccountType>())).Callback<BankAccountType>(x => { UpdateEntity(x); }).Verifiable();
            repository.Setup(x => x.Delete(It.IsAny<BankAccountType>())).Callback<BankAccountType>(x => { DeleteByEntity(x); }).Verifiable();
            repository.Setup(x => x.Delete(It.IsAny<object>())).Callback<object>(x => { DeleteById(x); }).Verifiable();
            repository.Setup(x => x.ApplyState(It.IsAny<BankAccountType>(), It.IsAny<EntityState>())).Callback<BankAccountType, EntityState>((x, y) => { this.EntityState = y; }).Verifiable();
            repository.Setup(x => x.GetState(It.IsAny<BankAccountType>())).Returns((BankAccountType entity) => { return this.EntityState; });

            return repository;
        }

        #region private

        private void Initialize(bool autoSeed)
        {
            Entities = AutoSeed(autoSeed);
            EntityState = EntityState.Unchanged;
        }

        private void DeleteById(object id)
        {
            var entity = this.Entities.FirstOrDefault(x => x.BankAccountTypeId == (int)id);
            if (entity != null)
                this.Entities.RemoveAt(Entities.IndexOf(entity));
        }

        private void DeleteByEntity(BankAccountType deletedEntity)
        {
            var entity = this.Entities.FirstOrDefault(x => x.BankAccountTypeId == deletedEntity.BankAccountTypeId);
            if (entity != null)
                this.Entities.Remove(entity);
        }

        private void UpdateEntity(BankAccountType updatedEntity)
        {
            var entity = this.Entities.FirstOrDefault(x => x.BankAccountTypeId == updatedEntity.BankAccountTypeId);
            if (entity != null)
                entity = updatedEntity;
        }

        private List<BankAccountType> AutoSeed(bool autoSeed)
        {
            if (!autoSeed)
                return new List<BankAccountType>();

            var database = new List<BankAccountType>();

            database.Add(new BankAccountType()
            {
                BankAccountTypeId = 1,
                BankAccountTypeName = "Checking Account"
            });

            database.Add(new BankAccountType()
            {
                BankAccountTypeId = 2,
                BankAccountTypeName = "Savings Account"
            });

            return database;
        }

        #endregion

        #endregion
    }
}
