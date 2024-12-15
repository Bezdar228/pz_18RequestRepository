﻿using pz_18Request.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_18Request.Services
{
    public interface ICommentRepository
    {
        //вывод комментариев к заявкам
        Task<List<Comment>> GetCommentAsync();

        //вывод комментария определенной заявки
        Task<List<Request>> GetCommentByRequestAsync(int requestId);

        Task<Comment> GetCommentByIdAsync(int customerId);
    }
}
