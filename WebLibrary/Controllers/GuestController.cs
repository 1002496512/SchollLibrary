using Microsoft.AspNetCore.Mvc;
using LibraryModels;
using WebApiClient;
using System.Net;

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
          
            if(authorId != null)
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

    }
}
