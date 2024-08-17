using DomainLayer.DomainModels;
using DomainLayer.Interfaces;
using RepositoryLayer.Contexts;
using RepositoryLayer.Repositories;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly SystemContext _context = new SystemContext();
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper mapper;

        public QuoteService()
        {
            _unitOfWork = new UnitOfWork(_context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Quotes, QuoteDTO>();
                cfg.CreateMap<QuoteDTO, Quotes>();
            });
            mapper = new Mapper(config);
        }

        public QuoteDTO GetQuote(int id)
        {
            var quoteDTO = mapper.Map<QuoteDTO>(_unitOfWork.QuoteRepo.GetByQuoteID(id));
            return quoteDTO;
        }

        public IEnumerable<QuoteDTO> GetQuotes()
        {
            var quoteDTOs = mapper.Map<List<QuoteDTO>>(_unitOfWork.QuoteRepo.GetAll());
            return quoteDTOs;
        }

        public void AddQuote(QuoteDTO quoteDTO)
        {
            var quote = mapper.Map<Quotes>(quoteDTO);
            _unitOfWork.QuoteRepo.Insert(quote);
            _unitOfWork.commit();
        }

        public void EditQuote(int id, QuoteDTO quoteDTO)
        {

            var quote = _unitOfWork.QuoteRepo.GetByQuoteID(id);
            mapper.Map(quoteDTO, quote);
            if(quote != null)
            {
                _unitOfWork.QuoteRepo.Update(quote);
                _unitOfWork.commit();
            }
        }

        public void RemoveQuote(int id)
        {
            var quote = _unitOfWork.QuoteRepo.GetByQuoteID(id);
            _unitOfWork.QuoteRepo.Delete(quote);
            _unitOfWork.commit();
        }

        public int GetNextAvailableID()
        {
            var existingIDs = _unitOfWork.QuoteRepo.GetAllIDs().OrderBy(id => id).ToList();
            int nextId = 1;

            foreach (var id in existingIDs)
            {
                if (id == nextId)
                {
                    nextId++;
                }
                else
                {
                    break;
                }
            }

            return nextId;
        }
    }
}