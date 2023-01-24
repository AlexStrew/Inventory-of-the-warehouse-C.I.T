using HandyControl.Tools;
using Inventarisation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        Core db = new Core();
        List<Companies> listCompanies;
        public AddWindow()
        {
            InitializeComponent();
            ConfigHelper.Instance.SetLang("ru-ru");
            

            listCompanies = db.context.Companies.ToList();
            CompanyNameCB.ItemsSource = listCompanies;
            CompanyNameCB.DisplayMemberPath = "company_name";
            CompanyNameCB.SelectedValuePath = "id_company";


        }

        /// <summary>
        /// Открытие окна с нуменклатурой
        /// </summary>
        private void SelectNumButtonClick(object sender, RoutedEventArgs e)
        {
            NomenclatureWindow win = new NomenclatureWindow();
            if (win.ShowDialog() == true)
            {
                //TestText.Text = Properties.Settings.Default.MKBCode;
                //Console.WriteLine("--" + Properties.Settings.Default.MKBCode + "--");
                Console.WriteLine("sdsd");
            }


        }
    }
}
