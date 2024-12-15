using System.Collections.ObjectModel;
using System.Threading.Tasks;
using pz_18Request.Models;
using pz_18Request.Services;

namespace pz_18Request.ViewModel
{
    public class CommentListViewModel : BindableBase
    {
        private readonly ICommentRepository _repository;

        public CommentListViewModel(ICommentRepository repository)
        {
            _repository = repository;
            Comments = new ObservableCollection<Comment>();
        }

        public ObservableCollection<Comment> Comments { get; private set; }

        public async void LoadComment()
        {
            try
            {
                var commentList = await _repository.GetCommentAsync(); 
                Comments.Clear(); 

                foreach (var comment in commentList)
                {
                    Comments.Add(comment); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке комментариев: {ex.Message}");
            }
        }

        public async void LoadCommentRequest(int requestId)
        {
            try
            {
                var commentList = await _repository.GetCommentByRequestAsync(requestId);
                Comments = new ObservableCollection<Comment>(commentList.OrderBy(c => c.CommentDate));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке комментариев: {ex.Message}");
            }
        }

    }
}
