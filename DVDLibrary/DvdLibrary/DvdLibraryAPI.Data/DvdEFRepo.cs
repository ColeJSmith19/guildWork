using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibraryAPI.Data.EF;

namespace DvdLibraryAPI.Data
{
    class DvdEFRepo : IDvdRepo
    {
        public void Add(Dvd dvd)
        {
            var repository = new DvdLibraryEntities();

            var myRelease = repository.Releases.FirstOrDefault(d => d.ReleaseYear == dvd.ReleaseYear);
            if(myRelease == null)
            {
                myRelease = repository.Releases.Add(new Release { ReleaseYear = dvd.ReleaseYear });
                repository.SaveChanges();
            }

            var myDirector = repository.Directors.FirstOrDefault(d => d.DirectorName == dvd.Director);
            if(myDirector == null)
            {
                 myDirector = repository.Directors.Add(new Director { DirectorName = dvd.Director  });
                 repository.SaveChanges();
            }

            var myRating = repository.Ratings.FirstOrDefault(d => d.RatingName == dvd.Rating);
            if(myRating == null)
            {
                myRating = repository.Ratings.Add(new Rating { RatingName = dvd.Rating });
                repository.SaveChanges();
            }
            var myDvdInfo = repository.Dvds.Add(
            new EF.DvdInfo
            {
                DvdTitle = dvd.Title,
                Release = myRelease,
                Director = myDirector,
                Rating = myRating

            });
            repository.SaveChanges();
           // return myDvdInfo.DvdID;

        }

        public void Delete(int dvdID)
        {
            var repository = new DvdLibraryEntities();
            var dvd = repository.Dvds.Find(dvdID);
            if(dvd != null)
            {
                Dvd delete = new Dvd
                {
                    DvdID = dvd.DvdID,
                    Title = dvd.DvdTitle,
                    ReleaseYear = dvd.Release.ReleaseYear,
                    Director = dvd.Director.DirectorName,
                    Rating = dvd.Rating.RatingName,
                    Notes = dvd.Notes
                };

                repository.Dvds.Remove(dvd);
                repository.SaveChanges();
            }
        }

        public void Edit(Dvd dvd)
        {
            var repository = new DvdLibraryEntities();
            Dvd oldDvd = GetByID(dvd.DvdID);
            if(oldDvd != null){
                var myRelease = repository.Releases.FirstOrDefault(d => d.ReleaseYear == dvd.ReleaseYear);
                if (myRelease == null)
                {
                    myRelease = repository.Releases.Add(new Release { ReleaseYear = dvd.ReleaseYear });
                    repository.SaveChanges();
                }

                var myDirector = repository.Directors.FirstOrDefault(d => d.DirectorName == dvd.Director);
                if (myDirector == null)
                {
                    myDirector = repository.Directors.Add(new Director { DirectorName = dvd.Director });
                    repository.SaveChanges();
                }

                var myRating = repository.Ratings.FirstOrDefault(d => d.RatingName == dvd.Rating);
                if (myRating == null)
                {
                    myRating = repository.Ratings.Add(new Rating { RatingName = dvd.Rating });
                    repository.SaveChanges();
                }
                DvdInfo edited = new DvdInfo
                {
                    DvdTitle = dvd.Title,
                    Release = myRelease,
                    Director = myDirector,
                    Rating = myRating

                };
                repository.Entry(edited).State = EntityState.Modified;
                repository.SaveChanges();
            }
        }

        public List<Dvd> GetAll()
        {
            var repository = new DvdLibraryEntities();
            List<Dvd> dvdListM = new List<Dvd>();
            var dvdList = repository.Dvds.ToList();
          
            foreach (EF.DvdInfo d in dvdList)
            {
                dvdListM.Add(
                    new Dvd
                    {
                        DvdID = d.DvdID,
                        Title = d.DvdTitle,
                        ReleaseYear = d.Release.ReleaseYear,
                        Director = d.Director.DirectorName,
                        Rating = d.Rating.RatingName,
                        Notes = d.Notes
                    });
            }
            return dvdListM;
        }

        public List<Dvd> GetByDirector(string dvdDirector)
        {
            var repository = new DvdLibraryEntities();
            List<Dvd> returnList = new List<Dvd>();
            var dvdList = repository.Dvds.ToList();

            foreach(DvdInfo d in dvdList)
            {
                var DVD = d;
                if(DVD.Director.DirectorName == dvdDirector)
                {
                    returnList.Add(

                        new Dvd
                        {
                            DvdID = d.DvdID,
                            Title = d.DvdTitle,
                            ReleaseYear = d.Release.ReleaseYear,
                            Director = d.Director.DirectorName,
                            Rating = d.Rating.RatingName,
                            Notes = d.Notes

                        });
                }
            }
            return returnList;
        }

        public Dvd GetByID(int dvdID)
        {
            var repository = new DvdLibraryEntities();
            var dvdToGet = repository.Dvds.Find(dvdID);
            Dvd returnDvd = new Dvd();
            if (dvdToGet != null)
            {
                returnDvd.DvdID = dvdToGet.DvdID;
                returnDvd.Title = dvdToGet.DvdTitle;
                returnDvd.ReleaseYear = dvdToGet.Release.ReleaseYear;
                returnDvd.Director = dvdToGet.Director.DirectorName;
                returnDvd.Rating = dvdToGet.Rating.RatingName;
                returnDvd.Notes = dvdToGet.Notes;
                return returnDvd;
            }
            else
                return null;
        }

        public List<Dvd> GetByRating(string dvdRating)
        {
            var repository = new DvdLibraryEntities();
            List<Dvd> returnList = new List<Dvd>();
            var dvdList = repository.Dvds.ToList();

            foreach (DvdInfo d in dvdList)
            {
                var DVD = d;
                if (DVD.Rating.RatingName == dvdRating)
                {
                    returnList.Add(

                        new Dvd
                        {
                            DvdID = d.DvdID,
                            Title = d.DvdTitle,
                            ReleaseYear = d.Release.ReleaseYear,
                            Director = d.Director.DirectorName,
                            Rating = d.Rating.RatingName,
                            Notes = d.Notes

                        });
                }
            }
            return returnList;
        }

        public List<Dvd> GetByTitle(string dvdTitle)
        {
            var repository = new DvdLibraryEntities();
            List<Dvd> returnList = new List<Dvd>();
            var dvdList = repository.Dvds.ToList();

            foreach (DvdInfo d in dvdList)
            {
                var DVD = d;
                if (DVD.DvdTitle == dvdTitle)
                {
                    returnList.Add(

                        new Dvd
                        {
                            DvdID = d.DvdID,
                            Title = d.DvdTitle,
                            ReleaseYear = d.Release.ReleaseYear,
                            Director = d.Director.DirectorName,
                            Rating = d.Rating.RatingName,
                            Notes = d.Notes

                        });
                }
            }
            return returnList;
        }

        public List<Dvd> GetByYear(int dvdYear)
        {
            var repository = new DvdLibraryEntities();
            List<Dvd> returnList = new List<Dvd>();
            var dvdList = repository.Dvds.ToList();

            foreach (DvdInfo d in dvdList)
            {
                var DVD = d;
                if (DVD.Release.ReleaseYear == dvdYear)
                {
                    returnList.Add(

                        new Dvd
                        {
                            DvdID = d.DvdID,
                            Title = d.DvdTitle,
                            ReleaseYear = d.Release.ReleaseYear,
                            Director = d.Director.DirectorName,
                            Rating = d.Rating.RatingName,
                            Notes = d.Notes

                        });
                }
            }
            return returnList;
        }
    }
}
