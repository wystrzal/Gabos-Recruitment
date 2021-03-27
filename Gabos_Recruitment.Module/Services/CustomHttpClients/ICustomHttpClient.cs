using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gabos_Recruitment.Module.Services.CustomHttpClients
{
    public interface ICustomHttpClient
    {
        Task<string> GetStringAsync(string uri, Dictionary<string, string> queryParams = null);
    }
}
