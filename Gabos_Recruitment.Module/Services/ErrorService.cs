using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gabos_Recruitment.Module.Services
{
    public static class ErrorService
    {
        public static void ShowError(Controller controller, string code, string message)
        {
            controller.Application.ShowViewStrategy.ShowMessage($"{code} - {message}", InformationType.Error, 4000);
        }
    }
}
