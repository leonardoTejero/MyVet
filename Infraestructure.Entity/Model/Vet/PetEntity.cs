using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.Entity.Model.Vet
{
    [Table("Pet", Schema = "Vet")]
    public class PetEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateBorns { get; set; }

        [ForeignKey("SexEntity")]
        public int IdSex { get; set; }

        public SexEntity SexEntity { get; set; } // relacion a 1 con tabla SexEntity

        [ForeignKey("TypePetEntity")]
        public int IdTypePet { get; set; }

        public TypePetEntity TypePetEntity { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public UserPetEntity UserPetEntity { get; set; }
    }
}
