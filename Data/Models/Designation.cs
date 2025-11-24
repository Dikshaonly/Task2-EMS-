using System;
using System.Collections.Generic;

namespace Task2.Data.Models;

public partial class Designation
{
    public int Did { get; set; }

    public string? Dname { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
