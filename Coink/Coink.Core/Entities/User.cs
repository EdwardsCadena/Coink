using System;
using System.Collections.Generic;

namespace Coink.Core.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? CountryId { get; set; }

    public int? DepartmentId { get; set; }

    public int? MunicipalityId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Municipality? Municipality { get; set; }
}
