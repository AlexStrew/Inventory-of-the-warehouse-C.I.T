using Inventarisation.Api.ApiModel;
using Newtonsoft.Json;
using Syncfusion.UI.Xaml.Grid;
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
    /// Логика взаимодействия для QueueWindow.xaml
    /// </summary>
    public partial class QueueWindow : Window
    {
        public ObservableCollection<QueueModel> QueueCollection { get; set; }

        public QueueWindow()
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
                using (var response = await _client.GetAsync($"https://invent.doker.ru/api/QueueLists"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    QueueCollection = JsonConvert.DeserializeObject<ObservableCollection<QueueModel>>(apiResponse);
                    OpenQueues.ItemsSource = QueueCollection.Where(x => x.IsActive == true);
                    ClosedQueues.ItemsSource = QueueCollection.Where(x => x.IsActive == false);
                }
            }

        }

        private void DelQueueBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshWinBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectQueueButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = OpenQueues.SelectedItem as QueueModel;
            if (selectedItem != null)
            {
                Properties.Settings.Default.IdQueueSelectProp = selectedItem.IdList;                
                Properties.Settings.Default.Save();

                ViewSelectRevisionItemWindow addWinNom = new ViewSelectRevisionItemWindow();
                if (addWinNom.ShowDialog() == true)
                {
                    Console.WriteLine("hehe");

                }

                LoadData();
            }
            else
            {
                HandyControl.Controls.MessageBox.Show("Строка не выбрана", "Просмотр", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            }

        }
    

        private void SearchQueueBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
