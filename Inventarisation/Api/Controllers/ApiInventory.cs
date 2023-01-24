using Inventarisation.Api.ApiModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Inventarisation.Api.Controllers
{
    public class ApiInventory
    {
        /// <summary>
        /// Вывод категорий
        /// </summary>
        /// <returns></returns>
        public static List<Inventory> GetInventory()
        {
            using (HttpClient client = new HttpClient())
            {
                string query = $"{Manager.RootURL}Inventories";
                Console.WriteLine(query);
                HttpResponseMessage response = client.GetAsync(query).Result;
                var content = response.Content.ReadAsStringAsync();
                var answer = JsonConvert
                    .DeserializeObject<ResponseApi<List<Inventory>>>(content.Result);
                return answer.Data;
            }
        }

    }
}
