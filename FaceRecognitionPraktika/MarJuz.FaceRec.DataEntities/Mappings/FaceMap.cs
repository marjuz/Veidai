using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using MarJuz.FaceRec.DataEntities.Entities;

namespace MarJuz.FaceRec.DataEntities.Mappings
{
    class FaceMap : ClassMap<Face>
    {
           FaceMap()
        {
            Id(m => m.Id);
            Map(m => m.FaceName).Length(50).Not.Nullable();
            
            References(x => x.Photo).LazyLoad().Cascade.All(); 
        }
    }
}
