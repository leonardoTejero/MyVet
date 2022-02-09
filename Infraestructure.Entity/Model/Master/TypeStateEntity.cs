using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.Entity.Model.Master
{
    [Table("TypeState", Schema = "Master")]
    public class TypeStateEntity
    {
        [Key]
        public int IdTypeState { get; set; }
        public string TypeState { get; set; }
    }
}
