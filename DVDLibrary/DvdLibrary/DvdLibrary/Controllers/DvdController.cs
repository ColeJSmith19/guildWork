using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DvdLibrary.Models;
using DvdLibraryAPI.Data;

namespace DvdLibrary.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdController : ApiController
    {
        private static IDvdRepo _dvdRepo = DvdRepoFactory.GetRepository();

        [Route("dvds")]
        [AcceptVerbs("GET")]
        public IHttpActionResult All()
        {
            return Ok(_dvdRepo.GetAll());
        }

        [Route("dvd/{dvdID}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByID(int dvdID)
        {
            Dvd dvd = _dvdRepo.GetByID(dvdID);

            if (dvd == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dvd);
            }
        }

        [Route("dvds/title/{dvdTitle}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByTitle(string dvdTitle)
        {
            List<Dvd> dvd = _dvdRepo.GetByTitle(dvdTitle);

            if (dvd == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dvd);
            }
        }

        [Route("dvds/year/{dvdYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByYear(int dvdYear)
        {
            List<Dvd> dvd = _dvdRepo.GetByYear(dvdYear);

            if (dvd == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dvd);
            }
        }

        [Route("dvds/director/{dvdDirector}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByDirector(string dvdDirector)
        {
            List<Dvd> dvd = _dvdRepo.GetByDirector(dvdDirector);

            if (dvd == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dvd);
            }
        }

        [Route("dvds/rating/{dvdRating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByRating(string dvdRating)
        {
            List<Dvd> dvd = _dvdRepo.GetByRating(dvdRating);

            if (dvd == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dvd);
            }
        }

        [Route("dvd")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Add(AddDvdRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dvd dvd = new Dvd()
            {
                Title = request.Title,
                ReleaseYear = request.ReleaseYear,
                Director = request.Director,
                Rating = request.Rating,
                Notes = request.Notes
            };

            _dvdRepo.Add(dvd);
            return Created($"dvds/get/{dvd.DvdID}", dvd);
        }

        [Route("dvd/{dvdID}")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult Update(UpdateDvdRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dvd dvd = _dvdRepo.GetByID(request.DvdID);

            if(dvd == null)
            {
                return NotFound();
            }

            dvd.DvdID = request.DvdID;
            dvd.Title = request.Title;
            dvd.ReleaseYear = request.ReleaseYear;
            dvd.Director = request.Director;
            dvd.Rating = request.Rating;
            dvd.Notes = request.Notes;

            _dvdRepo.Edit(dvd);
            return Ok(dvd);
        }

        [Route("dvd/{dvdID}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(int dvdID)
        {
            Dvd dvd = _dvdRepo.GetByID(dvdID);

            if (dvd == null)
            {
                return NotFound();
            }

            _dvdRepo.Delete(dvdID);
            return Ok();
        }
    }
}