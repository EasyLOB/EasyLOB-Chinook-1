using EasyLOB.Application;
using EasyLOB.Data;
using EasyLOB.Library.Resources;
using EasyLOB.Library.Syncfusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyLOB.Library.Mvc
{
    [Authorize]
    public class BaseControllerSCRUDApplication<TEntityDTO, TEntity> : BaseControllerSCRUD<TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        protected override string Domain
        {
            get { return Application.UnitOfWork.Domain; }
        }

        protected override string Entity
        {
            get { return Application.Repository.Entity; }
        }

        protected IGenericApplicationDTO<TEntityDTO, TEntity> Application { get; set; }

        #endregion Properties

        #region Methods Syncfusion

        protected void ExportToExcel(string gridModel, string fileName)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (IsExport(operationResult))
                {
                    IEnumerable data = Application.SelectAll(operationResult);
                    if (operationResult.Ok)
                    {
                        SyncfusionGrid.ExportToExcel(gridModel, data, fileName, AppDefaults.SyncfusionTheme);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            if (!operationResult.Ok)
            {
                gridModel = "{\"columns\":[{\"field\":\"Message\",\"headerText\":\"" + ErrorResources.Error + "\"}]}";
                SyncfusionGrid.ExportToExcel(gridModel, operationResult.ToDataSet(), fileName, AppDefaults.SyncfusionTheme);
            }
        }

        protected void ExportToPdf(string gridModel, string fileName)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (IsExport(operationResult))
                {
                    IEnumerable data = Application.SelectAll(operationResult);
                    if (operationResult.Ok)
                    {
                        SyncfusionGrid.ExportToPdf(gridModel, data, fileName, AppDefaults.SyncfusionTheme);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            if (!operationResult.Ok)
            {
                gridModel = "{\"columns\":[{\"field\":\"Message\",\"headerText\":\"" + ErrorResources.Error + "\"}]}";
                SyncfusionGrid.ExportToPdf(gridModel, operationResult.ToDataSet(), fileName, AppDefaults.SyncfusionTheme);
            }
        }

        protected void ExportToWord(string gridModel, string fileName)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (IsExport(operationResult))
                {
                    IEnumerable data = Application.SelectAll(operationResult);
                    if (operationResult.Ok)
                    {
                        SyncfusionGrid.ExportToWord(gridModel, data, fileName, AppDefaults.SyncfusionTheme);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            if (!operationResult.Ok)
            {
                gridModel = "{\"columns\":[{\"field\":\"Message\",\"headerText\":\"" + ErrorResources.Error + "\"}]}";
                SyncfusionGrid.ExportToWord(gridModel, operationResult.ToDataSet(), fileName, AppDefaults.SyncfusionTheme);
            }
        }

        #endregion Methods Syncfusion
    }
}