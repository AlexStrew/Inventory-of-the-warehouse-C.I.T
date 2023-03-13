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

namespace Inventarisation.Views
{
    /// <summary>
    /// Логика взаимодействия для NomenclatureWindow.xaml
    /// </summary>
    public partial class NomenclatureWindow : Window
    {
        public ObservableCollection<Nomenclature> DataNomen { get; set; }
       
        //List<Nomenclature> nomList;
        public NomenclatureWindow(VisualStyles theme)
        {
            InitializeComponent();
            //SfSkinManager.SetVisualStyle(this, Settings.VisualStyles.CurrentTheme);
            SfSkinManager.SetVisualStyle(this, theme);
            DataContext = this;
            GetData();
            //nomList = db.context.Nomenclature.OrderBy(x=>x.name_device).ToList();
            //NomenclatureDG.ItemsSource = nomList;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(NomenclatureDG.ItemsSource);
            
            //view.Filter = UserFilter;
        }


        private async void GetData()
        {
            HttpClient _client;
            IDataProtector _protector;
            DataNomen = new ObservableCollection<Nomenclature>();

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _protector = DataProtectionProvider.Create("Contoso").CreateProtector("JWT");

            var protectedToken = Properties.Settings.Default.JWTtoken;
            await Console.Out.WriteLineAsync(protectedToken);
            var token = protectedToken;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("http://invent.doker.ru/api/Nomenclatures");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Nomenclature>>(json);
                // Bind the data to the datagrid
                //sfDataGrid.ItemsSource = data;
                foreach (var item in data)
                {
                    DataNomen.Add(item);
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("errororororororor");
            }
        }
        //private bool UserFilter(object item)
        //{
        //    if (String.IsNullOrEmpty(SearchNomenclatureTBox.Text))
        //        return true;
        //    //else
        //    //    return ((item as Nomenclature).name_device.IndexOf(SearchNomenclatureTBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        //}

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
            //nomList = db.context.Nomenclature.ToList();
            //NomenclatureDG.ItemsSource = nomList;
            CollectionViewSource.GetDefaultView(NomenclatureDG.ItemsSource).Refresh();
        }


        private void SearchNomenclatureTBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            
            //CollectionViewSource.GetDefaultView(NomenclatureDG.ItemsSource).Refresh();
        }

        private void SelectNomenButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Properties.Settings.Default.NomenSelectProp);
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
    }
}
