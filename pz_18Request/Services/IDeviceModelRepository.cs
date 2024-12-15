using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pz_18Request.Models;

namespace pz_18Request.Services
{
    // Интерфейс IDeviceModelRepository
    public interface IDeviceModelRepository
    {
        Task<List<DeviceModel>> GetDeviceModelsAsync();
    }

}
