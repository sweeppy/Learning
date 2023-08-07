using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.Data;
using Notebook.Models;


namespace Notebook.Controllers
{
    public class HomeShortController : Controller
    {
        NotebookContext clientsContext;
        public HomeShortController(NotebookContext context)
        {
            clientsContext = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await clientsContext.Clients.ToListAsync());
        }

        public async Task<IActionResult> GetInformation(int id)
        {
            if (id != null)
            {
                Client? client = await clientsContext.Clients.FirstOrDefaultAsync(p => p.Id == id);
                return View(client);
            }
            return NotFound();
        }
    }
}
