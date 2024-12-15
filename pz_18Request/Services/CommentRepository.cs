using Microsoft.EntityFrameworkCore;
using pz_18Request.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_18Request.Services
{
    public class CommentRepository : ICommentRepository
    {
        readonly RegApplicationContext _context = new RegApplicationContext();
        public Task<List<Comment>> GetCommentAsync()
        {
            return _context.Comments.ToListAsync();
        }

        public Task<Comment> GetCommentByIdAsync(int customerId)
        {
            return _context.Comments.FirstOrDefaultAsync(c => c.RequestId == customerId);
        }

        public Task<List<Request>> GetCommentByRequestAsync(int requestId)
        {
            return _context.Requests.Where(x => x.RequestId == requestId).ToListAsync();
        }
    }   
}
