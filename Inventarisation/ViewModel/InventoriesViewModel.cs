﻿using Microsoft.AspNetCore.DataProtection;
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


namespace Inventarisation.ViewModel
{
    public class InventoriesViewModel
    {
      
        public ObservableCollection<InvMain> DataVM { get; set; }


        public async void GetData()
        {
            HttpClient _client;
            IDataProtector _protector;
            DataVM = new ObservableCollection<InvMain>();

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _protector = DataProtectionProvider.Create("Contoso").CreateProtector("JWT");

            var protectedToken = Properties.Settings.Default.JWTtoken;

            var token = protectedToken;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("https://invent.doker.ru/api/Inventories/ConnectedTables");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<InvMain>>(json);

                foreach (var item in data)
                {
                   
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
