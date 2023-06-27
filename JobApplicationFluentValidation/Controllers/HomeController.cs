using Azure.Core;
using CSharpFunctionalExtensions;
using JobApplicationFluentValidation.Models;
using JobApplicationFluentValidation.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Resources;

namespace JobApplicationFluentValidation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(IRepository repository, IStringLocalizer<HomeController> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CultureManagement(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Index(JobApplication model, IFormFile postedFile)
        {
            if (postedFile == null || postedFile.Length == 0)
            {
                var resm = new ResourceManager("JobApplicationFluentValidation.Resources.Controllers.HomeController", typeof(HomeController).Assembly);
                ModelState.AddModelError("FileUpload", resm.GetString("NoCV"));
            }
            else
            {
                var fileName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                var extension = Path.GetExtension(postedFile.FileName);
                if (extension != ".pdf" && extension != ".txt" && extension != ".doc" && extension != ".docx")
                {
                    var resm = new ResourceManager("JobApplicationFluentValidation.Resources.Controllers.HomeController", typeof(HomeController).Assembly);
                    ModelState.AddModelError("FileUpload", resm.GetString("ExtensionError"));
                }
                else
                {
                    var fileModel = new FileModel
                    {
                        CreatedOn = DateTime.UtcNow,
                        FileType = postedFile.ContentType,
                        Extension = extension,
                        Name = fileName
                    };

                    using (var dataStream = new MemoryStream())
                    {
                        await postedFile.CopyToAsync(dataStream);
                        fileModel.Data = dataStream.ToArray();
                    }

                    if (fileModel.Data.Length > 5 * 1024 * 1024)
                    {
                        var resm = new ResourceManager("JobApplicationFluentValidation.Resources.Controllers.HomeController", typeof(HomeController).Assembly);
                        ModelState.AddModelError("FileUpload", resm.GetString("DataError"));
                    }

                    model.FileUpload = fileModel;
                }
            }

            Result<Email> emailOrError = Email.Create(model.Email);
            if(emailOrError.IsFailure)
            ModelState.AddModelError("Email", _localizer[emailOrError.Error]);
            
            if(model.Email != model.ConfirmEmail)
            {
                ModelState.AddModelError("ConfirmEmail", _localizer["CompareEmail"]);
            }

            if (model.DateOfBirth == null)
            {
                ModelState.AddModelError("DateOfBirth", _localizer["DateOfBirthNull"]);
            }
            else
            {
                Result<DateOfBirth> dateOfBirthResult = DateOfBirth.Create(model.DateOfBirth.Value);
                if (dateOfBirthResult.IsFailure)
                {
                    ModelState.AddModelError("DateOfBirth", _localizer[dateOfBirthResult.Error]);
                }
            }

            Result<HomePhone> homephone = HomePhone.Create(model.HomePhone);
            if (homephone.IsFailure)
                ModelState.AddModelError("HomePhone", _localizer[homephone.Error]);

            if (!ModelState.IsValid)
            {
                return View();
            }
            await _repository.CreateApplicant(model);
            return RedirectToAction("Index");
        }
    }
}