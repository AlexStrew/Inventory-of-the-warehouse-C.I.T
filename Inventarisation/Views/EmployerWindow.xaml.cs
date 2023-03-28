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
    /// Логика взаимодействия для EmployerWindow.xaml
    /// </summary>
    public partial class EmployerWindow : Window
    {
        public ObservableCollection<Employers> DataEmployer { get; set; }
        public EmployerWindow()
        {
            InitializeComponent();
            LoadData();
        }

        public static implicit operator EmployerWindow(AddWindow v)
        {
            throw new NotImplementedException();
        }

        private async Task LoadData()
        {

            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var protectedToken = Properties.Settings.Default.JWTtoken;
            var token = protectedToken;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using (_client)
            {
                using (var response = await _client.GetAsync($"https://invent.doker.ru/api/Employers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataEmployer = JsonConvert.DeserializeObject<ObservableCollection<Employers>>(apiResponse);
                    EmployerDG.ItemsSource = DataEmployer;
                }
            }

        }

        private void AddEmployerWinBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEmployerWindow addWinNom = new AddEmployerWindow();
            if (addWinNom.ShowDialog() == true)
            {
                Console.WriteLine("hehe");
            }
            LoadData();
        }

        private void EditEmployerBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = EmployerDG.SelectedItem as Employers;
            if (selectedItem != null)
            {
                MessageBoxResult result = HandyControl.Controls.MessageBox.Show("Вы действительно хотите изменить данные?", "Редактирование", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Properties.Settings.Default.EmployerSelectProp = selectedItem.FullName;
                    Properties.Settings.Default.IdEmployerSelectProp = selectedItem.IdEmpolyer;
                    Properties.Settings.Default.Save();

                    EditEmployerWindow addWinNom = new EditEmployerWindow();
                    if (addWinNom.ShowDialog() == true)
                    {
                        Console.WriteLine("hehe");

                    }
                    LoadData();
                }
                else
                {
                    HandyControl.Controls.MessageBox.Show("Строка не выбрана", "Редактирование", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private async void DelEmployerBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = EmployerDG.SelectedItem as Employers;

            // Если строка выбрана
            if (selectedRow != null)
            {
                MessageBoxResult result = HandyControl.Controls.MessageBox.Show("Вы действительно хотите удалить строку?", "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Создание HttpClient
                    var client = new HttpClient();
                    var protectedToken = Properties.Settings.Default.JWTtoken;
                    var token = protectedToken;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // Отправка DELETE запроса на API
                    var response = await client.DeleteAsync($"https://invent.doker.ru/api/Employers/{selectedRow.IdEmpolyer}");

                    // Если ответ успешный
                    if (response.IsSuccessStatusCode)
                    {
                        // Удаление выбранной строки из sfDataGrid
                        var EmployerList = EmployerDG.ItemsSource as ObservableCollection<Employers>;
                        EmployerList.Remove(selectedRow);
                        EmployerDG.ItemsSource = EmployerList;
                        HandyControl.Controls.MessageBox.Show($"Удалено");
                    }
                    else
                    {
                        HandyControl.Controls.MessageBox.Show($"Произошла ошибка при удалении: {response.ReasonPhrase}");
                    }
                }
            }
            else
            {
                HandyControl.Controls.MessageBox.Show("Строка не выбрана", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RefreshWinBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SelectEmployerButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = EmployerDG.SelectedItem as Employers;

            // Если строка выбрана, сохраняем ее в Properties.Settings.Default.NomenSelectProp
            if (selectedItem != null)
            {
                Properties.Settings.Default.EmployerSelectProp = selectedItem.FullName;
                Properties.Settings.Default.IdEmployerSelectProp = selectedItem.IdEmpolyer;
                Properties.Settings.Default.Save();
            }

            DialogResult = true;
            this.Close();
        }

        private void SearchEmployerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.EmployerDG.SearchHelper.AllowFiltering = true;
            this.EmployerDG.SearchHelper.Search(SearchEmployerTBox.Text);
        }

        private void EmployerDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = EmployerDG.SelectedItem as Employers;

            // Если строка выбрана, сохраняем ее в Properties.Settings.Default.NomenSelectProp
            if (selectedItem != null)
            {
                Properties.Settings.Default.EmployerSelectProp = selectedItem.FullName;
                Properties.Settings.Default.IdEmployerSelectProp = selectedItem.IdEmpolyer;
                Properties.Settings.Default.Save();
            }

            DialogResult = true;
            this.Close();
        }
    }
}
