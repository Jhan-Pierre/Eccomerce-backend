using System;
using System.Collections.Generic;

namespace Eccomerce.Models;

public partial class TbShift
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TbUser>? TbUsers { get; set; }
}
