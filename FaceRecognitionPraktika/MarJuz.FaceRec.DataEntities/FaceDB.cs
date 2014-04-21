using NHibernate;
using FluentNHibernate.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using MarJuz.FaceRec.DataEntities.Mappings;
using NHibernate.Linq;

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarJuz.FaceRec.DataEntities.Entities;

namespace MarJuz.FaceRec.DataEntities
{

    public class FaceDB
    {
        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
           .Database(MsSqlConfiguration
           .MsSql2008
           .ConnectionString(ConnString)
           .ShowSql())
           .Mappings(m => m
           .FluentMappings.AddFromAssemblyOf<FaceMap>()
           .Conventions.Add(ForeignKey.EndsWith("Id")))
           .BuildConfiguration()
           .BuildSessionFactory();
        }
       
        
        //private const string ConnString = "Data Source=(local);Initial Catalog=FaceRec;Integrated Security=SSPI;";
        private const string ConnString = "Server=mjdb.clkwdikg2yai.us-west-2.rds.amazonaws.com,1433;Database=FaceRec;User Id=marjuz; Password=AMartynas0822;";

        public static IEnumerable<UploadPhoto> Read
	        {
            get{
                //List<UploadPhoto> uploadPhoto = new List<UploadPhoto>();

                var sessionFactory = CreateSessionFactory();
                using (var session = sessionFactory.OpenSession())
	                       
                
                {
                    var query = (from item in session.Query<UploadPhoto>()
                             select item).ToList();
                    return query;
                }
	            }
	        }
        public static IEnumerable<Face> ReadFace
        {
            get
            {
                //List<UploadPhoto> uploadPhoto = new List<UploadPhoto>();

                var sessionFactory = CreateSessionFactory();
                using (var session = sessionFactory.OpenSession())
                {
                    var query = (from item in session.Query<Face>()
                             select item).ToList();
                    return query;
                }
            }
        }

        public static void AddPhoto(UploadPhoto photo)
        {
            var sessionFactory = CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(photo);
                    transaction.Commit();
                }
            }
        }
        public static void AddFace(Face face)
        {
            var sessionFactory = CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(face);
                    transaction.Commit();
                }
            }
        }
        
    }
}
