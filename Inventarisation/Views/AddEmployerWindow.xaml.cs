using Inventarisation.Api.ApiModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Inventarisation.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEmployerWindow.xaml
    /// </summary>
    public partial class AddEmployerWindow : Window
    {
        public ObservableCollection<Employers> EmployerCollection { get; set; }
        public AddEmployerWindow()
        {
            InitializeComponent();
            
        }

        private async void AddEmployerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (EmployerTBox.Text != "" && EmployerTBox.Text != " ")
            {
                var employers = new Employers
                {
                    FullName = EmployerTBox.Text
                };

                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var protectedToken = Properties.Settings.Default.JWTtoken;
                var token = protectedToken;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (_client)
                {
                    using (var response = await _client.PostAsJsonAsync($"https://invent.doker.ru/api/Employers", employers))
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
                HandyControl.Controls.MessageBox.Show($"Поле: ФИО не должно быть пустым");
            }
        }
    }
}
