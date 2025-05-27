using System;
using System.Collections.Generic;

namespace Russian_cosmetic.Model;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrderNumber { get; set; } = null!;

    public DateOnly? CreationDate { get; set; }

    public string ClientCode { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? ClosingDate { get; set; }

    public int EmployeeId { get; set; }

    public string ExecutionTime { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    public virtual ICollection<Orderservice> Orderservices { get; set; } = new List<Orderservice>();
}
