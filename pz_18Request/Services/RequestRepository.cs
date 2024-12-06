using Microsoft.EntityFrameworkCore;
using Microsoft.Windows.Themes;
using pz_18Request.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_18Request.Services
{
    internal class RequestRepository : IRequestRepository
    {
        readonly RegApplicationContext _context = new RegApplicationContext();
        public Task<Request> AddRequestAsync(Request request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRequestAsync(int requestId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Request>> GetRequestAsync()
        {
            return _context.Requests.ToListAsync();
        }

        public Task<Request> GetRequestByIdAsync(int requestId)
        {
            return _context.Requests.FirstOrDefaultAsync(x => x.RequestId == requestId);
        }

        public Task<Request> UpdateRequestAsync(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
