using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyVet.Domain.Dto
{
    public class ServicesDto
    {
        [Key]
        public int Id { get; set; }
        public string Services { get; set; }
        public string Description { get; set; }
    }
}
