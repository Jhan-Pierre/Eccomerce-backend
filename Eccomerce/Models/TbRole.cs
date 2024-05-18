using System;
using System.Collections.Generic;

namespace Eccomerce.Models;

public partial class TbRole
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TbRolePermission> TbRolePermissions { get; set; } = new List<TbRolePermission>();

    public virtual ICollection<TbUser> TbUsers { get; set; } = new List<TbUser>();
}
