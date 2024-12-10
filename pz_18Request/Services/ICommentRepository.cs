using pz_18Request.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_18Request.Services
{
    internal interface ICommentRepository
    {
        //вывод комментариев к заявкам
        Task<List<Comment>> GetCommentAsync();

        //вывод комментария определенной заявки
        Task<List<Comment>> GetCommentByRequestAsync(int requestId);
    }
}
