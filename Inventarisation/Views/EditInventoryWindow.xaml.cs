using HandyControl.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Inventarisation.Api.ApiModel;
using Newtonsoft.Json;

namespace Inventarisation.Views
{
    /// <summary>
    /// Логика взаимодействия для EditInventoryWindow.xaml
    /// </summary>
    public partial class EditInventoryWindow : Window
    {
        public EditInventoryWindow()
        {
            InitializeComponent();
            ConfigHelper.Instance.SetLang("ru-ru");
            NameDeviceTB.Text = Properties.Settings.Default.NomenSelectProp;
            CompanyNameCB.Text = Properties.Settings.Default.CompanySelectProp;
            PaymentNumTB.Text = Properties.Settings.Default.PaymentSelectProp.ToString();
            CommentTB.Text = Properties.Settings.Default.CommentSelectProp;
            InvoiceTB.Text = Properties.Settings.Default.InvoiceSelectProp;

            PlacementTBox.Text = Properties.Settings.Default.PlacementSelectProp ;
        }

        private void SelectEmployerButton_Click(object sender, RoutedEventArgs e)
        {
            EmployerWindow win = new EmployerWindow();
            if (win.ShowDialog() == true)
            {

                Console.WriteLine("sdsd");
            }
            EmployerTBox.Text = Properties.Settings.Default.EmployerSelectProp;
        }
        /// <summary>
        /// Открытие окна с нуменклатурой
        /// </summary>
        private void SelectNumButtonClick(object sender, RoutedEventArgs e)
        {
            SubjectWindow win = new SubjectWindow();
            if (win.ShowDialog() == true)
            {

                Console.WriteLine("sdsd");
            }
            NameDeviceTB.Text = Properties.Settings.Default.SubjectSelectProp;

        }

        private void AddCompanyBtnOnClick(object sender, RoutedEventArgs e)
        {
            CompanyWindow addWinNom = new CompanyWindow();
            if (addWinNom.ShowDialog() == true)
            {
                Console.WriteLine("hehe");

            }
            CompanyNameCB.Text = Properties.Settings.Default.CompanySelectProp;
        }
        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            // Проверяем, что вводимый символ - цифра
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsNumeric(string text)
        {
            Regex regex = new Regex("[^0-9]+"); // Определяем регулярное выражение для поиска всех символов, кроме цифр
            return !regex.IsMatch(text); // Возвращаем true, если текст содержит только цифры
        }

        private void SelectPlaceButton_Click(object sender, RoutedEventArgs e)
        {
            PlacementWindow win = new PlacementWindow();
            if (win.ShowDialog() == true)
            {

                Console.WriteLine("sdsd");
            }
            PlacementTBox.Text = Properties.Settings.Default.PlacementSelectProp;
        }




        private async void SaveInvBtnOnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PlacementTBox.Text) || string.IsNullOrWhiteSpace(EmployerTBox.Text) || string.IsNullOrWhiteSpace(NameDeviceTB.Text) || string.IsNullOrWhiteSpace(CompanyNameCB.Text) || string.IsNullOrWhiteSpace(PaymentNumTB.Text) || string.IsNullOrWhiteSpace(InvoiceTB.Text) || string.IsNullOrWhiteSpace(CommentTB.Text))
            {
                HandyControl.Controls.MessageBox.Show($"Поле: Наименование не должно быть пустым");
                return;
            }
            try
            {
                var inventory = new Inventory()
                {
                    Id = Properties.Settings.Default.IdInventorySelectedProp,
                    SubjectId = Properties.Settings.Default.IdSubjectSelectProp,
                    CompanyId = Properties.Settings.Default.IdCompanySelectProp,
                    PaymentNum = PaymentNumTB.Text,
                    Invoice = InvoiceTB.Text,
                    Comment = CommentTB.Text
                };

                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var protectedToken = Properties.Settings.Default.JWTtoken;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", protectedToken);
                var json = JsonConvert.SerializeObject(inventory);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (_client)
                {
                    using (var response = await _client.PutAsync($"https://invent.doker.ru/api/Inventories/{Properties.Settings.Default.IdInventorySelectedProp}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            HandyControl.Controls.MessageBox.Show("da");
                        }
                        else
                        {
                            HandyControl.Controls.MessageBox.Show("net");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               HandyControl.Controls.MessageBox.Show(ex.Message);
            }

        }
    }
}
