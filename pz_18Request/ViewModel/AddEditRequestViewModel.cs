using System.Collections.ObjectModel;
using System.Threading.Tasks;
using pz_18Request.Models;
using pz_18Request.Services;
using pz_18Request.ViewModel;

namespace pz_18Request.ViewModel
{
    public class AddEditRequestViewModel : ValidableBindableBase
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IRequestStatusRepository _requestStatusRepository;
        private readonly IDeviceModelRepository _deviceModelRepository;

        // Событие, которое будет сигнализировать о завершении операции
        public event Action Done;

        public AddEditRequestViewModel(IRequestRepository requestRepository,
                                       IClientRepository clientRepository,
                                       IRequestStatusRepository requestStatusRepository,
                                       IDeviceModelRepository deviceModelRepository)
        {
            _requestRepository = requestRepository;
            _clientRepository = clientRepository;
            _requestStatusRepository = requestStatusRepository;
            _deviceModelRepository = deviceModelRepository;


            SaveCommand = new RelayCommand(SaveRequest);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Свойства для коллекций и выбранных элементов
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<RequestStatus> RequestStatuses { get; set; }
        public ObservableCollection<DeviceModel> DeviceModels { get; set; }

        private Request _request;
        public Request Request
        {
            get => _request;
            set => SetProperty(ref _request, value);
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value);
        }

        private RequestStatus _selectedRequestStatus;
        public RequestStatus SelectedRequestStatus
        {
            get => _selectedRequestStatus;
            set => SetProperty(ref _selectedRequestStatus, value);
        }

        private DeviceModel _selectedDeviceModel;
        public DeviceModel SelectedDeviceModel
        {
            get => _selectedDeviceModel;
            set => SetProperty(ref _selectedDeviceModel, value);
        }

        // Команды
        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }

        // Метод для загрузки данных
        public async Task LoadDataAsync()
        {
            Clients = new ObservableCollection<Client>(await _clientRepository.GetClientsAsync());
            RequestStatuses = new ObservableCollection<RequestStatus>(await _requestStatusRepository.GetRequestStatusesAsync());
            DeviceModels = new ObservableCollection<DeviceModel>(await _deviceModelRepository.GetDeviceModelsAsync());

            Request = new Request();
        }

        // Метод для сохранения заявки
        private async void SaveRequest()
        {
            try
            {
                if (Request.RequestId == 0)
                {
                    await _requestRepository.AddRequestAsync(Request);
                }
                else
                {
                    await _requestRepository.UpdateRequestAsync(Request);
                }

                // После завершения операции вызываем событие
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении заявки: {ex.Message}");
            }
        }

        // Метод для отмены
        private void Cancel()
        {
            // Вызываем событие Done, если нужно отменить изменения
            Done?.Invoke();
        }

        // Метод для установки данных заявки при редактировании
        public void SetCustomer(Request request)
        {
            Request = request ?? new Request();
            isEditMode = true;
        }

        // Метод для инициализации новой заявки
        public void SetNewRequest()
        {
            Request = new Request();
            isEditMode = false;
        }

        private bool _isEditMode;
        public bool isEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
        }
    }
}
