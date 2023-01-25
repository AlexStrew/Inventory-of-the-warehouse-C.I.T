using Inventarisation.Models;
using Inventarisation.Pages;
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
    /// Логика взаимодействия для AddNomeclatureWindow.xaml
    /// </summary>
    public partial class AddNomeclatureWindow : Window
    {
        Core db = new Core();
       // List<Nomenclature> listNomenclature;
        public AddNomeclatureWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Добавление устройства в справочник
        /// </summary>
        private void AddNomenBtnClick(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    string nameDevice = NameDeviceTBox.Text;
            //    NomenclatureVM newObject = new NomenclatureVM();
            //    bool result = newObject.CheckAddNomenclature(nameDevice);
            //    if (result)
            //    {
            //        newObject.AddNomenclature(nameDevice);
            //        MessageBox.Show("Вы успешно добавили устройство в справочник.\nНе забудьте обновить страницу.");
            //        Close();
            //        //NomenclatureWindow winNom = new NomenclatureWindow();
            //        //if (winNom.ShowDialog() == true)
            //        //{
            //        //    Console.WriteLine("hehe");
                       
            //        //}
                    
                   
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
           
        }
    }
}
