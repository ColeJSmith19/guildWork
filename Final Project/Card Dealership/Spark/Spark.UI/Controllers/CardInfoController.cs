using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spark.Data.Factory;
using Spark.Models.Tables;
using Spark.UI.Models;

namespace Spark.UI.Controllers
{
    public class CardInfoController : Controller
    {
        // GET: CardInfo
        public ActionResult Details(int id)
        {
            var repo = CardInfoRepositoryFactory.GetRepository();
            var model = repo.GetDetails(id);

            return View(model);
        }

        public ActionResult Index()
        {
            var repo = SetsRepositoryFactory.GetRepository();
            
            return View(repo.GetAll());
        }

        public ActionResult Add()
        {
            var model = new CardInfoAddViewModel();

            var setsRepo = SetsRepositoryFactory.GetRepository();
            var artistsRepo = ArtistRepositoryFactory.GetRepository();

            model.Sets = new SelectList(setsRepo.GetAll(), "SetId", "SetName");
            model.Artists = new SelectList(artistsRepo.GetAll(), "ArtistId", "ArtistName");
            model.CardInfo = new CardInfo();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(CardInfoAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = CardInfoRepositoryFactory.GetRepository();

                try
                {
                    var savepath = Server.MapPath("~/Images");

                    string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);

                    var filePath = Path.Combine(savepath, fileName + extension);

                    int counter = 1;
                    while (System.IO.File.Exists(filePath))
                    {
                        filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                        counter++;
                    }

                    model.ImageUpload.SaveAs(filePath);

                    model.CardInfo.CardArtURL = Path.GetFileName(filePath);

                    repo.Insert(model.CardInfo);

                    return RedirectToAction("Edit", new { id = model.CardInfo.CardId });
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var setsRepo = SetsRepositoryFactory.GetRepository();
                model.Sets = new SelectList(setsRepo.GetAll(), "SetId", "SetName");
                var artistsRepo = ArtistRepositoryFactory.GetRepository();
                model.Artists = new SelectList(artistsRepo.GetAll(), "ArtistId", "ArtistName");
                return View(model);
            }
             
        }

        public ActionResult Edit(int id)
        {
            var model = new CardInfoEditViewModel();

            var setsRepo = SetsRepositoryFactory.GetRepository();
            var artistsRepo = ArtistRepositoryFactory.GetRepository();
            var cardInfoRepo = CardInfoRepositoryFactory.GetRepository();

            model.Sets = new SelectList(setsRepo.GetAll(), "SetId", "SetName");
            model.Artists = new SelectList(artistsRepo.GetAll(), "ArtistId", "ArtistName");
            model.CardInfo = cardInfoRepo.GetById(id);

            //if(user is not admin)
            //{
            //   throw new Exception("Only the admin can edit");
            //}

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CardInfoEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = CardInfoRepositoryFactory.GetRepository();
                var oldCardInfo = repo.GetById(model.CardInfo.CardId);
                try
                {
                    if (oldCardInfo.CardArtURL != model.ImageUpload.FileName)
                    {
                        var savepath = Server.MapPath("~/Images");

                        string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        var filePath = Path.Combine(savepath, fileName + extension);

                        int counter = 1;
                        while (System.IO.File.Exists(filePath))
                        {
                            filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                            counter++;
                        }
                        model.ImageUpload.SaveAs(filePath);
                        model.CardInfo.CardArtURL = Path.GetFileName(filePath);

                        var oldPath = Path.Combine(savepath, oldCardInfo.CardArtURL);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    repo.Update(model.CardInfo);

                    return RedirectToAction("Edit", new { id = model.CardInfo.CardId });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var setsRepo = SetsRepositoryFactory.GetRepository();
                model.Sets = new SelectList(setsRepo.GetAll(), "SetId", "SetName");
                var artistsRepo = ArtistRepositoryFactory.GetRepository();
                model.Artists = new SelectList(artistsRepo.GetAll(), "ArtistId", "ArtistName");
                return View(model);
            }

        }
    }
}