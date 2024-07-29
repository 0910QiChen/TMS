using DomainLayer.DomainModels;
using DomainLayer.Interfaces;
using RepositoryLayer.Contexts;

namespace RepositoryLayer.Repositories
{
    public class QuoteRepo : GenericRepo<Quotes>, IQuoteRepo
    {
        public QuoteRepo(SystemContext context) : base(context)
        {
        }
    }
}