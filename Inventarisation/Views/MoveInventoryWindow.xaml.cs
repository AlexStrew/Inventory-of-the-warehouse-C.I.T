using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Inventarisation.Api.ApiModel;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Http;

namespace Inventarisation.Views
{
    /// <summary>
    /// Логика взаимодействия для MoveInventoryWindow.xaml
    /// </summary>
    public partial class MoveInventoryWindow : Window
    {
        public MoveInventoryWindow()
        {
            InitializeComponent();

            InvNumTBox.Text =  Properties.Settings.Default.InvNumForPrint;

            PlacementTBox.Text = Properties.Settings.Default.PlacementSelectProp;

            EmployerTBox.Text = Properties.Settings.Default.EmployerSelectProp;
        }

        private void SelectPlaceButton_Click(object sender, RoutedEventArgs e)
        {
            PlacementWindow win = new PlacementWindow();
            if (win.ShowDialog() == true)
            {

                Console.WriteLine("sdsd");
            }
            PlacementTBox.Text = Properties.Settings.Default.PlacementSelectProp;
        }

        private void SelectEmployerButton_Click(object sender, RoutedEventArgs e)
        {
            EmployerWindow win = new EmployerWindow();
            if (win.ShowDialog() == true)
            {

                Console.WriteLine("sdsd");
            }
            EmployerTBox.Text = Properties.Settings.Default.EmployerSelectProp;
        }

        private async void ReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlacementTBox.Text != "" && EmployerTBox.Text != "")
            {
                var movements = new Movements
                {
                    IdInventory = Properties.Settings.Default.IdInventorySelectedProp,
                    DateMove = DateTime.UtcNow,
                    PlacementId = Properties.Settings.Default.IdPlacementSelectProp,
                    EmployerId = Properties.Settings.Default.IdEmployerSelectProp
                };                

                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var protectedToken = Properties.Settings.Default.JWTtoken;
                var token = protectedToken;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (_client)
                {
                    using (var response = await _client.GetAsync($"https://invent.doker.ru/api/Movements/GetLast"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<int>(apiResponse);
                        if (response.IsSuccessStatusCode)
                        {
                            var _client1 = new HttpClient();
                            _client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var protectedToken1 = Properties.Settings.Default.JWTtoken;
                            var token1 = protectedToken1;
                            _client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token1);
                            using (_client1)
                            {
                                using (var response1 = await _client1.PostAsJsonAsync($"https://invent.doker.ru/api/Movements", movements))
                                {
                                    if (response1.IsSuccessStatusCode)
                                    {
                                        var inventory = new Inventory
                                        {
                                            Id = Properties.Settings.Default.IdInventorySelectedProp, //  Id inv
                                            MoveId = result
                                        };

                                        var _client2 = new HttpClient();
                                        _client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        var protectedToken2 = Properties.Settings.Default.JWTtoken;
                                        var token2 = protectedToken2;
                                        _client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token2);

                                        var updatedInventoryJson = JsonConvert.SerializeObject(inventory);
                                        var httpContent = new StringContent(updatedInventoryJson, Encoding.UTF8, "application/json");

                                        using (_client2)
                                        {
                                            
                                            using (var response2 = await _client2.PutAsync($"https://invent.doker.ru/api/Inventories/{Properties.Settings.Default.IdInventorySelectedProp}", httpContent)) 
                                            {
                                                if (response2.IsSuccessStatusCode)
                                                {

                                                    var _client3 = new HttpClient();
                                                    _client3.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                                    var protectedToken3 = Properties.Settings.Default.JWTtoken;
                                                    var token3 = protectedToken3;
                                                    _client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token3);

                                                    var updatedMoveJson = JsonConvert.SerializeObject(inventory);
                                                    var httpContent3 = new StringContent(updatedMoveJson, Encoding.UTF8, "application/json");

                                                    using (_client3)
                                                    {
                                                        #warning Try to Fix 
                                                        using (var response3 = await _client3.PutAsync($"https://invent.doker.ru/api/Movements/empSetSet?id={Properties.Settings.Default.IdEmployerSelectProp}", httpContent3))
                                                        {
                                                            if (response3.IsSuccessStatusCode)
                                                            {
                                                                HandyControl.Controls.MessageBox.Show($"Перемещено");
                                                                DialogResult = true;
                                                                this.Close();
                                                            }
                                                        }
                                                    }
                                                






                                                            
                                                }
                                            }
                                            
                                            
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                HandyControl.Controls.MessageBox.Show($"Поле: Наименование не должно быть пустым");
            }
        }
    }
}
