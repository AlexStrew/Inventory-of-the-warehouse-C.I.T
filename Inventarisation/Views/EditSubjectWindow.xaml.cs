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
    /// Логика взаимодействия для EditSubjectWindow.xaml
    /// </summary>
    public partial class EditSubjectWindow : Window
    {
        public ObservableCollection<Subjects> SubjectCollection { get; set; }
        public EditSubjectWindow()
        {
            InitializeComponent();
            SubjectTBox.Text = Properties.Settings.Default.SubjectSelectProp;
        }

        private async void EditSubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var protectedToken = Properties.Settings.Default.JWTtoken;
            var token = protectedToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Создание объекта Company для редактирования
            var editedSubject = new Subjects
            {
                IdSubject = Properties.Settings.Default.IdSubjectSelectProp, // сохраняем Id редактируемой Предмет
                NameSubject = SubjectTBox.Text // новое значение для поля NAmeSubject,

            };

            // Преобразуем объект editedCompany в JSON
            var editedSubjectJson = JsonConvert.SerializeObject(editedSubject);

            // Отправка PUT запроса на API
            var httpContent = new StringContent(editedSubjectJson, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://invent.doker.ru/api/Subjects/{Properties.Settings.Default.IdSubjectSelectProp}", httpContent);

            // Если ответ успешный
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Предмет успешно отредактирован.");
                Properties.Settings.Default.IdSubjectSelectProp = 0;
                Properties.Settings.Default.SubjectSelectProp = "";
                Properties.Settings.Default.Save();
                Close();
            }
            else
            {
                MessageBox.Show("Не удалось отредактировать предмет. Попробуйте позже или обратитесь к администратору.");
            }
        }
    }
}
