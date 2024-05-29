using System;
using System.Collections.Generic;

namespace Seniors.Models;

public partial class EmployeeRest
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public string LastName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? Image { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual ICollection<UserOrder> UserOrders { get; } = new List<UserOrder>();
}
