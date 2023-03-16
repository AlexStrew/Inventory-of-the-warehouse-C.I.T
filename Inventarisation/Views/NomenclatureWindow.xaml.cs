using Inventarisation.Api.ApiModel;
using Inventarisation.Models;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using Syncfusion.Data.Extensions;
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
using Syncfusion.Windows.Shared;
using System.Windows.Forms;
using System.Xml.Linq;
using Syncfusion.SfSkinManager;
using Inventarisation.Properties;
using Syncfusion.UI.Xaml.Grid;
using Eco.Persistence;

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
            //Close();
            AddNomeclatureWindow addWinNom = new AddNomeclatureWindow();
            if (addWinNom.ShowDialog() == true)
            {
                Console.WriteLine("hehe");
                
            }
            



        }

        private void RefreshWinBtnClick(object sender, RoutedEventArgs e)
        {
           
            CollectionViewSource.GetDefaultView(NomenclatureDG.ItemsSource).Refresh();
        }


        private void SearchNomenclatureTBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            
            //CollectionViewSource.GetDefaultView(NomenclatureDG.ItemsSource).Refresh();
        }

        private void SelectNomenButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = NomenclatureDG.SelectedItem as Nomenclature;

            // Если строка выбрана, сохраняем ее в Properties.Settings.Default.NomenSelectProp
            if (selectedItem != null)
            {
                Properties.Settings.Default.NomenSelectProp = selectedItem.NameDevice;
                Properties.Settings.Default.Save();
            }
            
            DialogResult = true;
            this.Close();
        }

        private void NomenclatureDG_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            //var selectedItem = NomenclatureDG.SelectedItem;
            //Properties.Settings.Default.NomenSelectProp = selectedItem.ToString();
            //Properties.Settings.Default.Save();
          
                
         
            var selectedCell = NomenclatureDG.SelectedItem;
            Properties.Settings.Default.NomenSelectProp = selectedCell.ToString();
            Properties.Settings.Default.Save();
        }

        private void EditNomenBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void DelNomenBtn_Click(object sender, RoutedEventArgs e)
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

        private void SearchNomenBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NomenclatureDG.SearchHelper.AllowFiltering = true;
            this.NomenclatureDG.SearchHelper.Search(SearchNomenclatureTBox.Text);
        }
    }
}
