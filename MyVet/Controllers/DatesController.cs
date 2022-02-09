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
    public class DatesController : Controller
    {
        #region Attrubute
        private readonly IDateServices _dateServices;
        #endregion

        #region Builder
        public DatesController(IDateServices dateServices)
        {
            _dateServices = dateServices;
        }
        #endregion

        #region Views
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DatesVet()
        {
            return View();
        }
        #endregion

        #region Methods

        [HttpGet]
        public IActionResult GetAllDates()
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;

            List<DatesDto> list = _dateServices.GetAllDates(Convert.ToInt32(idUser));
            return Ok(list);
        }

        [HttpGet]
        public IActionResult GetAllMyDates()
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;

            List<DatesDto> list = _dateServices.GetAllMyDates(Convert.ToInt32(idUser));
            return Ok(list);
        }

        [HttpGet]
        public IActionResult GetAllServices()
        {
            List<ServicesDto> response = _dateServices.GetAllServices();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDate(DatesDto date)
        {
            //Traer el id del usuario
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;
            //date.IdUser = Convert.ToInt32(idUser);

            bool response = await _dateServices.InsertDateAsyn(date);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDate(int idDate)
        {
            ResponseDto response = await _dateServices.DeleteDateAsync(idDate);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDate(DatesDto dates)
        {
            bool result = await _dateServices.UpdateDateAsync(dates);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> CancelDates(int idDates)
        {
            bool result = await _dateServices.CancelDateAsync(idDates, idUserVet: null);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDateVet(DatesDto dates)
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;
            dates.IdUserVet = Convert.ToInt32(idUser);

            bool result = await _dateServices.UpdateDateVetAsync(dates);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> CancelDatesVet(int idDates)
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;
            
            bool result = await _dateServices.CancelDateAsync(idDates, Convert.ToInt32(idUser));
            return Ok(result);
        }


        #endregion
    }
}
