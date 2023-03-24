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
            NomenclatureWindow win = new NomenclatureWindow();
            if (win.ShowDialog() == true)
            {

                Console.WriteLine("sdsd");
            }
            NameDeviceTB.Text = Properties.Settings.Default.NomenSelectProp;

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

            var client = new HttpClient();
            var protectedToken = Properties.Settings.Default.JWTtoken;
            var token = protectedToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Создание объекта Company для редактирования
            var editedCompany = new Inventory
            {
                Id = Properties.Settings.Default.IdInventorySelectedProp, // сохраняем Id редактируемой компании
                //CompanyId  // новое значение для поля NameCompany,

            };

            // Преобразуем объект editedCompany в JSON
            var editedCompanyJson = JsonConvert.SerializeObject(editedCompany);

            // Отправка PUT запроса на API
            var httpContent = new StringContent(editedCompanyJson, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://invent.doker.ru/api/Companies/{Properties.Settings.Default.IdCompanySelectProp}", httpContent);

            // Если ответ успешный
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Компания успешно отредактирована.");
                Properties.Settings.Default.IdCompanySelectProp = 0;
                Properties.Settings.Default.CompanySelectProp = "";
                Properties.Settings.Default.Save();
                Close();
            }
            else
            {
                MessageBox.Show("Не удалось отредактировать компанию. Попробуйте позже или обратитесь к администратору.");
            }

        }
    }
}
