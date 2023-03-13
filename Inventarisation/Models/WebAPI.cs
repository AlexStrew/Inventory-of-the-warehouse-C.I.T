using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Inventarisation.Api;
using Inventarisation.Api.Controllers;
using Microsoft.AspNetCore.Http;

namespace Inventarisation.Models
{
     class WebAPI
    {
        
        public static Task<HttpResponseMessage> GetCall(string url)
        {

            try
            {

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string apiUrl = Manager.RootURL + url;
                
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.Timeout = TimeSpan.FromSeconds(900);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync(apiUrl);
                    response.Wait();
                    return response;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
