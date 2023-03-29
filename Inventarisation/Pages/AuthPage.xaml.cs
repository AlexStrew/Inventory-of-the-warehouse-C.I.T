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
using WECPOFLogic;
using System.Windows.Forms;
using System.Security.Principal;
using static Inventarisation.Pages.AuthPage;

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
            CheckConnectionServer();
        }

        public class Token
        {
            public string token { get; set; }
        }

        private async void LoginBtnClick(object sender, RoutedEventArgs e)
        {


            var values = new Dictionary<string, string>
            {
                { "username", LoginTextBox.Text },
                { "password", AuthPasswordBox.Text }
            };

            var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");


            var response = await _client.PostAsync("https://invent.doker.ru/api/Authenticate/login", content);
            
            await Console.Out.WriteLineAsync(response.ToString());
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<Token>(json);

                Properties.Settings.Default.JWTtoken = token.token;
                Properties.Settings.Default.Save();
                this.NavigationService.Navigate(new MainPage());
            }
            MessageBoxManager.OK = "نعم";
            MessageBoxManager.No = "الله";
            MessageBoxManager.Cancel = "شهيد";
            MessageBoxManager.Register();
            System.Windows.Forms.MessageBox.Show("الحمار الانفجار...", "سوبرجود", MessageBoxButtons.YesNoCancel);
            MessageBoxManager.Unregister();
        }
        
    

        private void DontAuthTBMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void CallHelp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show("Абонент недоступен, перезвоните позже","", MessageBoxButton.OK);
        }

        private void LoginWinBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (principal.IsInRole(WindowsBuiltInRole.User))
            {
                Console.WriteLine("User Name: " + identity.Name);
                Console.WriteLine("Is Authenticated: " + identity.IsAuthenticated);
                Console.WriteLine("Authentication Type: " + identity.AuthenticationType);
                Console.WriteLine("Is User in User role: " + principal.IsInRole(WindowsBuiltInRole.User));

                Properties.Settings.Default.CurrentUser = identity.Name;
                Console.WriteLine(Properties.Settings.Default.CurrentUser);
                Properties.Settings.Default.Save();
                GetTokenWin();
                Task.Delay(2000);
                this.NavigationService.Navigate(new MainPage());
            }
            else
            {
                
                Console.WriteLine("User Name: " + identity.Name);
                Console.WriteLine("Is Authenticated: " + identity.IsAuthenticated);
                Console.WriteLine("Authentication Type: " + identity.AuthenticationType);
                Console.WriteLine("Is User in User role: " + principal.IsInRole(WindowsBuiltInRole.User));
                System.Windows.MessageBox.Show("Ошибка", "", MessageBoxButton.OK);
            }
        }

        private async void GetTokenWin()
        {
            var values = new Dictionary<string, string>
            {
                { "username", "AdminApi" },
                { "password", "QFgQJkWi4t" }
            };

            var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");


            var response = await _client.PostAsync("https://invent.doker.ru/api/Authenticate/login", content);

            await Console.Out.WriteLineAsync(response.ToString());
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<Token>(json);

                Properties.Settings.Default.JWTtoken = token.token;
                Properties.Settings.Default.Save();
                
            }
        }

        private async Task CheckConnectionServer()
        {
            try
            {
                var _client = new HttpClient();
                //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.JWTtoken);
                var response = await _client.GetAsync("https://invent.doker.ru/api/Status");
                if (response.IsSuccessStatusCode)
                {
                    ellipse.Fill = Brushes.Green;
                }
                else
                {
                    ellipse.Fill = Brushes.Red;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private async void CheckConUpd_Click(object sender, RoutedEventArgs e)
        {
            await CheckConnectionServer();
        }
    }
}
