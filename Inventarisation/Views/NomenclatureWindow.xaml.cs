using Inventarisation.Api.ApiModel;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;

namespace Inventarisation.Views
{
    /// <summary>
    /// Логика взаимодействия для NomenclatureWindow.xaml
    /// </summary>
    public partial class NomenclatureWindow : Window
    {
        public ObservableCollection<Nomenclature> DataNomen { get; set; }


        //List<Nomenclature> nomList;
        public NomenclatureWindow()
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
                using (var response = await _client.GetAsync($"https://invent.doker.ru/api/Nomenclatures"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataNomen = JsonConvert.DeserializeObject<ObservableCollection<Nomenclature>>(apiResponse);
                    NomenclatureDG.ItemsSource = DataNomen;
                }
            }

        }



        public static implicit operator NomenclatureWindow(AddWindow v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Открытие окна добавления устройства в справочник
        /// </summary>
        private void AddNomWinBtnClick(object sender, RoutedEventArgs e)
        {
            AddNomeclatureWindow addWinNom = new AddNomeclatureWindow();
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

        private void SelectNomenButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = NomenclatureDG.SelectedItem as Nomenclature;

            // Если строка выбрана, сохраняем ее в Properties.Settings.Default.NomenSelectProp
            if (selectedItem != null)
            {
                Properties.Settings.Default.NomenSelectProp = selectedItem.NameDevice;
                Properties.Settings.Default.IdNomenSelectProp = selectedItem.IdNomenclature;
                Properties.Settings.Default.Save();
            }

            DialogResult = true;
            this.Close();
        }



        private void EditNomenBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = NomenclatureDG.SelectedItem as Nomenclature;
            if (selectedItem != null)
            {
                MessageBoxResult result = HandyControl.Controls.MessageBox.Show("Вы действительно хотите изменить данные?", "Редактирование", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Properties.Settings.Default.NomenSelectProp = selectedItem.NameDevice;
                    Properties.Settings.Default.IdNomenSelectProp = selectedItem.IdNomenclature;
                    Properties.Settings.Default.Save();

                    EditNomenWindow addWinNom = new EditNomenWindow();
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

        private async void DelNomenBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = HandyControl.Controls.MessageBox.Show("Вы действительно хотите удалить строку?", "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var selectedRow = NomenclatureDG.SelectedItem as Nomenclature;

                // Если строка выбрана
                if (selectedRow != null)
                {
                    // Создание HttpClient
                    var client = new HttpClient();
                    var protectedToken = Properties.Settings.Default.JWTtoken;
                    var token = protectedToken;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // Отправка DELETE запроса на API
                    var response = await client.DeleteAsync($"https://invent.doker.ru/api/Nomenclatures/{selectedRow.IdNomenclature}");

                    // Если ответ успешный
                    if (response.IsSuccessStatusCode)
                    {
                        // Удаление выбранной строки из sfDataGrid
                        var nomenclatureList = NomenclatureDG.ItemsSource as ObservableCollection<Nomenclature>;
                        nomenclatureList.Remove(selectedRow);
                        NomenclatureDG.ItemsSource = nomenclatureList;
                        HandyControl.Controls.MessageBox.Show($"Удалено");
                    }
                    else
                    {
                        HandyControl.Controls.MessageBox.Show($"Произошла ошибка при удалении: {response.ReasonPhrase}");
                    }
                }
            }
        }

        private void SearchNomenBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NomenclatureDG.SearchHelper.AllowFiltering = true;
            this.NomenclatureDG.SearchHelper.Search(SearchNomenclatureTBox.Text);
        }

        private void NomenclatureDG_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedItem = NomenclatureDG.SelectedItem as Nomenclature;

            // Если строка выбрана, сохраняем ее в Properties.Settings.Default.NomenSelectProp
            if (selectedItem != null)
            {
                Properties.Settings.Default.NomenSelectProp = selectedItem.NameDevice;
                Properties.Settings.Default.IdNomenSelectProp = selectedItem.IdNomenclature;
                Properties.Settings.Default.Save();
            }

            DialogResult = true;
            this.Close();
        }
    }
}
