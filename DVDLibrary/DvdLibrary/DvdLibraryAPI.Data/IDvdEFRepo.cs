using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibraryAPI.Data.EF;

namespace DvdLibraryAPI.Data
{
    public interface IDvdEFRepo
    {
        List<Dvd> GetAll();
        DvdInfo GetByID(int dvdID);
        List<DvdInfo> GetByTitle(string dvdTitle);
        List<DvdInfo> GetByYear(int dvdYear);
        List<DvdInfo> GetByDirector(string dvdDirector);
        List<DvdInfo> GetByRating(string dvdRating);
        void Add(DvdInfo dvd);
        void Edit(DvdInfo dvd);
        void Delete(int dvdID);
    }
}
