using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model;
using MyVet.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyVet.Domain.Services
{
    public class RolServices : IRolServices
    {
        private readonly IUnitOfWork _unitOfWork;

        //Inyeccionde la unidad de trabajo
        public RolServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Traer la lista de roles
        public List<RolEntity> GetAll() => _unitOfWork.RolRepository.GetAll().ToList();

        //Es el mismo metodo que arriba pero de la manera tradicional
        //public List<RolEntity> GetAll()
        //{
        //    return _unitOfWork.RolRepository.GetAll().ToList();
        //}


    }
}
