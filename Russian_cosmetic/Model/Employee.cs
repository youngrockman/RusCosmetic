using System;
using System.Collections.Generic;

namespace Russian_cosmetic.Model;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int PositionId { get; set; }

    public string FullName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime LastLogin { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Position Position { get; set; } = null!;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
