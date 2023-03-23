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
    /// Логика взаимодействия для CompanyWindow.xaml
    /// </summary>
    public partial class CompanyWindow : Window
    {
        public ObservableCollection<Company> DataCompany { get; set; }
        public CompanyWindow()
        {
            InitializeComponent();
            LoadData();
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
                using (var response = await _client.GetAsync($"https://invent.doker.ru/api/Companies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataCompany = JsonConvert.DeserializeObject<ObservableCollection<Company>>(apiResponse);
                    CompanyDG.ItemsSource = DataCompany;
                }
            }

        }

        public static implicit operator CompanyWindow(AddWindow v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Открытие окна добавления устройства в справочник
        /// </summary>
        private void AddCompanyWinBtnClick(object sender, RoutedEventArgs e)
        {
            //Close();
            AddCompanyWindow addWinNom = new AddCompanyWindow();
            if (addWinNom.ShowDialog() == true)
            {
                Console.WriteLine("hehe");

            }
            LoadData();
        }

        private void RefreshWinBtnClick(object sender, RoutedEventArgs e)
        {

            LoadData();
        }


        private void SelectCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = CompanyDG.SelectedItem as Company;

            // Если строка выбрана, сохраняем ее в Properties.Settings.Default.NomenSelectProp
            if (selectedItem != null)
            {
                Properties.Settings.Default.CompanySelectProp = selectedItem.CompanyName;
                Properties.Settings.Default.IdCompanySelectProp = selectedItem.IdCompany;
                Properties.Settings.Default.Save();
            }

            DialogResult = true;
            this.Close();
        }

        private async void EditCompanyBtn_Click(object sender, RoutedEventArgs e)
        {


            var selectedItem = CompanyDG.SelectedItem as Company;
            if (selectedItem != null)
            {
                MessageBoxResult result = HandyControl.Controls.MessageBox.Show("Вы действительно хотите изменить данные?", "Редактирование", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Properties.Settings.Default.CompanySelectProp = selectedItem.CompanyName;
                    Properties.Settings.Default.IdCompanySelectProp = selectedItem.IdCompany;
                    Properties.Settings.Default.Save();

                    EditCompanyWindow addWinNom = new EditCompanyWindow();
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

    

        private async void DelCompanyBtn_Click(object sender, RoutedEventArgs e)
        {

            var selectedRow = CompanyDG.SelectedItem as Company;

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
                    var response = await client.DeleteAsync($"https://invent.doker.ru/api/Companies/{selectedRow.IdCompany}");

                    // Если ответ успешный
                    if (response.IsSuccessStatusCode)
                    {
                        // Удаление выбранной строки из sfDataGrid
                        var placeList = CompanyDG.ItemsSource as ObservableCollection<Company>;
                        placeList.Remove(selectedRow);
                        CompanyDG.ItemsSource = placeList;
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

        private void SearchCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            this.CompanyDG.SearchHelper.AllowFiltering = true;
            this.CompanyDG.SearchHelper.Search(SearchCompanyTBox.Text);
        }
    }
}
