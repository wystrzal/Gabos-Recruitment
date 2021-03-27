using System;
using System.Collections.Generic;
using System.Text;

namespace Gabos_Recruitment.Module.Services.ApiUrlAssigners
{
    public static class ApiStorage
    {
        public static string TestApiUrl { get; private set; } = "https://wl-test.mf.gov.pl/api/";
        public static string ProductionApiUrl { get; private set; } = "https://wl-api.mf.gov.pl/api/";
    }
}
