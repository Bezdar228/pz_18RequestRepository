﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Windows.Themes;
using pz_18Request.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_18Request.Services
{
    public class RequestRepository : IRequestRepository
    {
        readonly RegApplicationContext _context = new RegApplicationContext();

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

        public Task<List<Request>> GetRequestAsync()
        {
            return _context.Requests.ToListAsync();
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

    }
}
