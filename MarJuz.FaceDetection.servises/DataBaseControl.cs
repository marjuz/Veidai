using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MarJuz.FaceDetection.Entities;
using MarJuz.FaceDetection;

namespace MarJuz.FaceDetection.servises
{
    public class DataBaseControl
    {
// Gets lists of entities  
        public List<Photo> GetPhotos()
        {
            List<Photo> photo = FaceDataBase<Photo>.Read.ToList();
            return photo;
        }
        public List<Faces> GetFaces()
        {
            List<Faces> face = FaceDataBase<Faces>.Read.ToList();
            return face;
        }
//Creates Faces
        public void CreateFace(string filename, Photo photo)
        {
            Faces face = new Faces();
            face.FaceName = "Vardas";
            face.FileName = filename;
            face.photo = photo;
            FaceDataBase<Faces>.AddPhoto(face);
        }
//Creates Photos
        public void CreatePhoto(string PictureName,  List<String> facenames)
        {
            Photo photo = new Photo();
            photo.IsRec = 0;
            photo.PhotoName = PictureName;
            photo.NoOfFaces = facenames.Count;
            photo.UploadDate = Convert.ToDateTime(DateTime.Now);
            FaceDataBase<Photo>.AddPhoto(photo);
            Photo photoNew = FaceDataBase<Photo>.GetLast();
            foreach (String fname in facenames)
            {
                CreateFace(fname, photoNew);
            }
            photoNew = null;
        }

//Get by id
        public Photo GetPhotoId(int id)
        {
            Photo photo = new Photo();
            photo = FaceDataBase<Photo>.getById(id);
            return photo;
        }
//Get photo by id
        public Photo GetPhotoById(int id)
        {
            Photo photo = new Photo();
            photo = FaceDataBase<Photo>.getPhotoById(id);
            return photo;
        }
//Get Face by id
        public Faces GetFaceById(int id)
        {
            Faces photo = new Faces();
            photo = FaceDataBase<Faces>.getFaceById(id);
            return photo;
        }

//Finds Photos faces not named
        public List<Photo> GetDataUnRec()
        {
            List<Photo> photo = FaceDataBase<Photo>.UnrecFaces.ToList();
            return photo;
        }
//Search faces by name
        public List<Faces> GetbyName(string name)
        {
            List<Faces> face = FaceDataBase<Faces>.SearchFace(name).ToList();
            return face;
        }
//Update Faces
        public void UpdateFace (List<string> names)
        {
            int a = names.Count();
            for (int i = 0; i < a; i = i + 2) { 
                string b= names [i];
                int b2 = int.Parse(b);
                string c2 = names [i+1];
                FaceDataBase<Faces>.Update(c2, b2);
            }             
                
        }
//Deletes photos
        public void DeletePhoto (int id)
        {
                FaceDataBase<Photo>.delete(id);
        }
//Edit photos
        public void EditFaceName(string name, int id)
        {
            FaceDataBase<Faces>.Update(name, id); ;
        }
    }
}

