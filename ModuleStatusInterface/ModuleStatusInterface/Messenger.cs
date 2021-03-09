using FluentValidation;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;

namespace ModuleStatusInterface
{
    public static class Messenger
    {
        public static async Task<ModuleStatus> SendStatus(Target target, ModuleStatus status)
        {
            var client = GetHttpClient(target, status);
            var response = await client.PostAsJsonAsync("modules", status);
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<ModuleStatus>().Result;
        }

        public static ModuleStatus SendStatusSync(ModuleStatus status) {
            return SendStatusSync(Messenger.GetTargetFromString(), status);
        }

        public static ModuleStatus SendStatusSync(Target target, ModuleStatus status)
        {
            var client = GetHttpClient(target, status);
            var response = client.PostAsJsonAsync("modules", status).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<ModuleStatus>().Result;

        }

        /// <summary>
        /// Takes "dev" "stg" or "prd" and returns an enum
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Target GetTargetFromString(string str = null)
        {
            var t = ConfigurationManager.AppSettings["DashboardTargetString"];
            str = t ?? str;
            return str.ToLower() switch
            {
                "dev" => Target.Dev,
                "stg" => Target.Stg,
                "prd" => Target.Prd,
                _ => throw new ArgumentException(),
            };
        }

        #region helpers

        private static void ValidateModuleAndStatus(ModuleStatus mod)
        {
            var moduleValidator = new Validator.ModuleStatusValidator();
            moduleValidator.ValidateAndThrow(mod);
            var statusValidator = new Validator.StatusItemValidator();
            mod?.StatusItems?.ForEach(x => statusValidator.ValidateAndThrow(x));
        }

        private static string GetUrlFromTarget(Target t)
        {

            var d = ConfigurationManager.AppSettings["DashboardDevString"];
            var s = ConfigurationManager.AppSettings["DashboardStgString"];
            var p = ConfigurationManager.AppSettings["DashboardPrdString"];

            return t == Target.Dev ? d ?? "http://localhost:51866/api/" :
                   t == Target.Stg ? s ?? "http://stg-dashboard.umpublishing.org/api/" :
                   t == Target.Prd ? p ?? "http://dashboard.umpublishing.org/api/":
                   null;
        }

        private static HttpClient GetHttpClient(Target target, ModuleStatus status)
        {
            ValidateModuleAndStatus(status);
            var targetString = GetUrlFromTarget(target);
            var client = new HttpClient
            {
                BaseAddress = new Uri(targetString)
            };
            return client;
        }

        #endregion

    }
}

