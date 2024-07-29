using DomainLayer.DomainModels;
using DomainLayer.Interfaces;
using RepositoryLayer.Contexts;

namespace RepositoryLayer.Repositories
{
    public class UserRepo : GenericRepo<Users>, IUserRepo
    {
        public UserRepo(SystemContext context) : base(context)
        {
        }
    }
}