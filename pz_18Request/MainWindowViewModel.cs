using pz_18Request.Models;
using pz_18Request.Services;
using pz_18Request.ViewModel;
using System;
using Unity;

namespace pz_18Request
{
    internal class MainWindowViewModel : BindableBase
    {
        private RequestListViewModel _requestListVM;
        private AddEditRequestViewModel _addEditRequestVM;
        private CommentListViewModel _commentListVM;

        private IRequestRepository _requestRepository = new RequestRepository();
        public MainWindowViewModel()
        {
            NavigationCommand = new RelayCommand<string>(OnNavigation);
            _requestListVM = new RequestListViewModel(new RequestRepository());
            _addEditRequestVM = RepoContainer.Container.Resolve<AddEditRequestViewModel>();
            _requestListVM.AddRequestRequested += NavigationAddRequest;
            _requestListVM.EditRequestRequested += NavigationUpdateRequest;
            _addEditRequestVM.Done += OnCancelDone;
            _requestListVM.CheckCommentsRequested += NavigationToCommentRequest;
            _commentListVM = new CommentListViewModel(new CommentRepository());
            _commentListVM = RepoContainer.Container.Resolve<CommentListViewModel>();
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
                case "commentList":
                    CurrentViewModel = _commentListVM; break;
            }
        }

        private void NavigationUpdateRequest(Request request)
        {
            _addEditRequestVM.isEditMode = true;
            _addEditRequestVM.SetCustomer(request); // Устанавливаем редактируемую заявку
            CurrentViewModel = _addEditRequestVM;  // Переключаемся на AddEditRequestViewModel
        }

        private void NavigationAddRequest()
        {
            _addEditRequestVM.SetNewRequest(); // Инициализация новой заявки
            _addEditRequestVM.isEditMode = false; // Переключение в режим добавления
            CurrentViewModel = _addEditRequestVM; // Переход на AddEditRequestViewModel
        }

        private void OnCancelDone()
        {
            CurrentViewModel = _requestListVM; // Возврат к списку заявок
        }

        private void NavigationToCommentRequest(Request request)
        {
            _commentListVM.LoadCommentRequest(request.RequestId);
            CurrentViewModel = _commentListVM;
        }
    }
}
