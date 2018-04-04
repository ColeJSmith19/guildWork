using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibraryAPI.Data.EF;

namespace DvdLibraryAPI.Data
{
    public interface IDvdRepo
    {
        List<Dvd> GetAll();
        Dvd GetByID(int dvdID);
        List<Dvd> GetByTitle(string dvdTitle);
        List<Dvd> GetByYear(int dvdYear);
        List<Dvd> GetByDirector(string dvdDirector);
        List<Dvd> GetByRating(string dvdRating);
        void Add(Dvd dvd);
        void Edit(Dvd dvd);
        void Delete(int dvdID);
    }
}
