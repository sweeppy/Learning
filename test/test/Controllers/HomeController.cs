using Microsoft.AspNetCore.Mvc;
using Notebook.Models;
using Microsoft.EntityFrameworkCore;
using Notebook.Data;

namespace Notebook.Controllers
{
    public class HomeController : Controller
    {
        NotebookContext clientsContext;
        /// <summary>
        /// Передача данных из БД
        /// </summary>
        /// <param name="context"></param>
        public HomeController(NotebookContext context)
        {
            clientsContext = context;
        }
        /// <summary>
        /// Передача данных на главную страницу
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> IndexAsync()
        {
            return View(await clientsContext.Clients.ToListAsync());
        }
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Client client = new Client { Id = id.Value };
                clientsContext.Entry(client).State = EntityState.Deleted;
                await clientsContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        /// <summary>
        /// Возвращает форму с данными объекта, которые пользователь может отредактировать
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Client? client = await clientsContext.Clients.FirstOrDefaultAsync(p => p.Id==id);
                if (client != null) return View(client);
            }
            return NotFound();
        }
        /// <summary>
        /// Получает отредактированные данные в виде объекта Client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Client client)
        {
            if (Checks.CheckNullInput(client))
            {
                clientsContext.Clients.Update(client);
                await clientsContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {  
                return View(client);
            }
                
        }
            
    }
}
