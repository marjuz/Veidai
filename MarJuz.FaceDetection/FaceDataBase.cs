using NHibernate.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using MarJuz.FaceDetection.Entities;
using MarJuz.FaceDetection.Mappings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarJuz.FaceDetection
{
    public class FaceDataBase<M>
    {
       
        private static ISessionFactory _sessionsFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionsFactory == null)

                    InitializeSessionFactory();
                return _sessionsFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            _sessionsFactory = Fluently.Configure()
            .Database(MsSqlConfiguration
           .MsSql2008
           .ConnectionString(ConnString)
           .ShowSql())
           .Mappings(m => m
           .FluentMappings.AddFromAssemblyOf<FacesMap>()
           .Conventions.Add(ForeignKey.EndsWith("Id")))
           .BuildConfiguration()
           .BuildSessionFactory();
        }

        //private const string ConnString = "Data Source=(local);Initial Catalog=FaceDet;Integrated Security=SSPI;";
        private const string ConnString = "Server=mydbs.clkwdikg2yai.us-west-2.rds.amazonaws.com,1433;Database=FaceRec;User Id=praktika; Password=praktika;";
//Returns all entities
        public static IEnumerable<M> Read
        {
            get
            {
                using (var session = SessionFactory.OpenSession())
                {
                    var group = (from items in session.Query<M>()
                                 select items).ToList();
                    return group;
                }
            }
        }
//Adds entities
        public static void AddPhoto(M picture)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(picture);
                    transaction.Commit();
                }
            }
        }
//Fins all Photos where faces have not names
        public static IEnumerable<Photo> UnrecFaces
        {
            get
            {
                var session = SessionFactory.OpenSession();
                
                    var query = (from item in session.Query<Photo>()
                                 where item.IsRec == 0
                                 select item).ToList();
                    return query;
              
            }
        }
//searches faces 
        public static IEnumerable<Faces> SearchFace(string name)
        {

            var session = SessionFactory.OpenSession();
            
                var query = (from item in session.Query<Faces>()
                             where item.FaceName.Contains(name)
                             select item).ToList();
                return query;

        }
//Returns photo by id
        public static Photo getById(int id)
        {

            using (var session = SessionFactory.OpenSession())
            {
                var query = (from item in session.Query<Photo>()
                             where item.Id == id
                             select item).Single();
                return query;
            }
        }
//Returns face by id
        public static Faces getFaceById(int id)
        {

            using (var session = SessionFactory.OpenSession())
            {
                var query = (from item in session.Query<Faces>()
                             where item.Id == id
                             select item).Single();
                return query;
            }
        }
//Returns last entry
        public static Photo GetLast()
        {

            using (var session = SessionFactory.OpenSession())
            {
                var query = (from item in session.Query<Photo>()
                             where item.Id >= 1
                             orderby item.Id descending
                             select item).First();
                
                return query;  
            }
            
        }
//Update 
        public static void Update(string name, int id)
        {

            using (var session = SessionFactory.OpenSession())
            {
                Faces selected =session.Get<Faces>(id);
             
             
                selected.FaceName=name;
                selected.photo.IsRec = 1;
           
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(selected);
                    transaction.Commit();
                }
            }
        }
//Update face name
        public static void UpdateFace(string name, int id)
        {

            using (var session = SessionFactory.OpenSession())
            {
                Faces selected = session.Get<Faces>(id);


                selected.FaceName = name;

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(selected);
                    transaction.Commit();
                }
            }
        }
//Delete
        public static void delete (int id)
        {

            using (var session = SessionFactory.OpenSession())
            {
	         
	                var query = (from item in session.Query<Photo>()
                             where item.Id == id
                             select item).Single();
	 
	                using (var transaction = session.BeginTransaction())
	                {
	                    session.Delete(query);
	                    transaction.Commit();
	                }
            }
        }

        public static Photo getPhotoById(int id)
        {
            var session = SessionFactory.OpenSession();
            
                var query = (from item in session.Query<Photo>()
                             where item.Id == id
                             select item).Single();
                return query;
        }
    }
}
