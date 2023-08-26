using Microsoft.AspNetCore.Mvc;
using Notebook.Models;
using Microsoft.EntityFrameworkCore;
using Notebook.Data;
using Microsoft.AspNetCore.Authorization;

namespace Notebook.Controllers
{
    public class HomeController : Controller
    {
        readonly NotebookContext clientsContext;

        private DataApi dataApi;
        public HomeController(NotebookContext context, DataApi dApi)
        {
            clientsContext = context;
            dataApi = dApi;
        }
        /// <summary>
        /// Передача данных на главную страницу
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(dataApi.GetClients());
        }
        /// <summary>
        /// Удаление пользователя (только админ)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                dataApi.DeleteClient(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        /// <summary>
        /// Возвращает форму с данными объекта, которые админ может отредактировать
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
                Client? client = dataApi.GetClientById(id);
                if (client != null) return View(client);

            return NotFound();
        }
        /// <summary>
        /// Обновляет БД отредактированными данными
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost][Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Client client)
        {
            if (Checks.CheckNullInput(client))
            {
                await dataApi.FinishEdit(client);
                return RedirectToAction("Index");
            }
            else
            {  
                return View(client);
            }
                
        }
            
    }
}
