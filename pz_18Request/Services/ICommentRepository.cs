using pz_18Request.Models;

public interface ICommentRepository
{
    Task<List<Comment>> GetCommentAsync();
    Task<List<Comment>> GetCommentByRequestAsync(int requestId); 
}
