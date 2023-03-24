using Inventarisation.Api.ApiModel;
using Newtonsoft.Json;
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
    /// Логика взаимодействия для EditEmployerWindow.xaml
    /// </summary>
    public partial class EditEmployerWindow : Window
    {
        public ObservableCollection<Employers> EmployerCollection { get; set; }
        public EditEmployerWindow()
        {
            InitializeComponent();
            EmployerTBox.Text = Properties.Settings.Default.EmployerSelectProp;
        }

        private async void EditEmployerBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var protectedToken = Properties.Settings.Default.JWTtoken;
            var token = protectedToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Создание объекта Company для редактирования
            var editedEmployer = new Employers
            {
                IdEmpolyer = Properties.Settings.Default.IdEmployerSelectProp, // сохраняем Id редактируемой компании
                FullName = EmployerTBox.Text // новое значение для поля NameCompany,

            };

            // Преобразуем объект editedCompany в JSON
            var editedEmployerJson = JsonConvert.SerializeObject(editedEmployer);

            // Отправка PUT запроса на API
            var httpContent = new StringContent(editedEmployerJson, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://invent.doker.ru/api/Employers/{Properties.Settings.Default.IdEmployerSelectProp}", httpContent);

            // Если ответ успешный
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Сотрудник успешно отредактирован.");
                Properties.Settings.Default.IdEmployerSelectProp = 0;
                Properties.Settings.Default.EmployerSelectProp = "";
                Properties.Settings.Default.Save();
                Close();
            }
            else
            {
                MessageBox.Show("Не удалось отредактировать сотрудника. Попробуйте позже или обратитесь к администратору.");
            }
        }
    }
}
