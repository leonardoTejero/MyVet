using Infraestructure.Entity.Model;
using MyVet.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyVet.Domain.Services.Interface
{
    public interface IUserServices
    {
        #region Methods auth
        ResponseDto Login(UserDto user);
        Task<ResponseDto> Register(UserDto user);
        #endregion

        #region Methods Crud
        List<UserEntity> GetAll();

        UserEntity GetUser(int idUser);

        Task<bool> UpdateUser(UserEntity user);


        Task<bool> DeleteUser(int idUser);
        Task<ResponseDto> CreateUser(UserEntity user);
        
        #endregion
    }
}
