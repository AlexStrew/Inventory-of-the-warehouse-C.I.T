using Inventarisation.Api.ApiModel;
using Inventarisation.Models;
using Inventarisation.Pages;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace Inventarisation.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNomeclatureWindow.xaml
    /// </summary>
    public partial class AddNomeclatureWindow : Window
    {
        public ObservableCollection<Nomenclature> NomenCollection { get; set; }
        public AddNomeclatureWindow()
        {
            InitializeComponent();

            DateDeviceNow.SelectedDate = DateTime.Now;

        }

        /// <summary>
        /// Добавление устройства в справочник
        /// </summary>
        private async void  AddNomenBtnClick(object sender, RoutedEventArgs e )
        {
            if (NameDeviceTBox.Text != "" && NameDeviceTBox.Text != " " && CountDeviceTBox.Text != "" && ManufacturerDeviceTBox.Text != "" && ModelDeviceTBox.Text != "" && ManufacturerDeviceTBox.Text != "" && ManufacturerDeviceTBox.Text != "")
            {
                var nomenclature = new Nomenclature
                {
                    NameDevice = NameDeviceTBox.Text,
                    CountDevice = int.Parse(CountDeviceTBox.Text),
                    Manufacturer = ManufacturerDeviceTBox.Text,
                    Model = ModelDeviceTBox.Text,
                    DateCreation = DateTime.UtcNow,
                    DateChange = DateTime.UtcNow
                };

                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var protectedToken = Properties.Settings.Default.JWTtoken;
                var token = protectedToken;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (_client)
                {
                    using (var response = await _client.PostAsJsonAsync($"https://invent.doker.ru/api/Nomenclatures", nomenclature))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Удаление выбранной строки из sfDataGrid

                            HandyControl.Controls.MessageBox.Show($"Добавлено");
                            DialogResult = true;
                            this.Close();
                        }
                        else
                        {
                            HandyControl.Controls.MessageBox.Show($"Произошла ошибка при добавлении: {response.ReasonPhrase}");
                        }


                    }
                }
            }
            else
            {
                HandyControl.Controls.MessageBox.Show($"Поля не должны быть пустыми"); 
            }
            

            


            
            //try
            //{
            //    string nameDevice = NameDeviceTBox.Text;
            //    NomenclatureVM newObject = new NomenclatureVM();
            //    bool result = newObject.CheckAddNomenclature(nameDevice);
            //    if (result)
            //    {
            //        newObject.AddNomenclature(nameDevice);
            //        MessageBox.Show("Вы успешно добавили устройство в справочник.\nНе забудьте обновить страницу.");
            //        Close();
            //        //NomenclatureWindow winNom = new NomenclatureWindow();
            //        //if (winNom.ShowDialog() == true)
            //        //{
            //        //    Console.WriteLine("hehe");

            //        //}


            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }
    }
}
