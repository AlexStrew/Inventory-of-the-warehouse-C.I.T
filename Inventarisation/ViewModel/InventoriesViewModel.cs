using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Inventarisation.Api.ApiModel;
using Inventarisation.Models;
using Inventarisation.Views;

namespace Inventarisation.ViewModel
{
    public class InventoriesViewModel
    {
        public ObservableCollection<Inventory> DataVM { get; set; }

        public async void GetData()
        {
            HttpClient _client;
            IDataProtector _protector;
            DataVM = new ObservableCollection<Inventory>();

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _protector = DataProtectionProvider.Create("Contoso").CreateProtector("JWT");

            var protectedToken = Properties.Settings.Default.JWTtoken;

            var token = protectedToken;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("http://invent.doker.ru/api/Inventories");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Inventory>>(json);

                foreach (var item in data)
                {
                    await Console.Out.WriteLineAsync("Data");
                    DataVM.Add(item);
                }
               
            }
            else
            {
                await Console.Out.WriteLineAsync("errororororororor");
            }
        }

    }
}
