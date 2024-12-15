using Microsoft.EntityFrameworkCore;
using pz_18Request.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pz_18Request.Services
{
    public class RequestRepository : IRequestRepository
    {
        readonly RegApplicationContext _context = new RegApplicationContext();

        public event Action RequestsUpdated;

        public async Task<Request> AddRequestAsync(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<List<DeviceModel>> GetDeviceModelsAsync()
        {
            return await _context.DeviceModels.ToListAsync();
        }

        public async Task<List<Request>> GetRequestAsync()
        {
            // Возвращаем список всех заявок
            return await _context.Requests.ToListAsync();
        }

        public async Task<List<Request>> RefreshRequestsAsync()
        {
            // Перезагружаем заявки из базы данных
            return await _context.Requests.ToListAsync();
        }

        public Task<Request> GetRequstByIdAsync(int requestId)
        {
            return _context.Requests.FirstOrDefaultAsync(x => x.RequestId == requestId);
        }

        public async Task<Request> UpdateRequestAsync(Request request)
        {
            if (!_context.Requests.Local.Any(x => x.RequestId == request.RequestId))
            {
                _context.Requests.Attach(request);
            }
            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return request;
        }

        // Реализация метода получения списка клиентов
        public async Task<List<Client>> GetClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        // Реализация метода получения списка статусов заявки
        public async Task<List<RequestStatus>> GetRequestStatusesAsync()
        {
            return await _context.RequestStatuses.ToListAsync();
        }
    }
}
