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
            MessageBox.Show("Страница в разработке");
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
            }
        }
    }
}
