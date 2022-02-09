using MyVet.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyVet.Domain.Services.Interface
{
    public interface IDateServices
    {
        List<DatesDto> GetAllMyDates(int idUser);
        List<ServicesDto> GetAllServices();
        Task<bool> InsertDateAsyn(DatesDto date);
        Task<ResponseDto> DeleteDateAsync(int idDate);
        Task<bool> UpdateDateAsync(DatesDto date);
        Task<bool> CancelDateAsync(int idDate, int? idUserVet);
        List<DatesDto> GetAllDates(int idUser);
        Task<bool> UpdateDateVetAsync(DatesDto date);

    }
}
