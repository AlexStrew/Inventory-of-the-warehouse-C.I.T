using HandyControl.Tools;
using Inventarisation.Models;
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
using Syncfusion.SfSkinManager;
using Aspose.BarCode.Generation;
using QRCoder;
using System.Net.NetworkInformation;
using HandyControl.Tools;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;
using System.Drawing;
using System.Text.RegularExpressions;
using Syncfusion.Windows.Controls;
using System.Windows.Forms;

namespace Inventarisation.Views
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
       
        public AddWindow()
        {
            InitializeComponent();
            ConfigHelper.Instance.SetLang("ru-ru");
            

           

        }

        /// <summary>
        /// Открытие окна с нуменклатурой
        /// </summary>
        private void SelectNumButtonClick(object sender, RoutedEventArgs e)
        {
            NomenclatureWindow win = new NomenclatureWindow();
            if (win.ShowDialog() == true)
            {
               
                Console.WriteLine("sdsd");
            }
            NameDeviceTB.Text = Properties.Settings.Default.NomenSelectProp;

        }


        private void AddCompanyBtnOnClick(object sender, RoutedEventArgs e)
        {
            CompanyWindow addWinNom = new CompanyWindow();
            if (addWinNom.ShowDialog() == true)
            {
                Console.WriteLine("hehe");

            }
          
            CompanyNameCB.Text = Properties.Settings.Default.CompanySelectProp;
            
        }               

        //private async void PlacementCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var _client = new HttpClient();
        //    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    var protectedToken = Properties.Settings.Default.JWTtoken;
        //    var token = protectedToken;
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    using (_client)
        //    {
        //        using (var response = await _client.GetAsync("https://invent.doker.ru/api/Workplaces/ConnectedTables"))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            var workplaces = JsonConvert.DeserializeObject<IEnumerable<WorkplaceConnected>>(apiResponse);
        //            if (PlacementCB.SelectedItem != null)
        //            {
        //                string selectedPlacementName = PlacementCB.SelectedItem.ToString();

        //                foreach (var workplace in workplaces)
        //                {
        //                    var selectedDataItem = workplaces.Where(item => item.NamePlacement == selectedPlacementName).FirstOrDefault();

        //                    // Если элемент найден, выводим значение свойства FullName в TextBox
        //                    if (selectedDataItem != null)
        //                    {
        //                        if (selectedDataItem.FullName == null || selectedDataItem.FullName == "")
        //                        {
        //                            OwnerNameTBox.Text = "";
        //                        }
        //                        OwnerNameTBox.Text = selectedDataItem.FullName;
        //                    }
        //                }

        //            }
                    
        //        }

        //    }
        //}

        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            // Проверяем, что вводимый символ - цифра
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsNumeric(string text)
        {
            Regex regex = new Regex("[^0-9]+"); // Определяем регулярное выражение для поиска всех символов, кроме цифр
            return !regex.IsMatch(text); // Возвращаем true, если текст содержит только цифры
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




        private async void SaveInvBtnOnClick(object sender, RoutedEventArgs e)
        {
          
            
            //dateNew = DateTime.UtcNow;
            if (NameDeviceTB.Text != "" && CompanyNameCB.Text != "" && PaymentNumTB.Text != "" && CommentTB.Text != "" && InvoiceTB.Text != "" && PlacementTBox.Text != "")
            {
                var invent = new invAdding
                {
                    NomenclatureId = Properties.Settings.Default.IdNomenSelectProp,
                    CompanyId = Properties.Settings.Default.IdCompanySelectProp,
                    PaymentNum = PaymentNumTB.Text.ConvertToInt(),
                    Comment = CommentTB.Text,
                    Invoice = InvoiceTB.Text,
                    DateInv = DateTime.UtcNow

                };

                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var protectedToken = Properties.Settings.Default.JWTtoken;
                var token = protectedToken;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (_client)
                {
                    using (var response = await _client.PostAsJsonAsync($"https://invent.doker.ru/api/Inventories", invent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Удаление выбранной строки из sfDataGrid

                            HandyControl.Controls.MessageBox.Show($"Добавлено");
                            Properties.Settings.Default.IdNomenSelectProp = 0;
                            Properties.Settings.Default.IdCompanySelectProp = 0;
                            Properties.Settings.Default.IdPlacementSelectProp = 0;
                            Properties.Settings.Default.CompanySelectProp = "";
                            Properties.Settings.Default.NomenSelectProp = "";
                            Properties.Settings.Default.PlacementSelectProp = "";
                            Properties.Settings.Default.Save();
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
        }
    }
}
