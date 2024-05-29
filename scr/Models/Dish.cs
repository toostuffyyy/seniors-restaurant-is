using System;
using System.Collections.Generic;

namespace Seniors.Models;

public partial class Dish
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Price { get; set; } = null!;

    public string? Notes { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<UserOrder> Orders { get; } = new List<UserOrder>();

    public virtual ICollection<TypeDish> TypeDishes { get; } = new List<TypeDish>();
}
