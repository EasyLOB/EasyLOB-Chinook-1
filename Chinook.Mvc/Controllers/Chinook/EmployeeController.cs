using Newtonsoft.Json;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Chinook.Application;
using Chinook.Data;
using Chinook.Data.Resources;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Library.Mvc;
using EasyLOB.Library.Syncfusion;

namespace Chinook.Mvc
{
    public class EmployeeController : BaseControllerSCRUDApplication<EmployeeDTO, Employee>
    {
        #region Methods

        public EmployeeController(IChinookGenericApplicationDTO<EmployeeDTO, Employee> application)
        {
            Application = application;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: Employee
        // GET: Employee/Index
        [HttpGet]
        public ActionResult Index(int? masterReportsTo = null)
        {
            ClearUrlDictionary();

            EmployeeCollectionModel employeeCollectionModel = new EmployeeCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index",
                MasterReportsTo = masterReportsTo
            };

            try
            {
                IsSearch(employeeCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                employeeCollectionModel.OperationResult.ParseException(exception);
            }

            return View(employeeCollectionModel);
        }        

        // GET & POST: Employee/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, int? masterReportsTo = null)
        {
            WriteUrlDictionary();

            EmployeeCollectionModel employeeCollectionModel = new EmployeeCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction,
                MasterReportsTo = masterReportsTo
            };

            try
            {
                IsSearch(employeeCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                employeeCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_EmployeeCollection", employeeCollectionModel);
        }

        // GET & POST: Employee/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null)
        {
            LookupModel lookupModel = new LookupModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Text = text,
                ValueId = valueId,
                Elements = elements
            };

            try
            {
                IsSearch(lookupModel.OperationResult);
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return PartialView("_EmployeeLookup", lookupModel);
        }

        // GET: Employee/Create
        [HttpGet]
        public ActionResult Create(int? masterReportsTo = null)
        {
            EmployeeItemModel employeeItemModel = new EmployeeItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Employee = new EmployeeViewModel(),
                ControllerAction = "Create",
                MasterReportsTo = masterReportsTo
            };

            try
            {
                IsCreate(employeeItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            return View(employeeItemModel);
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeItemModel employeeItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Create(employeeItemModel.OperationResult, (EmployeeDTO)employeeItemModel.Employee.ToDTO()))
                    {
                        return RedirectToUrlDictionary();
                    }
                }
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            employeeItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(employeeItemModel);
        }

        // GET: Employee/Read/1
        [HttpGet]
        public ActionResult Read(int? employeeId, int? masterReportsTo = null)
        {
            EmployeeItemModel employeeItemModel = new EmployeeItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Employee = new EmployeeViewModel(),
                ControllerAction = "Read",
                MasterReportsTo = masterReportsTo
            };
            
            try
            {
                EmployeeDTO employeeDTO = Application.GetById(employeeItemModel.OperationResult, new object[] { employeeId });
                if (employeeDTO != null)
                {
                    employeeItemModel.Employee = new EmployeeViewModel(employeeDTO);
                }
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            return View(employeeItemModel);
        }

        // GET: Employee/Update/1
        [HttpGet]
        public ActionResult Update(int? employeeId, int? masterReportsTo = null)
        {
            EmployeeItemModel employeeItemModel = new EmployeeItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Employee = new EmployeeViewModel(),
                ControllerAction = "Update",
                MasterReportsTo = masterReportsTo
            };
            
            try
            {
                EmployeeDTO employeeDTO = Application.GetById(employeeItemModel.OperationResult, new object[] { employeeId });
                if (employeeDTO != null)
                {
                    employeeItemModel.Employee = new EmployeeViewModel(employeeDTO);
                }
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            return View(employeeItemModel);
        }

        // POST: Employee/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EmployeeItemModel employeeItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Update(employeeItemModel.OperationResult, (EmployeeDTO)employeeItemModel.Employee.ToDTO()))
                    {
                        return RedirectToUrlDictionary();
                    }
                }
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            employeeItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(employeeItemModel);
        }

        // GET: Employee/Delete/1
        [HttpGet]
        public ActionResult Delete(int? employeeId, int? masterReportsTo = null)
        {
            EmployeeItemModel employeeItemModel = new EmployeeItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Employee = new EmployeeViewModel(),
                ControllerAction = "Delete",
                MasterReportsTo = masterReportsTo
            };
            
            try
            {
                EmployeeDTO employeeDTO = Application.GetById(employeeItemModel.OperationResult, new object[] { employeeId });
                if (employeeDTO != null)
                {
                    employeeItemModel.Employee = new EmployeeViewModel(employeeDTO);
                }
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            return View(employeeItemModel);
        }

        // POST: Employee/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EmployeeItemModel employeeItemModel)
        {
            try
            {
                if (Application.Delete(employeeItemModel.OperationResult, (EmployeeDTO)employeeItemModel.Employee.ToDTO()))
                {
                    return RedirectToUrlDictionary();
                }
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            employeeItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(employeeItemModel);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Employee/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<EmployeeViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Employee), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<EmployeeViewModel, EmployeeDTO, Employee>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
                    if (dataManager.RequiresCounts)
                    {
                        countAll = Application.Count(where, args.ToArray());
                    }
                }
                catch (Exception exception)
                {
                    operationResult.ParseException(exception);
                }

                if (!operationResult.Ok)
                {
                    throw new InvalidOperationException(operationResult.Text);
                }
            }

            return Json(JsonConvert.SerializeObject(new { result = data, count = countAll }), JsonRequestBehavior.AllowGet);
        } 

        // POST: Employee/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, EmployeeResources.EntitySingular + ".xlsx");
        }

        // POST: Employee/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, EmployeeResources.EntitySingular + ".pdf");
        }

        // POST: Employee/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, EmployeeResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}