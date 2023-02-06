using Inventarisation.Api.ApiModel;
using Inventarisation.Models;
using Inventarisation.ViewModel;
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

namespace Inventarisation.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        //public ObservableCollection<Inventory> Data { get; set; }
       
        public MainPage()
        {          
            InitializeComponent();

            UserTextBlock.Text = Properties.Settings.Default.CurrentUser;
            var vm = new InventoriesViewModel();
            vm.GetData();
            this.DataContext = vm;            
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
            SearchTBox.Text = String.Empty;
            //CollectionViewSource.GetDefaultView(sfDataGrid.ItemsSource).Refresh();
            this.sfDataGrid.SearchHelper.Search(SearchTBox.Text);
        }

 

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.sfDataGrid.SearchHelper.AllowFiltering = true;
            this.sfDataGrid.SearchHelper.Search(SearchTBox.Text);
        }
    }
}
