using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarJuz.FaceDetection.Entities
{
    public class Faces
    {
        public virtual int Id { get; set; }
        public virtual int PhotosId { get; set; }
        public virtual string FaceName { get; set; }
        public virtual string FileName { get; set; }
        public virtual Photo photo { get; set; }
    }
}
