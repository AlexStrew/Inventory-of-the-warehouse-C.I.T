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
    /// Логика взаимодействия для NomenclatureWindow.xaml
    /// </summary>
    public partial class NomenclatureWindow : Window
    {
        Core db = new Core();
        List<Nomenclature> nomList;
        public NomenclatureWindow()
        {
            InitializeComponent();
            
            nomList = db.context.Nomenclature.ToList();
            NomenclatureDG.ItemsSource = nomList;
        }

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
            nomList = db.context.Nomenclature.ToList();
            NomenclatureDG.ItemsSource = nomList;
        }
    }
}
