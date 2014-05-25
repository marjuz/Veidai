using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using System.Threading.Tasks;
using MarJuz.FaceDetection.Entities;

namespace MarJuz.FaceDetection.Mappings
{
    class PhotoMap : ClassMap<Photo>
    {
                PhotoMap()
        {
            Id(m => m.Id);
            Map(m => m.IsRec).Not.Nullable();
            Map(m => m.PhotoName).Length(50).Not.Nullable();
            Map(m => m.NoOfFaces).Nullable();
            Map(m => m.UploadDate).Nullable();
            HasMany(x => x.faces).Cascade.All().Inverse();
        }
    }
}
