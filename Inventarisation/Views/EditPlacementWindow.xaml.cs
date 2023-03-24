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
    /// Логика взаимодействия для EditPlacementWindow.xaml
    /// </summary>
    public partial class EditPlacementWindow : Window
    {
        public ObservableCollection<Placements> PlacementCollection { get; set; }
        public EditPlacementWindow()
        {
            InitializeComponent();
            PlacementTBox.Text = Properties.Settings.Default.PlacementSelectProp;
        }

        private async void EditPlaceBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var protectedToken = Properties.Settings.Default.JWTtoken;
            var token = protectedToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Создание объекта Company для редактирования
            var editedPlacement = new Placements
            {
                IdPlacement = Properties.Settings.Default.IdPlacementSelectProp, // сохраняем Id редактируемой компании
                NamePlacement = PlacementTBox.Text // новое значение для поля NameCompany,

            };

            // Преобразуем объект editedCompany в JSON
            var editedPlacementJson = JsonConvert.SerializeObject(editedPlacement);

            // Отправка PUT запроса на API
            var httpContent = new StringContent(editedPlacementJson, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://invent.doker.ru/api/Placements/{Properties.Settings.Default.IdPlacementSelectProp}", httpContent);

            // Если ответ успешный
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Размещение успешно отредактировано.");
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
