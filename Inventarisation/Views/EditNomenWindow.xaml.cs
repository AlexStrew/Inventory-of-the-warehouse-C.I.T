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
    /// Логика взаимодействия для EditNomenWindow.xaml
    /// </summary>
    public partial class EditNomenWindow : Window
    {
        public ObservableCollection<Nomenclature> NomenCollection { get; set; }
        public EditNomenWindow()
        {
            InitializeComponent();
            NameDeviceTBox.Text = Properties.Settings.Default.NomenSelectProp;
        }

        private async void EditNomenBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var protectedToken = Properties.Settings.Default.JWTtoken;
            var token = protectedToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Создание объекта Company для редактирования
            var editedNomen = new Nomenclature
            {
                IdNomenclature = Properties.Settings.Default.IdNomenSelectProp, // сохраняем Id редактируемой компании
                NameDevice = NameDeviceTBox.Text // новое значение для поля NameCompany,

            };

            // Преобразуем объект editedCompany в JSON
            var editedNomenJson = JsonConvert.SerializeObject(editedNomen);

            // Отправка PUT запроса на API
            var httpContent = new StringContent(editedNomenJson, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://invent.doker.ru/api/Nomenclatures/{Properties.Settings.Default.IdNomenSelectProp}", httpContent);

            // Если ответ успешный
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Номеклатура успешно отредактирована.");
                Properties.Settings.Default.IdNomenSelectProp = 0;
                Properties.Settings.Default.NomenSelectProp = "";
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
