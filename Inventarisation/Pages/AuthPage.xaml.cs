using Microsoft.AspNetCore.DataProtection;
using Microsoft.Owin.Security.DataProtection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Http.Headers;

namespace Inventarisation.Pages
{

    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private readonly HttpClient _client;
        private readonly Microsoft.AspNetCore.DataProtection.IDataProtector _protector;
        public AuthPage()
        {
            InitializeComponent();
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _protector = DataProtectionProvider.Create("Contoso").CreateProtector("JWT");

        }

        public class Token
        {
            public string AccessToken { get; set; }
        }

        private async void LoginBtnClick(object sender, RoutedEventArgs e)
        {


            var values = new Dictionary<string, string>
            {
                { "username", LoginTextBox.Text },
                { "password", AuthPasswordBox.Text }
            };

            var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");


            var response = await _client.PostAsync("http://invent.doker.ru/api/Authenticate/login", content);
            
            await Console.Out.WriteLineAsync(response.ToString());
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<Token>(json);
                var protectedToken = token.AccessToken;
                Console.WriteLine(protectedToken);
                Properties.Settings.Default.JWTtoken = protectedToken;
                Properties.Settings.Default.Save();
                this.NavigationService.Navigate(new MainPage());
            }
        }
        
    

        private void DontAuthTBMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }
    }
}
