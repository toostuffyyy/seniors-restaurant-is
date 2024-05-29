using System;
using System.Collections.Generic;

namespace Seniors.Models;

public partial class TypeDish
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Dish> Dishes { get; } = new List<Dish>();
}
