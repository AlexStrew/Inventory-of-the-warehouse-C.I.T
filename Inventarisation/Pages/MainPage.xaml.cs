using Inventarisation.Models;

using Inventarisation.Views;
using Microsoft.Win32;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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

        Core db = new Core();
        List<Inventory> invList;

      
        public MainPage()
        {
            
       
            
                InitializeComponent();
              
            


            invList = db.context.Inventory.ToList();
            sfDataGrid.ItemsSource = invList;

            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(InventoryDataGrid.ItemsSource);
            //view.Filter = UserFilter;
        }

        //private bool UserFilter(object item)
        //{
        //    //if (String.IsNullOrEmpty(SearchTBox.Text))
        //    //    return true;
        //    //else
        //    //    return ((item as Inventory).inv_num.IndexOf(SearchTBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        //}

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

            this.sfDataGrid.SearchHelper.Search(SearchTBox.Text);
            this.sfDataGrid.SearchHelper.AllowFiltering = true;
            this.sfDataGrid.SearchHelper.Search(SearchTBox.Text);

        }

        private void SaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            var excelEngine = sfDataGrid.ExportToExcel(sfDataGrid.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            SaveFileDialog sfd = new SaveFileDialog
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

                if (MessageBox.Show("Do you want to view the workbook?", "Workbook has been created",
                                    MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {

                    //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
        }

        private void GoPrint_Click(object sender, RoutedEventArgs e)
        {

            sfDataGrid.PrintSettings.AllowRepeatHeaders = false;
            sfDataGrid.ShowPrintPreview();
        }
    }
}
