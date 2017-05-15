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
    public class BankAccountRepositoryBuilder
    {
        #region <Constructor>

        public BankAccountRepositoryBuilder()
        {
            Initialize(true);
        }

        public BankAccountRepositoryBuilder(bool autoSeed)
        {
            Initialize(autoSeed);
        }

        #endregion

        #region <Properties>

        public List<BankAccount> Entities { get; private set; }

        public EntityState EntityState { get; private set; }

        #endregion

        #region <Methods>

        public Mock<IRepository<BankAccount>> CreateMock()
        {
            var repository = new Mock<IRepository<BankAccount>>();

            repository.SetupAllProperties();

            repository.Setup(x => x.GetActive()).Returns(this.Entities.AsQueryable());
            repository.Setup(x => x.GetAll()).Returns(this.Entities.AsQueryable());
            repository.Setup(x => x.GetById(It.IsAny<object>())).Returns((object id) => { return this.Entities.Where(e => e.BankAccountId == (int)id).FirstOrDefault(); });
            repository.Setup(x => x.Find(new object[] { It.IsAny<string>() })).Returns((object id) => { return this.Entities.Where(e => e.BankAccountId == (int)id).FirstOrDefault(); });
            repository.Setup(x => x.Add(It.IsAny<BankAccount>())).Callback<BankAccount>(x => { this.Entities.Add(x); }).Returns((BankAccount entity) => { return Entities.First(y => y.BankAccountId == entity.BankAccountId); });
            repository.Setup(x => x.AddRange(It.IsAny<IEnumerable<BankAccount>>())).Callback<IEnumerable<BankAccount>>(x => { this.Entities.AddRange(x); }).Verifiable();
            repository.Setup(x => x.Update(It.IsAny<BankAccount>())).Callback<BankAccount>(x => { UpdateEntity(x); }).Verifiable();
            repository.Setup(x => x.Delete(It.IsAny<BankAccount>())).Callback<BankAccount>(x => { DeleteByEntity(x); }).Verifiable();
            repository.Setup(x => x.Delete(It.IsAny<object>())).Callback<object>(x => { DeleteById(x); }).Verifiable();
            repository.Setup(x => x.ApplyState(It.IsAny<BankAccount>(), It.IsAny<EntityState>())).Callback<BankAccount, EntityState>((x, y) => { this.EntityState = y; }).Verifiable();
            repository.Setup(x => x.GetState(It.IsAny<BankAccount>())).Returns((BankAccount entity) => { return this.EntityState; });

            return repository;
        }

        #region private

        private void Initialize(bool autoSeed)
        {
            Entities = AutoSeed(autoSeed);
            EntityState = EntityState.Unchanged;
        }

        private BankAccount AddRecord(BankAccount assistanceHistoryEntity)
        {
            assistanceHistoryEntity.BankAccountId = 1001;
            this.Entities.Add(assistanceHistoryEntity);

            return assistanceHistoryEntity;
        }

        private void DeleteById(object id)
        {
            var entity = this.Entities.FirstOrDefault(x => x.BankAccountId == (int)id);
            if (entity != null)
                this.Entities.RemoveAt(Entities.IndexOf(entity));
        }

        private void DeleteByEntity(BankAccount deletedEntity)
        {
            var entity = this.Entities.FirstOrDefault(x => x.BankAccountId == deletedEntity.BankAccountId);
            if (entity != null)
                this.Entities.Remove(entity);
        }

        private void UpdateEntity(BankAccount updatedEntity)
        {
            var entity = this.Entities.FirstOrDefault(x => x.BankAccountId == updatedEntity.BankAccountId);
            if (entity != null)
                entity = updatedEntity;
        }

        private List<BankAccount> AutoSeed(bool autoSeed)
        {
            if (!autoSeed)
                return new List<BankAccount>();

            var database = new List<BankAccount>();



            return database;
        }

        #endregion

        #endregion
    }
}
