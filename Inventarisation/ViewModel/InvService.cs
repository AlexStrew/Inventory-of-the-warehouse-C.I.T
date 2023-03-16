using Inventarisation.Api.ApiModel;
using Newtonsoft.Json;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Inventarisation.ViewModel
{
    public class InvService
    {
        private readonly HttpClient _client;

        public InvService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<InvMain>> GetMyModelsAsync(int page, int pageSize)
        {
            var response = await _client.GetAsync($"https://example.com/api/mymodels?page={page}&pageSize={pageSize}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error retrieving data: {response.StatusCode} - {content}");
            }

            return JsonConvert.DeserializeObject<List<InvMain>>(content);
        }
    }
}



//var _client = new HttpClient();
//_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//var protectedToken = Properties.Settings.Default.JWTtoken;
//var token = protectedToken;
//_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//using (var response = await _client.GetAsync("https://invent.doker.ru/api/Inventories/ConnectedTables"))
//{
//    if (response.IsSuccessStatusCode)
//    {
//        string apiResponse = await response.Content.ReadAsStringAsync();
//        DamaskCollection = JsonConvert.DeserializeObject<ObservableCollection<InvMain>>(apiResponse);
//        sfDataGrid.ItemsSource = DamaskCollection;
//    }
//    else
//    {
//        string errorResponse = await response.Content.ReadAsStringAsync();
//        throw new Exception($"Error getting data from API. Status code: {response.StatusCode}. Error message: {errorResponse}");
//    }
//}