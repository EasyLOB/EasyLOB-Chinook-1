using System;
using Chinook.Persistence.Default;
using EasyLOB.Persistence;
using EasyLOB.Library;

namespace Chinook.Persistence
{
    public class ChinookUnitOfWorkOData : UnitOfWorkOData, IChinookUnitOfWorkDTO
    {
        #region Methods

        public ChinookUnitOfWorkOData()
            : base(new Container(new Uri(LibraryHelper.AppSettings<string>("OData.Chinook"))))
        {
            Domain = "Chinook";

            //DataServiceContext container = (DataServiceContext)base.Container;
        }

        public override IGenericRepositoryDTO<TEntityDTO, TEntity> GetRepository<TEntityDTO, TEntity>()
        {
            if (!Repositories.Keys.Contains(typeof(TEntityDTO)))
            {
                var repository = new ChinookGenericRepositoryOData<TEntityDTO, TEntity>(this);
                Repositories.Add(typeof(TEntityDTO), repository);
            }

            return Repositories[typeof(TEntityDTO)] as IGenericRepositoryDTO<TEntityDTO, TEntity>;
        }

        #endregion Methods
    }
}