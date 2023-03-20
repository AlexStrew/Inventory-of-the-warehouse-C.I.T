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

using System.Drawing.Imaging;
using System.IO;
using QRCoder;
using System.Drawing;

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
            CompanyLoadData();

            string test = "G10488";
            InvNumTBox.Text = test;
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(test, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qRCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(25);
            InvNumQR.Source = BitmapToImageSource(qrCodeImage);

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


        private ImageSource BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void AddCompanyBtnOnClick(object sender, RoutedEventArgs e)
        {
            AddCompanyWindow addWinNom = new AddCompanyWindow();
            if (addWinNom.ShowDialog() == true)
            {
                Console.WriteLine("hehe");

            }
            CompanyNameCB.Items.Clear();
            PlacementCB.Items.Clear();
            CompanyLoadData();
        }

        private void WorkplaceBtnOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Страница в разработке");
        }

        private void SaveInvBtnOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Страница в разработке");
        }


        private async Task CompanyLoadData()
        {
            //var _client = new HttpClient();
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var protectedToken = Properties.Settings.Default.JWTtoken;
            //var token = protectedToken;
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //using (_client)
            //{
            //    using (var response = await _client.GetAsync("https://invent.doker.ru/api/Companies"))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        var result = JsonConvert.DeserializeObject<IEnumerable<Company>>(apiResponse);
            //        CompanyNameCB.ItemsSource = result;
            //    }
            //}

            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var protectedToken = Properties.Settings.Default.JWTtoken;
            var token = protectedToken;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using (_client)
            {
                using (var response = await _client.GetAsync("https://invent.doker.ru/api/Companies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var companies = JsonConvert.DeserializeObject<IEnumerable<Company>>(apiResponse);
                    foreach (var company in companies)
                    {
                        CompanyNameCB.Items.Add(company.CompanyName);
                    }
                }
                using (var response = await _client.GetAsync("https://invent.doker.ru/api/Workplaces/ConnectedTables"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var companies = JsonConvert.DeserializeObject<IEnumerable<WorkplaceConnected>>(apiResponse);
                    foreach (var company in companies)
                    {
                        PlacementCB.Items.Add(company.NamePlacement);
                    }
                }
            }
        }

        private async void PlacementCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var protectedToken = Properties.Settings.Default.JWTtoken;
            var token = protectedToken;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using (_client)
            {
                using (var response = await _client.GetAsync("https://invent.doker.ru/api/Workplaces/ConnectedTables"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var workplaces = JsonConvert.DeserializeObject<IEnumerable<WorkplaceConnected>>(apiResponse);
                    if (PlacementCB.SelectedItem != null)
                    {
                        string selectedPlacementName = PlacementCB.SelectedItem.ToString();

                        foreach (var workplace in workplaces)
                        {
                            var selectedDataItem = workplaces.Where(item => item.NamePlacement == selectedPlacementName).FirstOrDefault();

                            // Если элемент найден, выводим значение свойства FullName в TextBox
                            if (selectedDataItem != null)
                            {
                                if (selectedDataItem.FullName == null || selectedDataItem.FullName == "")
                                {
                                    OwnerNameTBox.Text = "";
                                }
                                OwnerNameTBox.Text = selectedDataItem.FullName;
                            }
                        }

                    }
                    
                }

            }
        }
    }
}
