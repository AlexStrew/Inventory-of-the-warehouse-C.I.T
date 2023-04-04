using HandyControl.Controls;
using Inventarisation.Api.ApiModel;
using Newtonsoft.Json;
using Syncfusion.UI.Xaml.ProgressBar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Window = System.Windows.Window;

namespace Inventarisation.Views
{
    /// <summary>
    /// Логика взаимодействия для HistoryMovements.xaml
    /// </summary>
    public partial class HistoryMovements : Window
    {
        public ObservableCollection<HistoryMove> DataMoveInv { get; set; }
        public  HistoryMovements()
        {
            InitializeComponent();

            //Properties.Settings.Default.IdInventorySelectedProp = selectedItem.Id;
            //Properties.Settings.Default.InvNumForPrint = selectedItem.InvNum;
            //Properties.Settings.Default.IdPlacementSelectProp = selectedItem.IdPlacement;
            //Properties.Settings.Default.PlacementSelectProp = selectedItem.NamePlacement;
            //Properties.Settings.Default.IdEmployerSelectProp = selectedItem.IdEmployer;
            //Properties.Settings.Default.EmployerSelectProp = selectedItem.FullName;


            NameDeviceTBox.Text = Properties.Settings.Default.SubjectSelectProp;
            CompanyTBox.Text = Properties.Settings.Default.CompanySelectProp;
            CommentTBox.Text = Properties.Settings.Default.CommentSelectedProp;
            SerialNumberTBox.Text = Properties.Settings.Default.SerialNumberSelectedProp;
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
                using (var response = await _client.GetAsync($"https://invent.doker.ru/api/Movements/gethistory/{Properties.Settings.Default.IdInventorySelectedProp}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataMoveInv = JsonConvert.DeserializeObject<ObservableCollection<HistoryMove>>(apiResponse);
                    MoveDG.ItemsSource = DataMoveInv;
                }
            }

        }

     

        private void RefreshWinBtn_Click(object sender, RoutedEventArgs e)
        {
         
            LoadData();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void SearchPlaceBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MoveDG.SearchHelper.AllowFiltering = true;
            this.MoveDG.SearchHelper.Search(SearchPlacementTBox.Text);
        }

        
        private void AddPlaceWinBtn_Click(object sender, RoutedEventArgs e)
        {
            MoveInventoryWindow addWinNom = new MoveInventoryWindow();
            if (addWinNom.ShowDialog() == true)
            {
                Console.WriteLine("hehe");
            }
            LoadData();
        }
    }
}
