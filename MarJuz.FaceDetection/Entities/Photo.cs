using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarJuz.FaceDetection.Entities
{
   public class Photo
    {
        public virtual int Id { get; set; }
        public virtual int IsRec { get; set; }
        public virtual string PhotoName { get; set; }
        public virtual int NoOfFaces { get; set; }
        public virtual DateTime? UploadDate { get; set; }
        public virtual IList<Faces> faces { get; set; }
    }
}
