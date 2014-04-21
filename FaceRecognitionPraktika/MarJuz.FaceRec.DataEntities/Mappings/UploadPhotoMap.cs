using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using MarJuz.FaceRec.DataEntities.Entities;

namespace MarJuz.FaceRec.DataEntities.Mappings
{
    class UploadPhotoMap : ClassMap<UploadPhoto>
    {
        UploadPhotoMap()
        {
            Id(m => m.Id);
            Map(m => m.IsPictureRecognized).Not.Nullable();
            Map(m => m.PictureName).Length(50).Not.Nullable();
            Map(m => m.NrOfFaces).Nullable();
            Map(m => m.UploadDate).Nullable();
            HasMany(x => x.faces).Cascade.All().Inverse(); 
        }
    }

}
