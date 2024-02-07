using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coink.Core.DTOs
{
    public class CountryDTOs
    {
        [StringLength(50)]
        public string Name { get; set; }
    }
}
