using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IOwnerRepository _ownerRepository;
        private IAccountRepository _accountRepository;
        private IUserRepository _userRepository;
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public IOwnerRepository Owner
        {
            get {
                if (_ownerRepository == null)
                {
                    _ownerRepository = new  OwnerRepository(_repoContext);
                }
                return _ownerRepository; 
            }
        }

        public IAccountRepository Account
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_repoContext);
                }
                return _accountRepository;
            }
            
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_repoContext);
                }
                return _userRepository;
            }

        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
