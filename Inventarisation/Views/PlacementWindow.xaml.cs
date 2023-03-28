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
    /// Логика взаимодействия для PlacementWindow.xaml
    /// </summary>
    public partial class PlacementWindow : Window
    {
        public ObservableCollection<Placements> DataPlacement { get; set; }
        public PlacementWindow()
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
                using (var response = await _client.GetAsync($"https://invent.doker.ru/api/Placements"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataPlacement = JsonConvert.DeserializeObject<ObservableCollection<Placements>>(apiResponse);
                    PlacementDG.ItemsSource = DataPlacement;
                }
            }

        }

        public static implicit operator PlacementWindow(AddWindow v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Открытие окна добавления устройства в справочник
        /// </summary>
        private void AddPlaceWinBtnClick(object sender, RoutedEventArgs e)
        {
            //Close();
            AddPlacementWindow addWinNom = new AddPlacementWindow();
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


        private void SelectPlaceButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = PlacementDG.SelectedItem as Placements;

            // Если строка выбрана, сохраняем ее в Properties.Settings.Default.NomenSelectProp
            if (selectedItem != null)
            {
                Properties.Settings.Default.PlacementSelectProp = selectedItem.NamePlacement;
                Properties.Settings.Default.IdPlacementSelectProp = selectedItem.IdPlacement;
                Properties.Settings.Default.Save();
            }

            DialogResult = true;
            this.Close();
        }

        private void EditPlaceBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = PlacementDG.SelectedItem as Placements;
            if (selectedItem != null)
            {
                MessageBoxResult result = HandyControl.Controls.MessageBox.Show("Вы действительно хотите изменить данные?", "Редактирование", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Properties.Settings.Default.PlacementSelectProp = selectedItem.NamePlacement;
                    Properties.Settings.Default.IdPlacementSelectProp = selectedItem.IdPlacement;
                    Properties.Settings.Default.Save();

                    EditPlacementWindow addWinNom = new EditPlacementWindow();
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

        private async void DelPlaceBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = HandyControl.Controls.MessageBox.Show("Вы действительно хотите удалить строку?", "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var selectedRow = PlacementDG.SelectedItem as Placements;

                // Если строка выбрана
                if (selectedRow != null)
                {
                    // Создание HttpClient
                    var client = new HttpClient();
                    var protectedToken = Properties.Settings.Default.JWTtoken;
                    var token = protectedToken;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // Отправка DELETE запроса на API
                    var response = await client.DeleteAsync($"https://invent.doker.ru/api/Placements/{selectedRow.IdPlacement}");

                    // Если ответ успешный
                    if (response.IsSuccessStatusCode)
                    {
                        // Удаление выбранной строки из sfDataGrid
                        var placeList = PlacementDG.ItemsSource as ObservableCollection<Placements>;
                        placeList.Remove(selectedRow);
                        PlacementDG.ItemsSource = placeList;
                        HandyControl.Controls.MessageBox.Show($"Удалено");
                    }
                    else
                    {
                        HandyControl.Controls.MessageBox.Show($"Произошла ошибка при удалении: {response.ReasonPhrase}");
                    }
                }
            }
            
        }

        private void SearchPlaceBtn_Click(object sender, RoutedEventArgs e)
        {
            this.PlacementDG.SearchHelper.AllowFiltering = true;
            this.PlacementDG.SearchHelper.Search(SearchPlacementTBox.Text);
        }

        private void PlacementDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = PlacementDG.SelectedItem as Placements;

            // Если строка выбрана, сохраняем ее в Properties.Settings.Default.NomenSelectProp
            if (selectedItem != null)
            {
                Properties.Settings.Default.PlacementSelectProp = selectedItem.NamePlacement;
                Properties.Settings.Default.IdPlacementSelectProp = selectedItem.IdPlacement;
                Properties.Settings.Default.Save();
            }

            DialogResult = true;
            this.Close();
        }
    }
}
