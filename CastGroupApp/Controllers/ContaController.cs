using CastGroupApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CastGroupApp.Controllers
{
    public class ContaController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<ContaViewModel> conta = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7056/api/");

                //HTTP GET
                var responseTask = client.GetAsync("conta");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ContaViewModel>>();
                    readTask.Wait();
                    conta = readTask.Result;
                }
                else
                {
                    conta = Enumerable.Empty<ContaViewModel>();
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro");
                }
                return View(conta);
            }
        }
    }
}
