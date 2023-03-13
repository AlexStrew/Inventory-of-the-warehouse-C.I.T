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

using static System.Net.Mime.MediaTypeNames;
using QRCoder;
using System.Drawing;
using System.IO;

namespace Inventarisation.Views
{
    /// <summary>
    /// Логика взаимодействия для QRAuthWindow.xaml
    /// </summary>
    public partial class QRWindow : Window
    {
        public QRWindow()
        {
            InitializeComponent();
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode("AdminApi QFgQJkWi4t", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qRCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(25);
            qr.Source = BitmapToImageSource(qrCodeImage);

        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
            
        //}
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
    }
}