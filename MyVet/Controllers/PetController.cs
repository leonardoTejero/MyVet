using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyVet.Domain.Dto;
using MyVet.Domain.Services.Interface;
using MyVet.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Common.Utils.Constant.Const;

namespace MyVet.Controllers
{
    [Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class PetController : Controller
    {
        #region Attribute
        private readonly IPetServices _petServices;
        #endregion

        #region Buider
        public PetController(IPetServices petServices)
        {
            _petServices = petServices;
        }
        #endregion

        #region Methods

        [HttpGet]
        public IActionResult Index()
        {
            //obtener el id del usuario que esta guardado en los Claims
            //---var user = HttpContext.User;
            //---string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;

            //--List<PetDto> list = _petServices.GetAllMyPets(Convert.ToInt32(idUser));
            return View(); //--list
        }

        [HttpGet]
        public IActionResult GetAllMyPets()
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;

            List<PetDto> list = _petServices.GetAllMyPets(Convert.ToInt32(idUser));
            return Ok(list);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePet(int idPet)
        {
            ResponseDto response = await _petServices.DeletePetAsync(idPet);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllSexs()
        {
            List<SexDto> response = _petServices.GetAllSexs();
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllTypePet()
        {
            List<TypePetDto> response = _petServices.GetAllTypePet();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPet(PetDto pet)
        {
            //Traer el id del usuario
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;
            pet.IdUser = Convert.ToInt32(idUser);

            bool response = await _petServices.InsertPetAsync(pet);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePet(PetDto pet)
        {
            bool response = await _petServices.UpdatePetAsync(pet);
            return Ok(response);
        }

        #endregion
    }
}
