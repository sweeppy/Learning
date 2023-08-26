using Microsoft.AspNetCore.Mvc;
using Notebook.Models;
using Notebook.Data;
using Microsoft.AspNetCore.Authorization;

namespace Notebook.Controllers
{
    [Authorize]
    public class AddController : Controller
    {
        public static NotebookContext clientsContext;

        private DataApi dataApi;

        public AddController(NotebookContext context, DataApi dApi)
        {
            clientsContext = context;
            dataApi = dApi;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Проверка ввода и добавление клиента в БД
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        
        public async Task<IActionResult> Check(Client client)
        {

            if (Checks.CheckNullInput(client))//Проверка на пустой ввод
            {

                if (client.PhoneNumber.Length != 11 || !ulong.TryParse(client.PhoneNumber, out _))//проверка номера телефона
                {
                    ModelState.AddModelError("PhoneNumber", "Wrong phone number");//обавление ошибки, если номер некорректно введен
                    if (!ModelState.IsValid)
                    {
                        return View("Index");
                    }
                }
                else//Добавление клмента и сохранение в БД
                {
                    dataApi.AddClient(client);
                    await clientsContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }
            return View("Index");
        }
    }
}
