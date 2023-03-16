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
using System.Threading;
using Syncfusion.UI.Xaml.Grid;


namespace Inventarisation.ViewModel
{
    public class InventoriesViewModel
    {
      
       // public ObservableCollection<InvMain> DataVM { get; set; }
       //// public ObservableCollection<InvMain> DamaskCollection { get; set; }

       // public async Task GetDataAsync(SfDataGrid sfDataGrid)
       // {
       //     var _client = new HttpClient();
       //     _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
       //     var protectedToken = Properties.Settings.Default.JWTtoken;
       //     var token = protectedToken;
       //     _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
       //     using (var response = await _client.GetAsync("https://invent.doker.ru/api/Inventories/ConnectedTables"))
       //     {
       //         if (response.IsSuccessStatusCode)
       //         {
       //             string apiResponse = await response.Content.ReadAsStringAsync();
       //             DataVM = JsonConvert.DeserializeObject<ObservableCollection<InvMain>>(apiResponse);
       //             sfDataGrid.ItemsSource = DataVM;
       //         }
       //         else
       //         {
       //             string errorResponse = await response.Content.ReadAsStringAsync();
       //             throw new Exception($"Error getting data from API. Status code: {response.StatusCode}. Error message: {errorResponse}");
       //         }
       //     }
       // }

    }
}
