using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.Data;
using Notebook.Models;


namespace Notebook.Controllers
{
    public class HomeShortController : Controller
    {
        readonly NotebookContext clientsContext;
        private DataApi dataApi;
        public HomeShortController(NotebookContext context, DataApi dApi)
        {
            clientsContext = context;
            dataApi = dApi;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(dataApi.GetClients());
        }

        public async Task<IActionResult> GetInformation(int id)
        {
            if (id != null)
            {
                Client? client = dataApi.GetClientById(id);
                return View(client);
            }
            return NotFound();
        }
    }
}
