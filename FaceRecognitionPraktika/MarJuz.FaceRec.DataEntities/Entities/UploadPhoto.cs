using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarJuz.FaceRec.DataEntities.Entities
{
    public class UploadPhoto
    {
              
        public virtual int Id { get; set; }
        public virtual int IsPictureRecognized { get; set; }
        public virtual string PictureName { get; set; }
        public virtual int NrOfFaces { get; set; }
        public virtual DateTime? UploadDate { get; set; }
        public virtual IList<Face> faces { get; set; }
        
        
    }
}
