using pz_18Request.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRequestRepository
{
    Task<List<Client>> GetClientsAsync(); // Получить список клиентов
    Task<List<RequestStatus>> GetRequestStatusesAsync(); // Получить список статусов заявки
    Task<List<DeviceModel>> GetDeviceModelsAsync(); // Получить список моделей устройств
    Task<Request> AddRequestAsync(Request request);
    Task<Request> UpdateRequestAsync(Request request);
    Task<List<Request>> RefreshRequestsAsync();
    Task<Request> GetRequstByIdAsync(int requestId);
}
