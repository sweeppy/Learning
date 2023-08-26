using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.Authorization;
using Notebook.Data;
using Notebook.Models;

namespace Notebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        NotebookContext data;
        UserManager<User> _userManager;
        public ValuesController(NotebookContext context, UserManager<User> userManager)
        {
            data = context;
            _userManager = userManager;
        }

        [HttpGet]//api/values
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            return await data.Clients.ToListAsync();           
        }

        [HttpGet("{id}")] //api/values/1
        public async Task<ActionResult<Client>> Get(int id)
        {
            Client client = await data.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
                return NotFound();
            return new ObjectResult(client);
        }

        [HttpPost]//api/values
        public async Task<ActionResult<Client>> Post(Client client)
        {
            if (client == null)
                return BadRequest();

            data.Clients.Add(client);
            data.SaveChanges();
            return Ok(client);
        }
        [HttpPut] //api/values
        public async Task<ActionResult<Client>> Put(Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }
            if (!data.Clients.Any(x => x.Id == client.Id))
            {
                return NotFound();
            }

            data.Update(client);
            await data.SaveChangesAsync();
            return Ok(client);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> Delete(int id)
        {
            Client client = data.Clients.FirstOrDefault(x => x.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            data.Clients.Remove(client);
            await data.SaveChangesAsync();
            return Ok(client);
        }
        [Route("Login")] //api/values/Login
        public async Task<ActionResult> Login([FromBody]UserLoginForWPF model)
        {
            var user = await _userManager.FindByNameAsync(model.LoginProp);//Поиск пользователя по логину
            if (user == null)
                return NotFound("User is not found");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);//проверка пароля
            if (!isPasswordValid)
            {
                return BadRequest("Wrong password");//пароль неверный
            }
            return Ok("Success");
            
        }
        [HttpGet]
        [Route("GetUser/{userName}")] //api/values/GetUser/xxx
        public async Task<ActionResult<List<string>>> GetUserRoles(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);//поиск пользователя по логину
            
            if (user == null) return NotFound("User is not found");

            return Ok(new List<string>(await _userManager.GetRolesAsync(user)));
        }

        [HttpPost][Route("Registration")] //api/values/Registration
        public async Task<ActionResult<IdentityResult>> Registration([FromBody]UserRegistration model)
        {
            var user = await _userManager.FindByNameAsync(model.LoginProp);
            if (user != null)
            {
                return BadRequest("User already registred");
            }

            user = new User { UserName = model.LoginProp };

            return await _userManager.CreateAsync(user, model.Password);
        }
    }
}
