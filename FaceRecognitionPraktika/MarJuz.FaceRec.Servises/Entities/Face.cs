using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MarJuz.FaceRec.Servises.Entities
{
    /// <summary>
    /// Represents a face that has been detected within an image.
    /// </summary>
    public class FaceR
    {
        /// <summary>
        /// Gets or sets the bounds of the detected face.
        /// </summary>
        /// <value>
        /// The bounds.
        /// </value>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Gets the square pixels.
        /// </summary>
        public int SquarePixels
        {
            get
            {
                return this.Bounds.Width * this.Bounds.Height;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Face"/> class.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        public FaceR (Rectangle bounds)
        {
            this.Bounds = bounds;
        }
    }
}