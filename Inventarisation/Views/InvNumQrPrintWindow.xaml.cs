using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Printing;
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
using static System.Net.Mime.MediaTypeNames;

namespace Inventarisation.Views
{
    /// <summary>
    /// Логика взаимодействия для InvNumQrPrintWindow.xaml
    /// </summary>
    public partial class InvNumQrPrintWindow : Window
    {
        public InvNumQrPrintWindow()
        {
            InitializeComponent();
            InvNumTBox.Text = Properties.Settings.Default.InvNumForPrint;
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(InvNumTBox.Text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qRCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(25);
            InvNumQR.Source = BitmapToImageSource(qrCodeImage);
        }

        private ImageSource BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void PrintCodeButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // Создаем объект Image с изображением, которое мы хотим напечатать
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                image.Source = InvNumQR.Source;

                // Устанавливаем размер изображения для печати на основе размера страницы принтера
                image.Width = 100;
                image.Height = 100;

                // Создаем объект FlowDocument и добавляем в него изображение
                FlowDocument document = new FlowDocument(new BlockUIContainer(image));

                // Отображаем диалоговое окно предварительного просмотра печати
                IDocumentPaginatorSource paginatorSource = document;
                printDialog.PrintDocument(paginatorSource.DocumentPaginator, "InvNumQrPrint Preview");

                // Печатаем документ на принтере
                printDialog.PrintVisual(image, "InvNumQrPrint");
            }
        }
    }
}
