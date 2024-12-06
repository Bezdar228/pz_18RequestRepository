using System;
using pz_18Request.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_18Request.Services
{
    internal interface IRequestRepository
    {
        Task<List<Request>> GetRequestAsync();

        Task<Request> GetRequestByIdAsync(int requestId);

        Task<Request> UpdateRequestAsync(Request request);

        Task DeleteRequestAsync(int requestId);

        Task<Request> AddRequestAsync(Request request);
    }
}
