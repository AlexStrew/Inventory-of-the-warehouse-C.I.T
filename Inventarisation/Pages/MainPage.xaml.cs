using Inventarisation.Api.ApiModel;
using Inventarisation.Views;
using Newtonsoft.Json;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Inventarisation.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private int _pageNumber = 1;
        private int _pageSize = 20;
        public ObservableCollection<InvMain> DamaskCollection { get; set; }
        public MainPage()
        {

            InitializeComponent();
            AwaitDataLoad();
            UserTextBlock.Text = Properties.Settings.Default.CurrentUser;            
        }

        private async Task AwaitDataLoad()
        {
            BusyBar.IsBusy = true;

            await LoadData();
            await LoadData();
            //DataContext = this;
            BusyBar.IsBusy = false;
        }

        private async Task LoadData()
        {
            BusyBar.IsBusy = true;
            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var protectedToken = Properties.Settings.Default.JWTtoken;
            var token = protectedToken;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using (_client)
            {
                using (var response = await _client.GetAsync($"https://invent.doker.ru/api/Inventories/ConnectedTables"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DamaskCollection = JsonConvert.DeserializeObject<ObservableCollection<InvMain>>(apiResponse);
                    sfDataGrid.ItemsSource = DamaskCollection;
                }
            }
            BusyBar.IsBusy = false;
        }


        private void AddButtonWindows_Click(object sender, RoutedEventArgs e)
        {
            AddWindow win = new AddWindow();
            if (win.ShowDialog() == true)
            {
                Console.WriteLine("sdsd");
            }
        }
        /// <summary>
        /// Поиск в DataGrid по TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CollectionViewSource.GetDefaultView(sfDataGrid.ItemsSource).Refresh();

            //this.sfDataGrid.SearchHelper.Search(SearchTBox.Text);
            //this.sfDataGrid.SearchHelper.AllowFiltering = true;
            //this.sfDataGrid.SearchHelper.Search(SearchTBox.Text);



        }

        /// <summary>
        /// Сохранение в Excel книгу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            var excelEngine = sfDataGrid.ExportToExcel(sfDataGrid.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog
            {
                FilterIndex = 2,
                Filter = "Excel 97 to 2003 Files(*.xls)|*.xls|Excel 2007 to 2010 Files(*.xlsx)|*.xlsx|Excel 2013 File(*.xlsx)|*.xlsx"
            };

            if (sfd.ShowDialog() == true)
            {
                using (Stream stream = sfd.OpenFile())
                {

                    if (sfd.FilterIndex == 1)
                        workBook.Version = ExcelVersion.Excel97to2003;

                    else if (sfd.FilterIndex == 2)
                        workBook.Version = ExcelVersion.Excel2010;

                    else
                        workBook.Version = ExcelVersion.Excel2013;
                    workBook.SaveAs(stream);
                }

                //Message box confirmation to view the created workbook.

                if (System.Windows.MessageBox.Show("Хотите открыть файл?", "Книга создана",
                                    MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {

                    //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
        }

        /// <summary>
        /// Отправка в печать с превью
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoPrint_Click(object sender, RoutedEventArgs e)
        {

            sfDataGrid.PrintSettings.AllowRepeatHeaders = false;
            sfDataGrid.ShowPrintPreview();
        }

        private void CheckUserIdentity_Click(object sender, RoutedEventArgs e)
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity != null)
            {
                Console.WriteLine("Name: " + identity.Name);
                Console.WriteLine("Authentication type: " + identity.AuthenticationType);
                Console.WriteLine("Is authenticated: " + identity.IsAuthenticated);
                Console.WriteLine("Token: " + identity.Token);
                Console.WriteLine("Groups: " + identity.Groups);
                Console.WriteLine("ImpersonationLevel: " + identity.ImpersonationLevel);
                Console.WriteLine("AccessToken: " + identity.AccessToken);
                Console.WriteLine("Actor: " + identity.Actor);
                Console.WriteLine("DeviceClaims: " + identity.DeviceClaims);
                Console.WriteLine("Owner: " + identity.Owner);


            }
        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.sfDataGrid.SearchHelper.ClearSearch();
        }



        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.sfDataGrid.SearchHelper.AllowFiltering = true;
            this.sfDataGrid.SearchHelper.Search(SearchTextBox.Text);
        }

        private void QueueButton_Click(object sender, RoutedEventArgs e)
        {
            QueueWindow win = new QueueWindow();
            if (win.ShowDialog() == true)
            {
                Console.WriteLine("ok");
            }
        }





        private void PrintInvNumQR_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button selectedButton = (System.Windows.Controls.Button)sender;
            InvMain item = selectedButton.DataContext as InvMain;
            Properties.Settings.Default.InvNumForPrint = item.InvNum;
            Properties.Settings.Default.Save();

            InvNumQrPrintWindow win = new InvNumQrPrintWindow();
            if (win.ShowDialog() == true)
            {
                Console.WriteLine("ok");
            }
        }

        private void EditButtonWindows_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedRow = sfDataGrid.SelectedItem as InvMain;

            if (selectedRow != null)
            {
                MessageBoxResult result = HandyControl.Controls.MessageBox.Show("Вы действительно хотите редактировать строку?", "Редактирование", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Properties.Settings.Default.IdInventorySelectedProp = selectedRow.Id;
                    Properties.Settings.Default.NomenSelectProp = selectedRow.NameDevice;
                    Properties.Settings.Default.CompanySelectProp = selectedRow.CompanyName;
                    Properties.Settings.Default.PlacementSelectProp = selectedRow.NamePlacement;

                    Properties.Settings.Default.PaymentSelectProp = selectedRow.PaymentNum;
                    Properties.Settings.Default.CommentSelectProp = selectedRow.Comment;
                    Properties.Settings.Default.InvoiceSelectProp = selectedRow.Invoice;

                    Properties.Settings.Default.Save();

                    EditInventoryWindow win = new EditInventoryWindow();
                    if (win.ShowDialog() == true)
                    {
                        Console.WriteLine("ok");
                    }
                    AwaitDataLoad();
                }
            }
        }

        private async void DeleteButtonWindows_Click(object sender, RoutedEventArgs e)
        {
         
                var selectedRow = sfDataGrid.SelectedItem as Inventory;

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
                    var response = await client.DeleteAsync($"https://invent.doker.ru/api/Inventories/{selectedRow.Id}");

                    // Если ответ успешный
                    if (response.IsSuccessStatusCode)
                    {
                        // Удаление выбранной строки из sfDataGrid
                        var inventList = sfDataGrid.ItemsSource as ObservableCollection<Inventory>;
                        inventList.Remove(selectedRow);
                        sfDataGrid.ItemsSource = inventList;
                        HandyControl.Controls.MessageBox.Show($"Удалено");
                    }
                    else
                    {
                        HandyControl.Controls.MessageBox.Show($"Произошла ошибка при удалении: {response.ReasonPhrase}");
                    }
                }

            }
        }
    }
}
