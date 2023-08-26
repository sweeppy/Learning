using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notebook.Data;
using Notebook.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Notebook.Models;


namespace Notebook.Controllers
{
    public class RoleInitializerController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        private NotebookContext _context;
        public RoleInitializerController(NotebookContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        /// <summary>
        /// Добавление ролей
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index() 
        {
            
            
            if (!_context.Roles.Any(e => e.Name == Roles.user.Name))
            {
                //_context.Add(Roles.user);//добавление роли пользователя
                //_context.Add(Roles.admin);//добавление роли админа
                await _roleManager.CreateAsync(Roles.admin);
                await _roleManager.CreateAsync(Roles.user);
                

                var user = new User { UserName = "Admin" };//Создание аккаунта админа
                var createResult = await _userManager.CreateAsync(user, "_aAA12345");//Добавление админа в список пользователей

                if (createResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.admin.Name);    
                }
                await _context.SaveChangesAsync();
            }

            return Redirect("/Home");
        }
    }
}
