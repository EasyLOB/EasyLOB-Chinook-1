using Chinook.Mvc.Resources.Tasks;
using EasyLOB.Library;
using System;
using System.ComponentModel.DataAnnotations;

namespace Chinook.Mvc
{
    public class TaskSimpleModel
    {
        public ZOperationResult OperationResult { get; set; }

        [Display(Name = "PropertyXBoolean", ResourceType = typeof(TaskSimpleResources))]
        public bool XBoolean { get; set; }

        public DateTime? XDateTime { get; set; }

        [Display(Name = "PropertyXDouble", ResourceType = typeof(TaskSimpleResources))]
        [Required]
        public double XDouble { get; set; }

        public int? XInteger { get; set; }

        public string XString { get; set; }

        public TaskSimpleModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}