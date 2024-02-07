using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coink.Core.DTOs
{
    public class UserDTOs
    {
        [Required]
        [StringLength(50)] // Asume que el nombre tiene una longitud máxima de 50 caracteres
        public string Name { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [StringLength(100)] // Asume que la dirección tiene una longitud máxima de 100 caracteres
        public string? Address { get; set; }

        [Required]
        public int? CountryId { get; set; }

        [Required]
        public int? DepartmentId { get; set; }

        [Required]
        public int? MunicipalityId { get; set; }
    }
}
