using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;

using MarJuz.FaceDetection.Entities;
using MarJuz.FaceDetection;

namespace MarJuz.FaceDetection.servises
{
    public class FacesDet
    {
        public DataBaseControl DataBaseControl
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

      //Main method  
        public void MakeChanges(Stream stream, string fname, string content)
        {
            List<String> facenames = new List<string>();
            var date = String.Format("{0:MM.dd.yyyy_HH.mm.ss}", DateTime.Now);
            string name = (date.ToString() + fname);
            string filePath = Path.Combine(content + "images/" + name);

            Bitmap bmp = new Bitmap(stream);
            bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            facenames = FindFaces(bmp, content);
            bmp.Dispose();
            SendPhotoToDB(name, facenames);
            facenames = null;
        }
        //Sends photo to DB
        private void SendPhotoToDB(string name, List<String> facenames)
        {
            DataBaseControl db = new DataBaseControl();
            db.CreatePhoto(name, facenames);
        }
      //Fins Faces in Photo
        public List<String> FindFaces(Bitmap image, string content)
        {
            #region variables
            Image<Bgr, Byte> result;
            Image<Gray, byte> grayImage = null;
            Rectangle[] rectangles = null;
            List<String> answer = new List<string>();
            #endregion

            CascadeClassifier Face = new CascadeClassifier(content + "Content\\haarcascade_frontalface_default.xml");

            if (image != null)
            {
                int maxWidth = 400;
                if (image.Width > maxWidth)
                {
                    Bitmap newBmp = new Bitmap(image, maxWidth, (int)(((float)maxWidth / (float)image.Width) * (float)image.Height));

                    image.Dispose();
                    image = newBmp;
                }
            }
            Image<Bgr, Byte> img = new Image<Bgr, Byte>(image);
            grayImage = img.Convert<Gray, byte>();
            
            rectangles = Face.DetectMultiScale(grayImage, 1.1, 10, Size.Empty, Size.Empty);

            foreach (Rectangle rec in rectangles)
            {
                img.Draw(rec, new Bgr(Color.Red), 2);
                result = img.Copy(rec).Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                using (Bitmap b = result.Bitmap)
                {
                    var myUniqueFileName = string.Format(@"{0}.jpg", Guid.NewGuid());
                    System.IO.FileStream fs = System.IO.File.Open((content + "images/" + myUniqueFileName), FileMode.Create);
                    b.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                    fs.Close();
                    answer.Add(myUniqueFileName);
                }
                result.Dispose();
            }
            return answer;
        }
    }
}