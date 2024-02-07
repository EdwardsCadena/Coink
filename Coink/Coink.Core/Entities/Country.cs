using System;
using System.Collections.Generic;

namespace Coink.Core.Entities;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Department> Departments { get; } = new List<Department>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
