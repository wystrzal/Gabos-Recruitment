using Gabos_Recruitment.Module.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gabos_Recruitment.Module.Services.ApiUrlAssigners
{
    public abstract class ApiUrlAssigner
    {
        public string BaseUrl { get; set; } = ApiStorage.TestApiUrl;

        public abstract string GetActionUrl(object data, int specification);
    }
}
