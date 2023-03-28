using Inventarisation.Api.ApiModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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
    /// Логика взаимодействия для SubjectWindow.xaml
    /// </summary>
    public partial class SubjectWindow : Window
    {
        public ObservableCollection<Subjects> DataSubject { get; set; }
        public SubjectWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private async Task LoadData()
        {

            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var protectedToken = Properties.Settings.Default.JWTtoken;
            var token = protectedToken;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using (_client)
            {
                using (var response = await _client.GetAsync($"https://invent.doker.ru/api/Subjects/getSorted/{Properties.Settings.Default.IdNomenSelectProp}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataSubject = JsonConvert.DeserializeObject<ObservableCollection<Subjects>>(apiResponse);
                    SubjectDG.ItemsSource = DataSubject;
                }
            }

        }

        public static implicit operator SubjectWindow(AddWindow v)
        {
            throw new NotImplementedException();
        }

        private void AddSubjectWinBtn_Click(object sender, RoutedEventArgs e)
        {
            AddSubjectWindow addWinNom = new AddSubjectWindow();
            if (addWinNom.ShowDialog() == true)
            {
                Console.WriteLine("hehe");

            }
            LoadData();
        }

        private void EditSubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = SubjectDG.SelectedItem as Subjects;
            if (selectedItem != null)
            {
                MessageBoxResult result = HandyControl.Controls.MessageBox.Show("Вы действительно хотите изменить данные?", "Редактирование", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Properties.Settings.Default.SubjectSelectProp = selectedItem.NameSubject;
                    Properties.Settings.Default.IdSubjectSelectProp = selectedItem.IdSubject;
                    Properties.Settings.Default.Save();

                    EditSubjectWindow addWinNom = new EditSubjectWindow();
                    if (addWinNom.ShowDialog() == true)
                    {
                        Console.WriteLine("hehe");

                    }
                    LoadData();
                }
                else
                {
                    HandyControl.Controls.MessageBox.Show("Строка не выбрана", "Редактирование", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private async void DelSubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = SubjectDG.SelectedItem as Subjects;

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
                    var response = await client.DeleteAsync($"https://invent.doker.ru/api/Subjects/{selectedRow.IdSubject}");

                    // Если ответ успешный
                    if (response.IsSuccessStatusCode)
                    {
                        // Удаление выбранной строки из sfDataGrid
                        var subjectList = SubjectDG.ItemsSource as ObservableCollection<Subjects>;
                        subjectList.Remove(selectedRow);
                        SubjectDG.ItemsSource = subjectList;
                        HandyControl.Controls.MessageBox.Show($"Удалено");
                    }
                    else
                    {
                        HandyControl.Controls.MessageBox.Show($"Произошла ошибка при удалении: {response.ReasonPhrase}");
                    }
                }
            }
            else
            {
                HandyControl.Controls.MessageBox.Show("Строка не выбрана", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RefreshWinBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SelectSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = SubjectDG.SelectedItem as Subjects;

            // Если строка выбрана, сохраняем ее в Properties.Settings.Default.NomenSelectProp
            if (selectedItem != null)
            {
                Properties.Settings.Default.SubjectSelectProp = selectedItem.NameSubject;
                Properties.Settings.Default.IdSubjectSelectProp = selectedItem.IdSubject;
                Properties.Settings.Default.Save();
            }

            DialogResult = true;
            this.Close();
        }

        private void SearchSubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            this.SubjectDG.SearchHelper.AllowFiltering = true;
            this.SubjectDG.SearchHelper.Search(SearchSubjectTBox.Text);
        }

        private void SubjectDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            var selectedItem = SubjectDG.SelectedItem as Subjects;

            // Если строка выбрана, сохраняем ее в Properties.Settings.Default.NomenSelectProp
            if (selectedItem != null)
            {
                Properties.Settings.Default.SubjectSelectProp = selectedItem.NameSubject;
                Properties.Settings.Default.IdSubjectSelectProp = selectedItem.IdSubject;
                Properties.Settings.Default.Save();
            }

            DialogResult = true;
            this.Close();
        }
    }
}
