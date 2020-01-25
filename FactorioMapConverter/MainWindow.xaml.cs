using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace FactorioMapConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TileData> tiles;
        private List<TileData> enabledTiles;
        private string imageFile;
        private string outputFile;

        public MainWindow()
        {
            InitializeComponent();
            Init();

            //PrintTileDatas();
        }

        private void PrintTileDatas()
        {
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, "Assets", "Tiles");
            var files = Directory.GetFiles(path);

            int number = 3;
            StringBuilder sb = new StringBuilder();

            foreach (var file in files)
            {
                var name = System.IO.Path.GetFileNameWithoutExtension(file);

                BitmapImage image = new BitmapImage(new Uri(file, UriKind.Absolute));

                FormatConvertedBitmap dec = new FormatConvertedBitmap();
                dec.BeginInit();
                dec.Source = image;
                dec.DestinationFormat = PixelFormats.Bgra32;
                dec.EndInit();

                int height = dec.PixelHeight;
                int width = dec.PixelWidth;
                int stride = width * 4;
                int pixels = height * width;
                var bytes = new byte[pixels * 4];
                dec.CopyPixels(bytes, stride, 0);


                int sumB = 0;
                int sumG = 0;
                int sumR = 0;

                for (int i = 0; i < bytes.Length; i += 4)
                {
                    sumB += bytes[i];
                    sumG += bytes[i + 1];
                    sumR += bytes[i + 2];
                }

                int b = sumB / pixels;
                int g = sumG / pixels;
                int r = sumR / pixels;

                sb.AppendLine($"name = \"{name}\", colorRGB = {r},{g},{b}");
                //sb.AppendLine($"[{number}] = \"{name}\",");
                //sb.AppendLine($"new TileData() {{name = \"{name}\", Number = {number}, color = Color.FromArgb(255,{r},{g},{b}), enabled = true}},");
                number++;
            }

            Debug.WriteLine(sb.ToString());

        }

        private void Init()
        {
            ProcessImageButton.IsEnabled = false;
            ProcessImageProgress.IsEnabled = false;

            tiles = TileData.Tiles;

            TilesView.ItemsSource = tiles;
        }

        private void GetImageFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() is true)
            {
                imageFile = dlg.FileName;
                ImageFilePathTextBlock.Text = imageFile;
            }

            ProcessImageButton.IsEnabled = CanProcessImage();
        }

        private string lastOutputFile = "";
        private void SetOutputFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = lastOutputFile;
            dlg.FileName = "map.lua";
            dlg.AddExtension = true;
            dlg.DefaultExt = ".lua";
            dlg.Filter = "lua file|*.lua";


            if (dlg.ShowDialog() is true)
            {
                outputFile = dlg.FileName;
                OutputFilePathTextBlock.Text = outputFile;
                Debug.WriteLine(System.IO.Path.GetDirectoryName(dlg.FileName));
                lastOutputFile = System.IO.Path.GetDirectoryName(dlg.FileName);
            }

            ProcessImageButton.IsEnabled = CanProcessImage();
        }

        private bool CanProcessImage()
        {
            return imageFile != null && outputFile != null;
        }

        private async void ProcessImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (imageFile == null)
            {
                Debug.WriteLine("image file not set");
                return;
            }

            if (outputFile == null)
            {
                Debug.WriteLine("output file not set");
                return;
            }

            ProcessImageProgress.Visibility = Visibility.Visible;
            ProcessImageProgress.IsIndeterminate = true;
            ProcessImageButton.IsEnabled = false;
            GetImageFileButton.IsEnabled = false;
            SetOutputFileButton.IsEnabled = false;
            Status.Visibility = Visibility.Collapsed;
            Status.Text = string.Empty;

            Func<int, int, int, int, TileData> tileGetter;
            if (BlackAndWhiteRadioButton.IsChecked is true)
                tileGetter = GetTrueOrFalseTile;
            else if (ColorRadioButton.IsChecked is true)
                tileGetter = GetTileNameOfColor;
            else
            {
                Debug.WriteLine("picture mode not selected");
                return;
            }

            enabledTiles = tiles.Where(x => x.Enabled).ToList();

            string data;
            try
            {
                data = await Task.Run(() => DecodeImageAndCompress(imageFile, tileGetter));

                using (FileStream SourceStream = File.Open(outputFile, FileMode.Create))
                {
                    SourceStream.Seek(0, SeekOrigin.Begin);
                    await SourceStream.WriteAsync(Encoding.ASCII.GetBytes(data), 0, data.Length);
                }

                Status.Text = "Conversion Performed";
            }
            catch (Exception exc)
            {
                MessageBox.Show(
                    $"Invalid operation upon selected file: \r\n{exc.Message}\r\n\r\n Is the selected file an image file?",
                    "Error during image decoding", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            finally
            {

                ProcessImageProgress.Visibility = Visibility.Collapsed;
                Status.Visibility = Visibility.Visible;
                ProcessImageProgress.IsIndeterminate = false;
                ProcessImageButton.IsEnabled = true;
                GetImageFileButton.IsEnabled = true;
                SetOutputFileButton.IsEnabled = true;
            }
        }

        private string DecodeImageAndCompress(string file, Func<int, int, int, int, TileData> tileGetter)
        {
            BitmapImage image = new BitmapImage(new Uri(file, UriKind.Absolute));

            FormatConvertedBitmap dec = new FormatConvertedBitmap();
            dec.BeginInit();
            dec.Source = image;
            dec.DestinationFormat = PixelFormats.Bgra32;
            dec.EndInit();

            int height = dec.PixelHeight;
            int width = dec.PixelWidth;
            int stride = width * 4;
            var bytes = new byte[height * width * 4];
            dec.CopyPixels(bytes, stride, 0);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("local b = require 'map_gen.shared.builders'");
            sb.AppendLine("return b.decompress({");
            sb.Append("height = ").Append(height).AppendLine(",");
            sb.Append("width = ").Append(width).AppendLine(",");
            sb.AppendLine("data = {");

            for (int i = 0; i < height; i++)
            {
                sb.Append("\t{");

                TileData prev = null;
                int count = 0;

                for (int j = 0; j < width; j++)
                {
                    int k = (i * width + j) * 4;
                    byte b = bytes[k];
                    byte g = bytes[k + 1];
                    byte r = bytes[k + 2];
                    byte a = bytes[k + 3];

                    var tile = tileGetter(r, g, b, a);

                    if (prev == null)
                    {
                        prev = tile;
                        count = 1;
                    }
                    else if (prev == tile)
                    {
                        count++;
                    }
                    else
                    {
                        sb.Append(prev.Number).Append(',').Append(count).Append(',');
                        prev = tile;
                        count = 1;
                    }
                }
                if (prev != null)
                {
                    sb.Append(prev.Number).Append(',').Append(count).Append(',');
                }

                sb.AppendLine("},");
            }

            sb.AppendLine("}");
            sb.Append("})");

            return sb.ToString();
        }

        private TileData GetTileNameOfColor(int r, int g, int b, int a)
        {
            // https://stackoverflow.com/questions/4057475/rounding-colour-values-to-the-nearest-of-a-small-set-of-colours

            int min = int.MaxValue;
            TileData minTile = null;
            foreach (var tile in enabledTiles)
            {
                var c = tile.Color;

                int rd = c.R - r;
                int gd = c.G - g;
                int bd = c.B - b;

                int dd = rd * rd + gd * gd + bd * bd;

                if (dd < min)
                {
                    min = dd;
                    minTile = tile;
                }
            }

            return minTile;
        }

        private (double x, double y, double z) ApplyHCL(int r, int g, int b)
        {
            double varR = r / 255.0;
            double varG = g / 255.0;
            double varB = b / 255.0;

            double varMin = Math.Min(Math.Min(varR, varG), varB);
            double varMax = Math.Max(Math.Max(varR, varG), varB);
            double C = varMax - varMin;

            double L = (varMax + varMin) - 1;

            double delR = (((varMax - varR) / 6) + (C / 2)) / C;
            double delG = (((varMax - varG) / 6) + (C / 2)) / C;
            double delB = (((varMax - varB) / 6) + (C / 2)) / C;

            double H;

            if (varR == varMax)
                H = delB - delG;
            else if (varG == varMax)
                H = (1 / 3) + delR - delB;
            else
                H = (2 / 3) + delG - delR;

            if (H < 0)
                H++;
            if (H > 1)
                H--;

            H = 2 * H;

            double x = C * Math.Cos(H * Math.PI);
            double y = C * Math.Sin(H * Math.PI);

            return (x, y, L);
        }

        private TileData GetTileNameOfColor2(int r, int g, int b, int a)
        {
            var target = ApplyHCL(r, g, b);

            double min = double.MaxValue;
            TileData minTile = null;
            foreach (var tile in enabledTiles)
            {
                var c = tile.Color;

                var tc = ApplyHCL(c.R, c.G, c.B);

                double xx = tc.x - target.x;
                double yy = tc.y - target.y;
                double zz = tc.z - target.z;

                double dd = xx * xx + yy * yy + zz * zz;

                if (dd < min)
                {
                    min = dd;
                    minTile = tile;
                }
            }

            return minTile;
        }

        private static bool IsBlack(int r, int g, int b, int a)
        {
            double y = 0.2126 * r + 0.7152 * g + 0.0722 * b;
            return y < 128;
        }

        private static TileData GetTrueOrFalseTile(int r, int b, int g, int a)
        {
            return IsBlack(r, g, b, a) ? TileData.Tiles[0] : TileData.Tiles[1];
        }
    }


}
