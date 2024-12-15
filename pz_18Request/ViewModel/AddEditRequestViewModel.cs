using pz_18Request.Models;
using pz_18Request.Services;
using System;
using System.Threading.Tasks;

namespace pz_18Request.ViewModel
{
    public class AddEditRequestViewModel : BindableBase
    {
        private readonly IRequestRepository _repository;

        public AddEditRequestViewModel(IRequestRepository repository)
        {
            _repository = repository;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }

        private bool _isEditMode;
        public bool isEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
        }

        private Request _editRequest = new Request();
        public Request EditRequest
        {
            get => _editRequest;
            set => SetProperty(ref _editRequest, value);
        }

        private ValidationRequest _validationRequest = new ValidationRequest();
        public ValidationRequest ValidationRequest
        {
            get => _validationRequest;
            set
            {
                if (_validationRequest != null)
                    _validationRequest.ErrorsChanged -= OnCanExecuteChanges;

                SetProperty(ref _validationRequest, value);

                if (_validationRequest != null)
                    _validationRequest.ErrorsChanged += OnCanExecuteChanges;

                SaveCommand.OnCanExecuteChanged();
            }
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public event Action Done;


        /// Проверка доступности команды SaveCommand
 
        private bool CanSave()
        {
            return ValidationRequest != null && !ValidationRequest.HasErrors;
        }

    
        /// Логика команды SaveCommand

        private async void OnSave()
        {
            try
            {
                if (isEditMode)
                {
                    // Обновление существующей заявки
                    UpdateRequest(ValidationRequest, EditRequest);
                    await _repository.UpdateRequestAsync(EditRequest);
                }
                else
                {
                    // Добавление новой заявки
                    UpdateRequest(ValidationRequest, EditRequest);
                    await _repository.AddRequestAsync(EditRequest);
                }

                Done?.Invoke(); // Уведомляем о завершении
            }
            catch (Exception ex)
            {
                // Логирование ошибки или уведомление пользователя
                Console.WriteLine($"Ошибка при сохранении заявки: {ex.Message}");
            }
        }


        /// Логика команды CancelCommand
   
        private void OnCancel()
        {
            Done?.Invoke();
        }

    
        /// Копирование данных из ValidationRequest в Request
   
        private void UpdateRequest(ValidationRequest source, Request target)
        {
            if (source == null || target == null) return;

            target.RequestId = source.RequestId;
            target.DateAdded = source.DateAdded;
            target.DeviceModelId = source.DeviceModelId;
            target.ProblemDescription = source.ProblemDescription;
        }

        /// Обновление доступности SaveCommand при изменении ошибок
        private void OnCanExecuteChanges(object sender, EventArgs e)
        {
            SaveCommand.OnCanExecuteChanged();
        }


        /// Установить данные для редактирования существующего запроса
     
        public void SetCustomer(Request request)
        {
            EditRequest = request ?? new Request();
            isEditMode = true; // Переключение в режим редактирования

            if (ValidationRequest != null)
                ValidationRequest.ErrorsChanged -= OnCanExecuteChanges;

            ValidationRequest = new ValidationRequest();
            ValidationRequest.ErrorsChanged += OnCanExecuteChanges;

            CopyRequest(EditRequest, ValidationRequest);
        }

      
        /// Копирование данных из Request в ValidationRequest
      
        private void CopyRequest(Request source, ValidationRequest target)
        {
            if (source == null || target == null) return;

            target.RequestId = source.RequestId;
            target.DateAdded = source.DateAdded;
            target.DeviceModelId = source.DeviceModelId;
            target.ProblemDescription = source.ProblemDescription;
        }

     
        /// Инициализация новой заявки
    
        public void SetNewRequest()
        {
            EditRequest = new Request
            {
                DateAdded = DateOnly.FromDateTime(DateTime.Now), // Преобразование текущей даты в DateOnly
                StatusId = 1                                     // Статус по умолчанию
            };

            isEditMode = false; // Переключение в режим добавления

            if (ValidationRequest != null)
                ValidationRequest.ErrorsChanged -= OnCanExecuteChanges;

            ValidationRequest = new ValidationRequest();
            ValidationRequest.ErrorsChanged += OnCanExecuteChanges;

            CopyRequest(EditRequest, ValidationRequest);
        }
    }
}
