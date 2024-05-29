using System;
using System.Collections.Generic;

namespace Seniors.Models;

public partial class UserOrder
{
    public int Id { get; set; }

    public int EmloyeeId { get; set; }

    public DateTime Date { get; set; }

    public double Price { get; set; }

    public virtual EmployeeRest Emloyee { get; set; } = null!;

    public virtual ICollection<Dish> Dishes { get; } = new List<Dish>();
}
