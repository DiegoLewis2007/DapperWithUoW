using DapperTransactionWithUoW.Domain.Entities;
using System.Collections.Generic;

namespace DapperTransactionWithUoW.Domain.Interfaces
{
    public interface IService
    {
        void Add(IEnumerable<User> users);

        IEnumerable<User> GetAll();
    }
}
