using pz_18Request.Models;
using pz_18Request.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_18Request.ViewModel
{
    class RequestListViewModel : BindableBase
    {
        private IRequestRepository _repository;

        public RequestListViewModel(IRequestRepository repository)
        {
            _repository = repository;
            Requests = new ObservableCollection<Request>();
            AddRequestCommand = new RelayCommand(OnAddRequest);
            EditRequestCommand = new RelayCommand<Request>(OnEditRequest);
            LoadRequest();
        }

        private ObservableCollection<Request>? _request;
        public ObservableCollection<Request>? Requests
        {
            get => _request;
            set => SetProperty(ref _request, value);
        }

        private List<Request>? _requestList;

        public async void LoadRequest()
        {
            var updatedRequests = await _repository.RefreshRequestsAsync();
            Requests.Clear(); // Очистка существующей коллекции
            foreach (var request in updatedRequests)
            {
                Requests.Add(request); // Добавление новых заявок
            }
        }

        public RelayCommand AddRequestCommand { get; private set; }
        public RelayCommand<Request> EditRequestCommand { get; private set; }
 
        public event Action AddRequestRequested = delegate { };
        public event Action<Request> EditRequestRequested = delegate { };
        public event Action<Request> CheckCommentsRequested = delegate { };

        private void OnAddRequest()
        {
            AddRequestRequested?.Invoke();
        }

        private void OnEditRequest(Request request)
        {
            EditRequestRequested(request);
        }

    }
}
