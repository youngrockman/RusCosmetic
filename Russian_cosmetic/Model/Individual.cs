using System;
using System.Collections.Generic;

namespace Russian_cosmetic.Model;

public partial class Individual
{
    public string ClientCode { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string PassportDetails { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
