using DomainLayer.DomainModels;
using DomainLayer.Interfaces;
using RepositoryLayer.Contexts;
using RepositoryLayer.Repositories;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using AutoMapper;
using System.Collections.Generic;

namespace ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly SystemContext _context = new SystemContext();
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper mapper;

        public UserService()
        {
            _unitOfWork = new UnitOfWork(_context);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Users, UserDTO>();
                cfg.CreateMap<UserDTO, Users>();
            });
            mapper = new Mapper(config);
        }

        public UserDTO GetUser(int id)
        {
            var userDTO = mapper.Map<UserDTO>(_unitOfWork.UserRepo.GetById(id));
            return userDTO;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var userDTOs = mapper.Map<List<UserDTO>>(_unitOfWork.UserRepo.GetAll());
            return userDTOs;
        }

        public void Register(UserDTO userDTO)
        {
            var user = mapper.Map<Users>(userDTO);
            _unitOfWork.UserRepo.Insert(user);
            _unitOfWork.commit();
        }
    }
}