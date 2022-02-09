using Common.Utils.Enums;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model.Vet;
using MyVet.Domain.Dto;
using MyVet.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVet.Domain.Services
{
    public class DateServices : IDateServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public DateServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods

        public List<DatesDto> GetAllMyDates(int idUser)
        {
            var dates = _unitOfWork.DatesRepository.FindAll(x => x.PetEntity.UserPetEntity.IdUser == idUser,
                                                        p => p.PetEntity.UserPetEntity,
                                                        p => p.ServicesEntity,
                                                        p => p.StateEntity).ToList();

            List<DatesDto> list = dates.Select(x => new DatesDto
            {
                
                Id = x.Id,
                AppointmentDate = x.Date,
                Contact = x.Contact,
                IdServices = x.IdServices,
                Services = x.ServicesEntity.Services,
                IdPet = x.IdPet,
                NamePet = x.PetEntity.Name,
                IdState = x.IdState,
                State = x.StateEntity.State,
                Description = x.Description,
                ClosedDate = x.ClosedDate,
                StrDate = x.Date.ToString("yyyy-MM-dd"),
                StrClosedDate = x.ClosedDate == null ? "No disponible" : x.ClosedDate.Value.ToString("yyyy-MM-dd"),
                //agregar el tipo de mascota opcional

            }).OrderBy(f => f.AppointmentDate).ToList();

            return list;
        }

        public List<DatesDto> GetAllDates(int idUser)
        {
            var dates = _unitOfWork.DatesRepository.FindAll(x => (x.IdUserVet == idUser || x.IdUserVet == null)
                                                            && x.IdState != (int)Enums.State.CitaCancelada,
                                                            p => p.PetEntity.UserPetEntity,
                                                            p => p.ServicesEntity,
                                                            p => p.StateEntity).ToList();
             
            var datesDeleteList = dates.Where(x => (x.IdState == (int)Enums.State.CitaCancelada && x.IdUserVet == null)).ToList();

            var datesSelect = (from t in dates
                                 where !datesDeleteList.Any(x => x.Id == t.Id)
                                 select t).ToList();

            List<DatesDto> list = datesSelect.Select(x => new DatesDto
            {

                Id = x.Id,
                AppointmentDate = x.Date,
                Contact = x.Contact,
                IdServices = x.IdServices,
                Services = x.ServicesEntity.Services,
                IdPet = x.IdPet,
                NamePet = x.PetEntity.Name,
                IdState = x.IdState,
                State = x.StateEntity.State,
                Description = x.Description,
                ClosedDate = x.ClosedDate,
                StrDate = x.Date.ToString("yyyy-MM-dd"),
                StrClosedDate = x.ClosedDate == null ? "No disponible" : x.ClosedDate.Value.ToString("yyyy-MM-dd"),
                //agregar el tipo de mascota opcional

            }).OrderBy(f=>f.AppointmentDate).ToList();

            return list;
        }

        public List<ServicesDto> GetAllServices()
        {
            List<ServicesEntity> services = _unitOfWork.ServicesRepository.GetAll().ToList();

            List<ServicesDto> list = services.Select(x => new ServicesDto
            {
                Id = x.Id,
                Services = x.Services,
                Description = x.Description,

            }).ToList();

            return list;
        }

        public async Task<bool> InsertDateAsyn(DatesDto date)
        {

            DatesEntity datesEntity = new DatesEntity()
            {

                Date = date.AppointmentDate,
                Contact = date.Contact,
                IdServices = date.IdServices,
                IdPet = date.IdPet,
                IdState = (int)Enums.State.CitaActiva,
                Description = date.Description,
                          
                
            };

            _unitOfWork.DatesRepository.Insert(datesEntity);
            return await _unitOfWork.Save() > 0;
        }

        public async Task<ResponseDto> DeleteDateAsync(int idDate)
        {
            ResponseDto response = new ResponseDto();

            _unitOfWork.DatesRepository.Delete(idDate);
            response.IsSuccess = await _unitOfWork.Save() > 0;

            if (response.IsSuccess)
                response.Message = "Se elminó correctamente la Mascota";
            else
                response.Message = "Hubo un error al eliminar la cita, por favor vuelva a intentalo";

            return response;
        }

        public async Task<bool> UpdateDateAsync(DatesDto date)
        {
            bool result = false;

            DatesEntity datesEntity = _unitOfWork.DatesRepository.FirstOrDefault(x => x.Id == date.Id);

            if (datesEntity != null) //validar que si exista la cita
            {
                datesEntity.Contact = date.Contact;
                datesEntity.Date = date.AppointmentDate;
                datesEntity.Description = date.Description;
                datesEntity.IdPet = date.IdPet;
                datesEntity.IdServices = date.IdServices;
                datesEntity.IdState = date.IdState;
                datesEntity.Description = date.Description;

                _unitOfWork.DatesRepository.Update(datesEntity);

                result = await _unitOfWork.Save() > 0;
            }

            return result;
        }

        public async Task<bool> UpdateDateVetAsync(DatesDto date)
        {
            bool result = false;

            DatesEntity datesEntity = _unitOfWork.DatesRepository.FirstOrDefault(x => x.Id == date.Id);

            if (datesEntity != null) //validar que si exista la cita
            {
                datesEntity.Contact = date.Contact;
                datesEntity.Date = date.AppointmentDate;
                datesEntity.Description = date.Description;
                datesEntity.IdPet = date.IdPet;
                datesEntity.IdServices = date.IdServices;
                datesEntity.IdState = (int)Enums.State.CitaFinalizada;
                datesEntity.Description = date.Description;
                datesEntity.IdUserVet = date.IdUserVet;
                datesEntity.ClosedDate = DateTime.Now;
                datesEntity.Observation = date.Observation;


                _unitOfWork.DatesRepository.Update(datesEntity);

                result = await _unitOfWork.Save() > 0;
            }

            return result;
        }

        public async Task<bool> CancelDateAsync(int idDate, int? idUserVet)
        {
            bool result = false;

            DatesEntity date = _unitOfWork.DatesRepository.FirstOrDefault(x => x.Id == idDate);
            if (date != null)
            {
                date.IdState = (int)Enums.State.CitaCancelada;
                date.ClosedDate = DateTime.Now;
                date.IdUserVet = idUserVet ?? null; // si es nulo envia el nulo o sino envia el id cualquiera

                _unitOfWork.DatesRepository.Update(date);
                result = await _unitOfWork.Save() > 0;
            }

            return result;
        }


        #endregion

    }
}
