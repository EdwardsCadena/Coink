using System;
using System.Collections.Generic;

namespace Coink.Core.Entities;

public partial class Municipality
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
