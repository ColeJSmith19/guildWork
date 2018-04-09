using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spark.Models.Queries;
using Spark.Models.Tables;

namespace Spark.UI.Models
{
    public class CardInfoAddViewModel : IValidatableObject
    {
        public IEnumerable<SelectListItem> Sets { get; set; }
        public IEnumerable<SelectListItem> Artists { get; set; }
        public CardInfo CardInfo { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(CardInfo.CardName))
            {
                errors.Add(new ValidationResult("Card Name is required."));
            }
            if (CardInfo.Cmc < 0)
            {
                errors.Add(new ValidationResult("CMC is required."));
            }
            if (string.IsNullOrEmpty(CardInfo.Colors))
            {
                errors.Add(new ValidationResult("Card Color is required."));
            }
            if (string.IsNullOrEmpty(CardInfo.CardNumber))
            {
                errors.Add(new ValidationResult("Card Number is required."));
            }
            if (CardInfo.MSRP <= 0)
            {
                errors.Add(new ValidationResult("MSRP is required."));
            }
            if (CardInfo.SalePrice <= 0)
            {
                errors.Add(new ValidationResult("Sale Price is required."));
            }
            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                var extensions = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

                var extension = Path.GetExtension(ImageUpload.FileName);

                if(!extensions.Contains(extension))
                {
                    errors.Add(new ValidationResult("Image file must be jpg. png. gif or jpeg."));
                }
            }
            else
            {
                errors.Add(new ValidationResult("Image is required."));
            }
            return errors;
        }
    }
}