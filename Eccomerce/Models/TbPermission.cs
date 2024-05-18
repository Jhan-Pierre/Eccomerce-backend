using System;
using System.Collections.Generic;

namespace Eccomerce.Models;

public partial class TbPermission
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long? ModuleId { get; set; }

    public virtual TbModule? Module { get; set; }

    public virtual ICollection<TbRolePermission> TbRolePermissions { get; set; } = new List<TbRolePermission>();
}
