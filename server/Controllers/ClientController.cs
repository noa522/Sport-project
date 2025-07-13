using computerized_sports_course.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interface;
using Service.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Interface; // אם IUserService נמצא שם
using Service.Services;  // אם TokenService נמצא שם
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using computerized_sports_course.Models;
using BCrypt.Net;  // זה מה שדרוש כדי להשתמש ב-BCrypt
// עבור TokenAndName



namespace computerized_sports_course.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;
        private readonly TokenService _tokenService; 
        public ClientController(IClientService service, TokenService tokenService) 
        {
            _service = service;
            _tokenService = tokenService; 
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _service.GetById(id);
            if (client == null)
                return NotFound("Client not found");

            return Ok(client);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] UserLogin userLogin)
        {
            var users = await _service.GetAll(); // מחזיר רשימה של כל המשתמשים
            var user = users.FirstOrDefault(c => c.Mail == userLogin.Mail); // מחפש משתמש מתאים

            if (user == null || !BCrypt.Net.BCrypt.Verify(CleanPhoneNumber(userLogin.Password), user.PhoneNumber))
            {
                return Unauthorized(new { message = "אימייל או סיסמה שגויים" });
            }

            // יצירת טוקן למשתמש בעזרת הפונקציה ב-ClientService
            var token = _service.Generate(user);

            // החזרת נתוני המשתמש עם הטוקן
            return Ok(new
            {
                id = user.Id,
                name = user.Name,
                email = user.Mail,
                token = token
            });
        }



        // ✅ הרשמה עם הצפנת סיסמה
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] Client client)
        {
            if (client == null)
                return BadRequest("Invalid data");

            var existingUser = await _service.GetByEmailAndPassword(client.Mail, client.PhoneNumber);
            if (existingUser != null)
                return Conflict("כבר קיים משתמש עם מייל או סיסמא זהה");

            client.PhoneNumber = BCrypt.Net.BCrypt.HashPassword(client.PhoneNumber);
            var newClient = await _service.AddItem(client);

            return Ok(newClient);
        }

        // ✅ עדכון לקוח (רק משתמש מחובר)
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
        {
            if (id != client.Id)
                return BadRequest("Mismatched client ID");

            await _service.UpdateItem(id, client);
            return NoContent();
        }

        // ✅ מחיקת לקוח (רק משתמש מחובר)
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _service.DeleteItem(id);
            return NoContent();
        }

        private string CleanPhoneNumber(string phone)
        {
            return System.Text.RegularExpressions.Regex.Replace(phone.Trim(), @"\D", "");
        }

    }
}
