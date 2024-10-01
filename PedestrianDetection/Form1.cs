using Accord.Imaging;
using Accord.Imaging.Filters;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PedestrianDetection
{
    public partial class Form1 : Form
    {
        private string initialDirectory;
        private List<string> selectedImagePaths;
        private bool isLoadImagesEnabled = true;
        private bool isRecognizeEnabled;

        public Form1()
        {
            InitializeComponent();
        }

        private void RecognitionForm_Load(object sender, EventArgs e)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            initialDirectory = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\..\..\"));

            btnLoadImages.Enabled = isLoadImagesEnabled;
            btnRecognize.Enabled = isRecognizeEnabled;
        }

        private void BtnLoadImages_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = initialDirectory;
                openFileDialog.Filter = "PNG files|*.png";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePaths = openFileDialog.FileNames.ToList();
                    DisplayImage(selectedImagePaths.First());
                }
                else
                {
                    return;
                }
            }

            isLoadImagesEnabled = false;
            UpdateButtons();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            isLoadImagesEnabled = true;
            isRecognizeEnabled = false;
            selectedImagePaths = null;
            pictureBoxBefore.Image = null;
            pictureBoxAfter.Image = null;

            UpdateButtons();
        }

        private async void BtnRecognize_Click(object sender, EventArgs e)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = selectedImagePaths.Count();
            progressBar.Value = 0;

            var progress = new Progress<int>(value =>
            {
                progressBar.Value = value;
            });

            var mlContext = new MLContext();
            var hog = new HistogramsOfOrientedGradients();
            var grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);

            await Task.Run(() =>
            {
                int progressValue = 0;
                foreach (var imagePath in selectedImagePaths)
                {
                    var fileNum = Convert.ToInt32(Path.GetFileNameWithoutExtension(imagePath));
                    var originalImage = new Bitmap(imagePath);
                    var regions = SplitImageIntoRegions(originalImage, 80, 40);

                    foreach (var region in regions)
                    {
                        var regionImage = originalImage.Clone(region, originalImage.PixelFormat);
                        var grayTestImage = grayscaleFilter.Apply(regionImage);

                        var loadedModel = mlContext.Model.Load($"{initialDirectory}\\model.mdl", out _);
                        var predictionEngine = mlContext.Model.CreatePredictionEngine<TrainData, ImagePrediction>(loadedModel);

                        var imageData = new TrainData
                        {
                            Hog = hog.Transform(regionImage)
                            .SelectMany(x => x.Descriptor).Select(x => (float)x).ToArray()
                        };

                        var prediction = predictionEngine.Predict(imageData);

                        if (prediction.PredictedLabel)
                        {
                            using (var g = Graphics.FromImage(originalImage))
                            {
                                var cropRectangle = new Rectangle(region.X, region.Y, region.Width, region.Height);
                                g.DrawRectangle(new Pen(Color.Green, 3), cropRectangle);
                            }
                        }
                    }

                    originalImage.Save(initialDirectory + $"Result\\{fileNum}.png");

                    DisplayImageAfterRecognition(initialDirectory + $"Result\\{fileNum}.png");

                    progressValue++;
                    ((IProgress<int>)progress).Report(progressValue);
                }

                MessageBox.Show("Распознавание окончено");
            });
        }

        private List<Rectangle> SplitImageIntoRegions(Bitmap image, int regionWidth, int step)
        {
            var regions = new List<Rectangle>();

            int imageWidth = image.Width;
            int imageHeight = image.Height;

            for (int x = 0; x < imageWidth; x += step)
            {
                if (x + regionWidth > imageWidth)
                    x = imageWidth - regionWidth;

                var cropRectangle = new Rectangle(x, 0, regionWidth, imageHeight);
                regions.Add(cropRectangle);

                if (x + regionWidth >= imageWidth)
                    break;
            }

            return regions;
        }

        private void UpdateButtons()
        {
            if (!isLoadImagesEnabled)
                isRecognizeEnabled = true;

            btnLoadImages.Enabled = isLoadImagesEnabled;
            btnRecognize.Enabled = isRecognizeEnabled;
        }

        private void DisplayImage(string imagePath)
        {
            pictureBoxBefore.Image = System.Drawing.Image.FromFile(imagePath);
        }

        private async void DisplayImageAfterRecognition(string imagePath)
        {
            try
            {
                if (File.Exists(imagePath))
                {
                    var image = await Task.Run(() => System.Drawing.Image.FromFile(imagePath));

                    if (pictureBoxAfter.InvokeRequired)
                    {
                        pictureBoxAfter.Invoke(new Action(() => pictureBoxAfter.Image = image));
                    }
                    else
                    {
                        pictureBoxAfter.Image = image;
                    }
                }
                else
                {
                    MessageBox.Show($"Файл не найден: {imagePath}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отображении изображения: {ex.Message}");
            }
        }


        public class ImagePrediction
        {
            public float Score { get; set; }
            public bool PredictedLabel { get; set; }
        }

        public class TrainData
        {
            public bool Label { get; set; }
            [VectorType(3564)]
            public float[] Hog { get; set; }
        }
    }
}
