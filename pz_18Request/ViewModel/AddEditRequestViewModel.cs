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
    public class AddEditRequestViewModel : BindableBase
    {
        private IRequestRepository _repository;

        public AddEditRequestViewModel(IRequestRepository repository)
        {
            _repository = repository;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave);
          
        }
        public bool _isEditMode;

        public bool isEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
        }

        private Request _editRequest;

        private ValidationRequest _validationRequest;
        public ValidationRequest ValidationRequest
        {
            get => _validationRequest;
            set => SetProperty(ref _validationRequest, value);
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public event Action Done;

        private void OnCanExecuteChanges(object sender, EventArgs e)
        {
            SaveCommand.OnCanExecuteChanged();
        }

        private void CopyRequest(Request request, ValidationRequest validRequest)
        {
            validRequest.RequestId = request.RequestId;
            if (isEditMode)
            {
                validRequest.RequestId = request.RequestId;
                validRequest.DateAdded = request.DateAdded;
                validRequest.DeviceModelId = request.DeviceModelId;
                validRequest.ProblemDescription = request.ProblemDescription;
            }
        }
        private async void OnSave()
        {
            if (_editRequest == null) return;

            if (_editRequest.RequestId == 0)
            {
                // Если RequestId равен 0, то создаём новую заявку
                await _repository.AddRequestAsync(_editRequest);
            }
            else
            {
                // Если RequestId не равен 0, то обновляем существующую заявку
                await _repository.UpdateRequestAsync(_editRequest);
            }

            Done?.Invoke();  // Вызов события Done после сохранения
        }

        private void OnCancel()
        {
           
            if (isEditMode)
            {
             
                CopyRequest(_editRequest, ValidationRequest);  
            }
            else
            {
               
                ValidationRequest = new ValidationRequest(); 
            }

            Done?.Invoke(); 
        }

        private void UpdateRequest(ValidationRequest request, Request target)
        {
            target.RequestId = request.RequestId;
            target.DateAdded = request.DateAdded;
            target.DeviceModelId = request.DeviceModelId;
            target.ProblemDescription = request.ProblemDescription;
        }

        

        public void SetCustomer(Request request)
        {
            _editRequest = request;
            if (ValidationRequest != null)
                ValidationRequest.ErrorsChanged -= OnCanExecuteChanges;
            ValidationRequest = new ValidationRequest();
            ValidationRequest.ErrorsChanged += OnCanExecuteChanges;
            CopyRequest(request, ValidationRequest);
        }
        public void SetRequestForEdit(Request request)
        {
            _editRequest = request;
        }
    }

}
