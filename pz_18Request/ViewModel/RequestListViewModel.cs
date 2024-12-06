using System;
using pz_18Request.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using pz_18Request.Models;

namespace pz_18Request.ViewModel
{
    internal class RequestListViewModel : BindableBase
    {
        private IRequestRepository _repository;

        public RequestListViewModel(IRequestRepository repository)
        {
            _repository = repository;
            Requests = new ObservableCollection<Request>();
            LoadRequest();
        }

        private ObservableCollection<Request>? _requests;

        public ObservableCollection<Request>? Requests
        {
            get => _requests;
            set => SetProperty(ref _requests, value);
        }
        private List<Request>? _requestList;

        public async void LoadRequest()
        {
            _requestList = await _repository.GetRequestAsync();
            Requests = new ObservableCollection<Request>(_requestList);
        }
    }
}
