using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibraryAPI.Data
{
    public class DvdMockRepo : IDvdRepo
    {
        private List<Dvd> _dvds = new List<Dvd>
        {
            new Dvd
            {
                DvdID=1, Title="Kung Fu Hustle", ReleaseYear=2004, Rating="PG13", Director="Stephen Chow", Notes="Great film"
            },
            new Dvd
            {
                DvdID=2, Title="Howl's Moving Castle", ReleaseYear=2005, Rating="PG", Director="Hayao Miyazaki", Notes = "Fantastic movie"
            },
            new Dvd
            {
                DvdID=3, Title="Mean Girls", ReleaseYear=2004, Rating="PG13", Director="Mark Waters", Notes = "Clown film"
            }
        };

        public List<Dvd> GetAll()
        {
            return _dvds;
        }

        public Dvd GetByID(int dvdID)
        {
            return _dvds.FirstOrDefault(d => d.DvdID == dvdID);
        }

        public List<Dvd> GetByTitle(string dvdTitle)
        {
            return _dvds.Where(d => d.Title == dvdTitle).ToList();
        }

        public List<Dvd> GetByYear(int dvdYear)
        {
            return _dvds.Where(d => d.ReleaseYear == dvdYear).ToList();
        }

        public List<Dvd> GetByDirector(string dvdDirector)
        {
            return _dvds.Where(d => d.Director == dvdDirector).ToList();
        }

        public List<Dvd> GetByRating(string dvdRating)
        {
            return _dvds.Where(d => d.Rating == dvdRating).ToList();
        }

        public void Add(Dvd dvd)
        {
            dvd.DvdID = _dvds.Max(d => d.DvdID) + 1;
            _dvds.Add(dvd);
        }

        public void Edit(Dvd dvd)
        {
            var found = _dvds.FirstOrDefault(d => d.DvdID == dvd.DvdID);
        }

        public void Delete(int dvdID)
        {
            _dvds.RemoveAll(d => d.DvdID == dvdID);
        }
    }
}

