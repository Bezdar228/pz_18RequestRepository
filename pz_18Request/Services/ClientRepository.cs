using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pz_18Request.Models;

namespace pz_18Request.Services
{
    // Реализация ClientRepository
    public class ClientRepository : IClientRepository
    {
        private readonly RegApplicationContext _context;

        public ClientRepository(RegApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }
    }

}
