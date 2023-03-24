using Inventarisation.Api.ApiModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;

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
