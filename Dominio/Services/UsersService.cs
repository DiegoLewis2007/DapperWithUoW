using DapperTransactionWithUoW.Domain.Entities;
using DapperTransactionWithUoW.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperTransactionWithUoW.Domain.Services
{
    public sealed class UsersService : IService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository repository;

        public UsersService(IUnitOfWork unitOfWork, IRepository repository)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        public void Add(IEnumerable<User> users)
        {
            var user = repository.GetAll();

            this.unitOfWork.BeginTransaction();

            Parallel.ForEach(users, g =>
            {
                lock (new object())
                {
                    unitOfWork.UserRepository.Add(g);
                }
            });

            unitOfWork.Commit();
        }

        public IEnumerable<User> GetAll()
        {
            return repository.GetAll();
        }
    }
}
