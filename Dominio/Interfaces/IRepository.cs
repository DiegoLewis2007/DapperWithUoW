using DapperTransactionWithUoW.Domain.Entities;
using System.Collections.Generic;

namespace DapperTransactionWithUoW.Domain.Interfaces
{
    public interface IRepository
    {
        void Add(User entity);

        void Update(User entity);

        IEnumerable<User> GetAll();
    }
}
