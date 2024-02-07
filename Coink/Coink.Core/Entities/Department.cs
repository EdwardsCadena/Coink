using System;
using System.Collections.Generic;

namespace Coink.Core.Entities;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Municipality> Municipalities { get; } = new List<Municipality>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
