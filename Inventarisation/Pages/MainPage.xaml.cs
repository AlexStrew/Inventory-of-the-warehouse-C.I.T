using Inventarisation.Api.ApiModel;
using Inventarisation.Models;
using Inventarisation.ViewModel;
using Inventarisation.Views;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Win32;
using Newtonsoft.Json;
using Syncfusion.SfSkinManager;
using Syncfusion.UI.Xaml.Controls.DataPager;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ContextMenu = System.Windows.Controls.ContextMenu;
using MenuItem = System.Windows.Controls.MenuItem;

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
     
            UserTextBlock.Text = Properties.Settings.Default.CurrentUser;
            DataContext = this;
            AwaitDataLoad();


        }



        private async  Task AwaitDataLoad()
        {
            BusyBar.IsBusy = true;
            await LoadData();
            BusyBar.IsBusy = false;
        }

        //private async Task LoadData(int _pageNumber, int _pageSize)
        //{
        //    //var _client = new HttpClient();
        //    //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    //var protectedToken = Properties.Settings.Default.JWTtoken;
        //    //var token = protectedToken;
        //    //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    //using (var response = await _client.GetAsync("https://localhost:7050/api/Inventories/test1?pageNumber=1&pageSize=10"))
        //    //{
        //    //    if (response.IsSuccessStatusCode)
        //    //    {
        //    //        string apiResponse = await response.Content.ReadAsStringAsync();
        //    //        DamaskCollection = JsonConvert.DeserializeObject<ObservableCollection<InvMain>>(apiResponse);
        //    //        sfDataGrid.ItemsSource = DamaskCollection;
        //    //    }
        //    //    else
        //    //    {
        //    //        string errorResponse = await response.Content.ReadAsStringAsync();
        //    //        throw new Exception($"Error getting data from API. Status code: {response.StatusCode}. Error message: {errorResponse}");
        //    //    }
        //    //}

        //    BusyBar.IsBusy = true;
        //    var _client = new HttpClient();
        //    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    var protectedToken = Properties.Settings.Default.JWTtoken;
        //    var token = protectedToken;
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    using (_client)
        //    {
        //        using (var response = await _client.GetAsync($"https://invent.doker.ru/api/Inventories/test1?pageNumber={_pageNumber}&pageSize={_pageSize}"))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            var result = JsonConvert.DeserializeObject<IEnumerable<InventoryModel>>(apiResponse);
        //            sfDataGrid.ItemsSource = result;
        //            sfDataPager.PageSize = _pageSize;
        //            sfDataPager.PageCount = result.FirstOrDefault()?.Id ?? 0;
        //        }
        //    }
        //    BusyBar.IsBusy = false;
        //}

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
                    sfDataGrid.ItemsSource =  DamaskCollection;
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
            this.sfDataGrid.SearchHelper.Search(SearchTBox.Text);
        }

        private void QueueButton_Click(object sender, RoutedEventArgs e)
        {
            QueueWindow win = new QueueWindow();
            if (win.ShowDialog() == true)
            {
                Console.WriteLine("ok");
            }
        }
    }
}
