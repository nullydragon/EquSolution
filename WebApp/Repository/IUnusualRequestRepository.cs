using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repository
{
    public interface IRequestRepository
    {
        void Create(UnusualRequests item);

        IEnumerable<UnusualRequests> GetAll();
        UnusualRequests GetById(int id);

        void Update(UnusualRequests item);
        void Remove(int Id);

    }
}
