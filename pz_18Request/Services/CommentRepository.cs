using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pz_18Request.Models;

namespace pz_18Request.Services
{
    public class CommentRepository : ICommentRepository
    {
        private readonly RegApplicationContext _context;

        public CommentRepository()
        {
            _context = new RegApplicationContext();
        }

        // Реализация метода для получения всех комментариев
        public async Task<List<Comment>> GetCommentAsync()
        {
            return await _context.Comments
                                 .OrderBy(c => c.CommentDate)
                                 .ToListAsync();
        }


        // Реализация метода для получения комментариев по ID заявки
        public async Task<List<Comment>> GetCommentByRequestAsync(int requestId)
        {
            return await _context.Comments
                                 .Where(c => c.RequestId == requestId)
                                 .OrderBy(c => c.CommentDate)
                                 .ToListAsync();
        }
    }
}
