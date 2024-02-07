using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coink.Core.DTOs
{
    public class MunicipalityDTOs
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
