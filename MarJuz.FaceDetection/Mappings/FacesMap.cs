using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using MarJuz.FaceDetection.Entities;

namespace MarJuz.FaceDetection.Mappings
{
    class FacesMap : ClassMap<Faces>
    {
                FacesMap()
        {
            Id(m => m.Id);
            Map(m => m.FaceName).Length(50).Not.Nullable();
            Map(m => m.FileName).Length(50).Not.Nullable();
            References(x => x.photo).LazyLoad().Cascade.All();
        }
    }
}
