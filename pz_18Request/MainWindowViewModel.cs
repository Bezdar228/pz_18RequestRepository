using pz_18Request.Models;
using pz_18Request.Services;
using pz_18Request.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace pz_18Request
{
    internal class MainWindowViewModel : BindableBase
    {
        private RequestListViewModel _requestListVM;
        private AddEditRequestViewModel _addEditRequestVM;

        private IRequestRepository _requestRepository = new RequestRepository();
        public MainWindowViewModel()
        {
            NavigationCommand = new RelayCommand<string>(OnNavigation);
            _requestListVM = new RequestListViewModel(new RequestRepository());
            _addEditRequestVM = RepoContainer.Container.Resolve<AddEditRequestViewModel>();
            _requestListVM.AddRequestRequested += NavigationAddRequest;
            _requestListVM.EditRequestRequested += NavigationUpdateRequest;
            _addEditRequestVM.Done += OnCancelDone;
        }

        public BindableBase _currentViewModel;
        public BindableBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }
        public RelayCommand<string> NavigationCommand { get; private set; }

        private void OnNavigation(string dest)
        {
            switch (dest)
            {
                case "requestList":
                    CurrentViewModel = _requestListVM; break;
            }
        }

        private void NavigationUpdateRequest(Request request)
        {
            _addEditRequestVM.isEditMode = true;    
            _addEditRequestVM.SetCustomer(request);
            CurrentViewModel = _addEditRequestVM;
        }
        private void NavigationAddRequest()
        {
            _addEditRequestVM.isEditMode = false;
            CurrentViewModel = _addEditRequestVM;
        }
        private void OnCancelDone()
        {
            CurrentViewModel = _requestListVM;
        }

    }
}
