using System;
using System.Collections.Generic;

namespace Eccomerce.Models;

public partial class TbModule
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TbPermission> TbPermissions { get; set; } = new List<TbPermission>();
}
