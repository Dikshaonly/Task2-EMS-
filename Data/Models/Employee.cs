using System;
using System.Collections.Generic;

namespace Task2.Data.Models;

public partial class Employee
{
    public int Eid { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public int? Phone { get; set; }

    public int? DepId { get; set; }

    public int? Did { get; set; }

    public virtual Department? Dep { get; set; }

    public virtual Designation? DidNavigation { get; set; }
}
