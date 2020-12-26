using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AOCI1
{
    public partial class Form1 : Form
    {
        ImageEditor imgEditor = new ImageEditor();
        Image<Bgr, byte> image, defaultImage;
        PointF[] srcPoints = new PointF[4];
        List<PointF> points = new List<PointF>();
        int mouseX, mouseY;
        VideoCapture capture;
        int frameCounter = 0;
        int minArea = 0;
        CascadeClassifier face = new CascadeClassifier("D:\\Tesseract\\haarcascade_frontalface_default.xml");
        Mat frame;
        Mat capImage = new Mat();

        public Form1()
        {
            InitializeComponent();
            imageBox1.MouseClick += new MouseEventHandler(imageBox2_MouseClick);
          
        }

        private void imageBox2_MouseClick(object sender, MouseEventArgs e)
        {
            mouseX = (int)(e.Location.X / imageBox1.ZoomScale / ((double)imageBox1.Width / (double)defaultImage.Width));
            mouseY = (int)(e.Location.Y / imageBox1.ZoomScale / ((double)imageBox1.Width / (double)defaultImage.Width));

            points.Add(new PointF(mouseX, mouseY));
            if(points.Count > 4)
            {
                points.RemoveAt(0);
            }
            if(points.Count == 4)
            {
                srcPoints = points.ToArray();
            }

            Point center = new Point(mouseX, mouseY);
            int radius = 2;
            int thickness = 2;
            var color = new Bgr(Color.Red).MCvScalar;
            // функция, рисующая на изображении круг с заданными параметрами
            CvInvoke.Circle(defaultImage, center, radius, color, thickness);
            imgEditor.ShowImage(imageBox1, defaultImage);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            defaultImage = imgEditor.SetSourceImage(OpenImageFile());
            imgEditor.ShowImage(imageBox1, defaultImage);
        }


   

        private void buttonLukas_Click(object sender, EventArgs e)
        {
            image = imgEditor.SetAddtionalImage(OpenImageFile());
            image = imgEditor.ReturnLucas(defaultImage, image, out defaultImage);
            imgEditor.ShowImage(imageBox1, defaultImage);
            FillImage2();
        }

        private void buttonCompare_Click(object sender, EventArgs e)
        {
            image = imgEditor.SetAddtionalImage(OpenImageFile());
            var bigImg = imgEditor.ReturnCompared(out defaultImage, out image);
            imgEditor.ShowImage(imageBox1, defaultImage);
            imgEditor.ShowImage(imageBox3, bigImg);
            FillImage2();
        }

        private string OpenImageFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog(); // открытие диалога выбора файла
            if (result == DialogResult.OK) // открытие выбранного файла
            {
                string fileName = openFileDialog.FileName;
                return fileName;
            }
            return null;
        }
        

       //filllllllllllll

        private void FillImage2(bool mode = true)
        {
            
            imgEditor.ShowImage(imageBox2, image, mode);
        }

        
    }
}
