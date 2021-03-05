using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Runtime.Remoting.Channels;
using FluentValidation;

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

        [Obsolete("Use capitalized one, please!")]
        public static Target getTargetFromString(string str = null) {
            return GetTargetFromString(str);
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
            switch (str.ToLower())
            {
                case "dev":
                    return Target.Dev;
                case "stg":
                    return Target.Stg;
                case "prd":
                    return Target.Prd;
                default:
                    throw new ArgumentException();
            }
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
            var client = new HttpClient();
            client.BaseAddress = new Uri(targetString);
            return client;
        }

        #endregion

    }
}

