using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pz_18Request.Models;

namespace pz_18Request.Services
{
    // Реализация RequestStatusRepository
    public class RequestStatusRepository : IRequestStatusRepository
    {
        private readonly RegApplicationContext _context;

        public RequestStatusRepository(RegApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<RequestStatus>> GetRequestStatusesAsync()
        {
            return await _context.RequestStatuses.ToListAsync();
        }
    }

}
