using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.Entity.Model.Vet
{
    [Table("TypePet", Schema = "Vet")]
    public class TypePetEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string TypePet { get; set; }
    }
}
