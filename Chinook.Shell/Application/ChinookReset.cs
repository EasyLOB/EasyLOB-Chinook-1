using Chinook.Application;
using Chinook.Persistence;
using EasyLOB;
using EasyLOB.Library;
using Microsoft.Practices.Unity;
using System;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void ApplicationChinookReset()
        {
            Console.WriteLine("\nApplication Chinook RESET");

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            ZOperationResult operationResult = new ZOperationResult();
            IChinookUnitOfWork unitOfWork = (IChinookUnitOfWork)container.Resolve<IChinookUnitOfWork>();
            IChinookApplication application = (IChinookApplication)container.Resolve<IChinookApplication>();
            application.Reset(operationResult, unitOfWork);

            if (!operationResult.Ok)
            {
                Console.WriteLine("\n");
                Console.WriteLine(operationResult.Text);
            }
        }
    }
}