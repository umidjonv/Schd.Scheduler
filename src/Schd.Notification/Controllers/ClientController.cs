using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schd.Common.Response;
using Schd.Notification.Data;
using Schd.Notification.Models;

namespace Schd.Notification.Controllers
{
    public class ClientController :BaseController
    {
        private readonly AppDbContext _db;

        public ClientController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<IActionResult> Add([FromBody] ClientModel clientModel)
        {
            var exist = _db.Clients.Any(x => x.Secret == clientModel.Secret);
            if (!exist)
            {
                var client = new Client
                {
                    Secret = clientModel.Secret,
                    Name = clientModel.Name,
                    IsApproved = false
                };

                _db.Clients.Add(client);
                await _db.SaveChangesAsync();
            }
            else
            {
                return Ok("User already exist");
            }

            return Ok("User added");
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<IActionResult> All()
        {
            var clients = _db.Clients.ToList();
            
            return Ok(clients);
        }

        [HttpDelete]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<IActionResult> Delete(string id)
        {
            var client = await _db.Clients.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            if (client!=null)
            {
                _db.Clients.Remove(client);
                await _db.SaveChangesAsync();



                return Ok("Client deleted");
            }
            
            return Ok("User not found");
        }
    }
}
