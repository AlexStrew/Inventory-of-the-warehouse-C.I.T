using Inventarisation.Api.ApiModel;
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
    /// Логика взаимодействия для AddSubjectWindow.xaml
    /// </summary>
    public partial class AddSubjectWindow : Window
    {
        //Subject
        public ObservableCollection<Subjects> SubjectCollection { get; set; }
        public AddSubjectWindow()
        {
            InitializeComponent();
        }

        private async void AddSubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectTBox.Text != "" && SubjectTBox.Text != " ")
            {
                var subjects = new Subjects
                {
                    NameSubject = SubjectTBox.Text,
                    NomenId = Properties.Settings.Default.IdNomenSelectProp
                };

                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var protectedToken = Properties.Settings.Default.JWTtoken;
                var token = protectedToken;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (_client)
                {
                    using (var response = await _client.PostAsJsonAsync($"https://invent.doker.ru/api/Subjects", subjects))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Удаление выбранной строки из sfDataGrid

                            HandyControl.Controls.MessageBox.Show($"Добавлено");
                            DialogResult = true;
                            this.Close();
                        }
                        else
                        {
                            HandyControl.Controls.MessageBox.Show($"Сначала выберите категорию");
                        }


                    }
                }
            }
            else
            {
                HandyControl.Controls.MessageBox.Show($"Поле: Наименование не должно быть пустым");
            }

        }
    }
}
