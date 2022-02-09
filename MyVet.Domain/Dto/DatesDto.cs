using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyVet.Domain.Dto
{
    public class DatesDto
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La fecha es requerida")]
        [Display(Name = "Fecha Cita")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "El Contacto es requerido")]
        [MaxLength(100)]
        public string Contact { get; set; }

        [Required(ErrorMessage = "El Servicio es requerido")]
        public int IdServices { get; set; }

        [Required(ErrorMessage = "La mascota es requerida")]
        public int IdPet { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
        [MaxLength(300)]
        public string Observation { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int IdState { get; set; }
        public string State { get; set; }
        public int? IdUserVet { get; set; }
        public string NamePet { get; set; }
        public string Services { get; set; }
        public string StrClosedDate { get; set; }
        public string StrDate { get; set; }

    }
}
