using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarJuz.FaceRec.Servises.Entities;
using System.Drawing;
using OpenCvSharp;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using OpenCvSharp.Extensions;

using System.Diagnostics;





namespace MarJuz.FaceRec.Servises
{
    /// <summary>
    /// Provides methods for performing face detection within images.
    /// </summary>
    public class FaceDetection
    {

        private const double Scale = 1;
        private const double ScaleFactorFirst = 1.05;
        private const double ScaleFactorSecond = 1.01;
        private const int MinNeighborsFirst = 50;
        private const int MinNeighborsSecond = 3;

        /// <summary>
        /// Gets or sets the haar cascade.
        /// </summary>
        /// <value>
        /// The haar cascade.
        /// </value>
        public CvHaarClassifierCascade HaarCascade { get; protected set; }

        
        public static bool HasJpegExtension(string filename)
        {

            return Path.GetExtension(filename).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase)
                || Path.GetExtension(filename).Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase);
        }   

        /// <summary>
        /// Initializes the <see cref="FaceDetection"/> class.
        /// </summary>
         
        
        
        
        public FaceDetection(string haarCascadeFileName)
        {
            if (HasJpegExtension(haarCascadeFileName))
                this.HaarCascade = CvHaarClassifierCascade.FromFile(haarCascadeFileName);
            else
                this.HaarCascade = null;
                
        }

        /// <summary>
        /// Finds the faces.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public List<FaceR> FindFaces(Bitmap image)
        {
            List<FaceR> results = new List<FaceR>();

            int w = image.Width;
            int h = image.Height;
            int channels;
            switch (image.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    channels = 3; break;
                case PixelFormat.Format32bppArgb:
                    channels = 4; break;
                case PixelFormat.Format8bppIndexed:
                case PixelFormat.Format1bppIndexed:
                    channels = 1; break;
                default:
                    throw new NotImplementedException();
            }

            using (IplImage img = IplImage.FromFile("image"))
            {
                using (IplImage smallImg = new IplImage(new CvSize(Cv.Round(img.Width / Scale), Cv.Round(img.Height / Scale)), BitDepth.U8, 1))
                {
                    using (IplImage gray = new IplImage(img.Size, BitDepth.U8, 1))
                    {
                        Cv.CvtColor(img, gray, ColorConversion.BgrToGray);
                        Cv.Resize(gray, smallImg, Interpolation.Linear);
                        Cv.EqualizeHist(smallImg, smallImg);
                    }

                    using (CvMemStorage storage = new CvMemStorage())
                    {
                        storage.Clear();

                        CvSeq<CvAvgComp> faces = Cv.HaarDetectObjects(smallImg, this.HaarCascade, storage, ScaleFactorFirst, MinNeighborsFirst, HaarDetectionType.DoCannyPruning, new CvSize(10, 10));

                        for (int i = 0; i < faces.Total; i++)
                        {
                            FaceR face = new FaceR(ToRectangle(faces[i].Value.Rect));
                            results.Add(face);
                        }
                    }

                    if (results.Count == 0)
                    {
                        using (CvMemStorage storage = new CvMemStorage())
                        {
                            storage.Clear();

                            CvSeq<CvAvgComp> faces = Cv.HaarDetectObjects(smallImg, this.HaarCascade, storage, ScaleFactorSecond, MinNeighborsSecond, HaarDetectionType.Zero, new CvSize(10, 10));

                            for (int i = 0; i < faces.Total; i++)
                            {
                                FaceR face = new FaceR(ToRectangle(faces[i].Value.Rect));
                                results.Add(face);
                            }
                        }
                    }
                }
            }

            List<FaceR> filteredResults = new List<FaceR>();

            while (results.Count > 0)
            {
                FaceR face = results[0];
                results.Remove(face);
                foreach (FaceR otherFace in results.FindAll(f => f.Bounds.IntersectsWith(face.Bounds)))
                {
                    int left = Math.Min(face.Bounds.Left, otherFace.Bounds.Left);
                    int top = Math.Min(face.Bounds.Top, otherFace.Bounds.Top);
                    int right = Math.Max(face.Bounds.Right, otherFace.Bounds.Right);
                    int bottom = Math.Max(face.Bounds.Bottom, otherFace.Bounds.Bottom);
                    face.Bounds = new Rectangle(new Point(left, top), new Size(right - left, bottom - top));
                    results.Remove(otherFace);
                }
                filteredResults.Add(face);
            }

            return filteredResults;
        }

        /// <summary>
        /// Finds the largest face.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public FaceR FindLargestFace(Bitmap image)
        {
            return this.LargestFace(FindFaces(image));
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Rectangle"/> to <see cref="OpenCvSharp.CvRect"/>.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static CvRect ToCvRect(Rectangle r)
        {
            return new CvRect(r.X, r.Y, r.Width, r.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="OpenCvSharp.CvRect"/> to <see cref="System.Drawing.Rectangle"/>.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static Rectangle ToRectangle(CvRect r)
        {
            return new Rectangle(r.X, r.Y, r.Width, r.Height);
        }

        /// <summary>
        /// Gets the largest face from the list of Faces.
        /// </summary>
        /// <param name="faces">The faces.</param>
        /// <returns></returns>
        public FaceR LargestFace(List<FaceR> faces)
        {
            List<FaceR> sortedList = new List<FaceR>(faces);
            sortedList.Sort(delegate(FaceR a, FaceR b) { return b.SquarePixels.CompareTo(a.SquarePixels); });
            return sortedList.FirstOrDefault();
        }



    }
}