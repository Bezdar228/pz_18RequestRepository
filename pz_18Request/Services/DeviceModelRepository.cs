using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pz_18Request.Models;

namespace pz_18Request.Services
{
    // Реализация DeviceModelRepository
    public class DeviceModelRepository : IDeviceModelRepository
    {
        private readonly RegApplicationContext _context;

        public DeviceModelRepository(RegApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<DeviceModel>> GetDeviceModelsAsync()
        {
            return await _context.DeviceModels.ToListAsync();
        }
    }

}
