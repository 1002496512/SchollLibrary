using Microsoft.AspNetCore.Mvc;
using LibraryModels;
using WebApiClient;
using System.Net;
using System.Collections.Generic;
using LibraryModels;

namespace WebLibrary.Controllers
{
    public class GuestController : Controller
    {
        [HttpGet]
        public IActionResult HomePage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BookCatalog(string authorId = null, string ganreId = null, int page = 1)
        {
            // 1/get data from WS
            WebClient<CatalogViewModel> client = new WebClient<CatalogViewModel>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5185;
            client.Path = "api/Guest/GetBookCatalog";

            if (authorId != null)
            {
                client.AddParameter("authorId", authorId);
            }
            if (ganreId != null)
            {
                client.AddParameter("ganreId", ganreId);
            }
            if (page != 0)
            {
                client.AddParameter("page", page.ToString());
            }
            CatalogViewModel catalogViewModel = client.Get();
            return View(catalogViewModel);
        }

        [HttpGet]
        public IActionResult ViewBook(string bookId)
        {
            WebClient<BookViewModel> client = new WebClient<BookViewModel>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5185;
            client.Path = "api/Guest/ViewBook";
            client.AddParameter("bookId", bookId);
            BookViewModel bookViewModel = client.Get();
            return View(bookViewModel);
        }


        [HttpGet]
        public IActionResult ViewRegistrationForm()
        {
            WebClient<RegistrationViewModel> client = new WebClient<RegistrationViewModel>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5185;
            client.Path = "api/Guest/GetRegistrationViewModel";
            RegistrationViewModel registrationViewModel = client.Get();
            return View(registrationViewModel);
        }

        [HttpPost]
        public IActionResult ReaderRegistration(Reader reader, IFormFile file)
        {
            if (ModelState.IsValid == false)
            {
                return View("ViewRegistrationForm", GetRegistrationViewModel(reader));
            }
            bool ok = PostReader(reader, file);
            if (ok == true)
            {
                HttpContext.Session.SetString("readerId", reader.ReaderId);
                return RedirectToAction("HomePage", "guest");
            }
            ViewBag.Message = "Registration failed. Try again.";
            return View("ViewRegistrationForm", GetRegistrationViewModel(reader));

        }

        private RegistrationViewModel GetRegistrationViewModel(Reader reader)
        {
            WebClient<RegistrationViewModel> client = new WebClient<RegistrationViewModel>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5185;
            client.Path = "api/Guest/GetRegistrationViewModel";
            RegistrationViewModel registrationViewModel = client.Get();
            registrationViewModel.Reader = reader;
            return registrationViewModel;
        }

        private bool PostReader(Reader reader)
        {
            WebClient<Reader> clientReader = new WebClient<Reader>();
            clientReader.Scheme = "http";
            clientReader.Host = "localhost";
            clientReader.Port = 5185;
            clientReader.Path = "api/Guest/RegisterReader";
            return clientReader.Post(reader);
        }
        private bool PostReader(Reader reader, IFormFile file)
        {
            WebClient<Reader> clientReader = new WebClient<Reader>();
            clientReader.Scheme = "http";
            clientReader.Host = "localhost";
            clientReader.Port = 5185;
            clientReader.Path = "api/Guest/RegisterReader";
            return clientReader.Post(reader,file.OpenReadStream());
        }


        [HttpGet]
        public PartialViewResult BookListView(string authorId = null, string ganreId = null, int page = 1)
        {
            // 1/get data from WS
            WebClient<CatalogBooks> client = new WebClient<CatalogBooks>();
            client.Scheme = "http";
            client.Host = "localhost";
            client.Port = 5185;
            client.Path = "api/Guest/GetBookList";

            if (authorId != null)
            {
                client.AddParameter("authorId", authorId);
            }
            if (ganreId != null)
            {
                client.AddParameter("ganreId", ganreId);
            }
            if (page != 0)
            {
                client.AddParameter("page", page.ToString());
            }
            CatalogBooks catalogBooks = client.Get();
            return View(catalogBooks);
        }

    }
}
