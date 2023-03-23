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
using System.Collections.ObjectModel;
using Inventarisation.Api.ApiModel;

namespace Inventarisation.Views
{
    /// <summary>
    /// Логика взаимодействия для EditCompanyWindow.xaml
    /// </summary>
    public partial class EditCompanyWindow : Window
    {
        public ObservableCollection<Company> CompanyCollection { get; set; }
        public EditCompanyWindow()
        {
            InitializeComponent();
            CompanyTBox.Text = Properties.Settings.Default.CompanySelectProp;
        }

        private async void EditCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var protectedToken = Properties.Settings.Default.JWTtoken;
            var token = protectedToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Создание объекта Company для редактирования
            var editedCompany = new Company
            {
                IdCompany = Properties.Settings.Default.IdCompanySelectProp, // сохраняем Id редактируемой компании
                CompanyName = CompanyTBox.Text // новое значение для поля NameCompany,
                
            };

            // Преобразуем объект editedCompany в JSON
            var editedCompanyJson = JsonConvert.SerializeObject(editedCompany);

            // Отправка PUT запроса на API
            var httpContent = new StringContent(editedCompanyJson, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://invent.doker.ru/api/Companies/{Properties.Settings.Default.IdCompanySelectProp}", httpContent);

            // Если ответ успешный
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Компания успешно отредактирована.");
                Properties.Settings.Default.IdCompanySelectProp = 0;
                Properties.Settings.Default.CompanySelectProp = "";
                Properties.Settings.Default.Save();
                Close();
            }
            else
            {
                MessageBox.Show("Не удалось отредактировать компанию. Попробуйте позже или обратитесь к администратору.");
            }
        }
    }
}
