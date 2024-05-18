using System;
using System.Collections.Generic;

namespace Eccomerce.Models;

public partial class TbRolePermission
{
    public long Id { get; set; }

    public int? RoleId { get; set; }

    public long? PermissionId { get; set; }

    public virtual TbPermission? Permission { get; set; }

    public virtual TbRole? Role { get; set; }
}
