using Inventarisation.Api.ApiModel;
using Inventarisation.Models;

using Inventarisation.Views;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Win32;
using Newtonsoft.Json;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Inventarisation.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        //private readonly HttpClient _client;
        //private readonly IDataProtector _protector;
        public ObservableCollection<Inventory> Data { get; set; }

        public MainPage()
        {
           



                InitializeComponent();

            DataContext = this;
            
            GetData();

            //this.DataContext = new MainPageViewModel();


            //invList = db.context.Inventory.ToList();
            //sfDataGrid.ItemsSource = invList;

            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(InventoryDataGrid.ItemsSource);
            //view.Filter = UserFilter;
        }

        private async void GetData()
        {
            HttpClient _client;
            IDataProtector _protector;
            Data = new ObservableCollection<Inventory>();

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _protector = DataProtectionProvider.Create("Contoso").CreateProtector("JWT");

            var protectedToken = Properties.Settings.Default.JWTtoken;
            await Console.Out.WriteLineAsync(protectedToken);
            var token = protectedToken;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("http://invent.doker.ru/api/Inventories");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Inventory>>(json);
                // Bind the data to the datagrid
                //sfDataGrid.ItemsSource = data;
                foreach (var item in data)
                {
                    Data.Add(item);
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("errororororororor");
            }
        }


        private void AddButtonWindows_Click(object sender, RoutedEventArgs e)
        {
            AddWindow win = new AddWindow();
            if (win.ShowDialog() == true)
            {
                //TestText.Text = Properties.Settings.Default.MKBCode;
                //Console.WriteLine("--" + Properties.Settings.Default.MKBCode + "--");
                Console.WriteLine("sdsd");
            }
        }

        private void SearchTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CollectionViewSource.GetDefaultView(InventoryDataGrid.ItemsSource).Refresh();

            //this.sfDataGrid.SearchHelper.Search(SearchTBox.Text);
            //this.sfDataGrid.SearchHelper.AllowFiltering = true;
            //this.sfDataGrid.SearchHelper.Search(SearchTBox.Text);

        }

        private void SaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            //var options = new ExcelExportingOptions();
            //options.ExcelVersion = ExcelVersion.Excel2013;
            //var excelEngine = sfDataGrid.ExportToExcel(sfDataGrid.View, options);
            //var workBook = excelEngine.Excel.Workbooks[0];

            //SaveFileDialog sfd = new SaveFileDialog
            //{
            //    FilterIndex = 2,
            //    Filter = "Excel 97 to 2003 Files(*.xls)|*.xls|Excel 2007 to 2010 Files(*.xlsx)|*.xlsx|Excel 2013 File(*.xlsx)|*.xlsx"
            //};

            //if (sfd.ShowDialog() == true)
            //{
            //    using (Stream stream = sfd.OpenFile())
            //    {

            //        if (sfd.FilterIndex == 1)
            //            workBook.Version = ExcelVersion.Excel97to2003;

            //        else if (sfd.FilterIndex == 2)
            //            workBook.Version = ExcelVersion.Excel2010;

            //        else
            //            workBook.Version = ExcelVersion.Excel2013;
            //        workBook.SaveAs(stream);
            //    }

            //    //Message box confirmation to view the created workbook.

            //    if (MessageBox.Show("Do you want to view the workbook?", "Workbook has been created",
            //                        MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            //    {

            //        //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
            //        System.Diagnostics.Process.Start(sfd.FileName);
            //    }
            //}
        }

        private void GoPrint_Click(object sender, RoutedEventArgs e)
        {

            //sfDataGrid.PrintSettings.AllowRepeatHeaders = false;
            //sfDataGrid.ShowPrintPreview();
        }

        private async void TestButton_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {


                var response = await client.GetAsync("http://localhost:5099/api/Inventories");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(message);
                }
            }
        }

       
    }
}
