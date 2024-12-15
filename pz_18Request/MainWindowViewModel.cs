using pz_18Request.Models;
using pz_18Request.Services;
using pz_18Request.ViewModel;
using System;
using Unity;

namespace pz_18Request
{
    internal class MainWindowViewModel : BindableBase
    {
        private readonly RequestListViewModel _requestListVM;
        private readonly AddEditRequestViewModel _addEditRequestVM;
        private readonly CommentListViewModel _commentListVM;

        private readonly IRequestRepository _requestRepository;
        private readonly ICommentRepository _commentRepository;

        public MainWindowViewModel()
        {
            // Инициализация репозиториев и ViewModel через контейнер зависимостей
            _requestRepository = RepoContainer.Container.Resolve<IRequestRepository>();
            _commentRepository = RepoContainer.Container.Resolve<ICommentRepository>();
            _requestListVM = RepoContainer.Container.Resolve<RequestListViewModel>();
            _addEditRequestVM = RepoContainer.Container.Resolve<AddEditRequestViewModel>();
            _commentListVM = RepoContainer.Container.Resolve<CommentListViewModel>();

            // Подписка на события
            SubscribeToEvents();

            // Установка начального представления
            CurrentViewModel = _requestListVM;

            // Инициализация команды навигации
            NavigationCommand = new RelayCommand<string>(OnNavigation);
        }

        public BindableBase CurrentViewModel { get; set; }

        public RelayCommand<string> NavigationCommand { get; }

        // Подписка на события ViewModel
        private void SubscribeToEvents()
        {
            _requestListVM.AddRequestRequested += NavigationAddRequest;
            _requestListVM.EditRequestRequested += NavigationUpdateRequest;
            _addEditRequestVM.Done += OnCancelDone;
            _requestListVM.CheckCommentsRequested += NavigationToCommentRequest;
        }

        // Навигация по контенту
        private void OnNavigation(string destination)
        {
            switch (destination)
            {
                case "requestList":
                    SetCurrentViewModel(_requestListVM);
                    break;

                case "commentList":
                    SetCurrentViewModel(_commentListVM);
                    _commentListVM.LoadComment();
                    break;

                default:
                    SetCurrentViewModel(_requestListVM);
                    break;
            }
        }

        // Метод для переключения представления
        private void SetCurrentViewModel(BindableBase viewModel)
        {
            CurrentViewModel = viewModel;
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        // Навигация для редактирования заявки
        private void NavigationUpdateRequest(Request request)
        {
            _addEditRequestVM.isEditMode = true;
            _addEditRequestVM.SetCustomer(request);
            SetCurrentViewModel(_addEditRequestVM);
        }

        // Навигация для добавления новой заявки
        private void NavigationAddRequest()
        {
            _addEditRequestVM.SetNewRequest();
            _addEditRequestVM.isEditMode = false;
            SetCurrentViewModel(_addEditRequestVM);
        }

        // Завершение редактирования и возврат к списку заявок
        private void OnCancelDone()
        {
            SetCurrentViewModel(_requestListVM);
        }

        // Навигация на список комментариев для заявки
        private void NavigationToCommentRequest(Request request)
        {
            _commentListVM.LoadCommentRequest(request.RequestId);
            SetCurrentViewModel(_commentListVM);
        }
    }
}
