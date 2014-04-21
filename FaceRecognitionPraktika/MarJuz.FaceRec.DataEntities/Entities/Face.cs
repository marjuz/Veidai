using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarJuz.FaceRec.DataEntities.Entities
{
    public class Face
    {
               
        public virtual int Id { get; set; }
        public virtual int PhotoId { get; set; }
        public virtual string FaceName { get; set; }
        public virtual UploadPhoto Photo { get; set; }

    }
}
