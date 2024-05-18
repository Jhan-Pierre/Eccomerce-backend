using System;
using System.Collections.Generic;

namespace Eccomerce.Models;

public partial class TbUser
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int RoleId { get; set; }

    public int StateId { get; set; }

    public int? ShiftId { get; set; }

    public virtual TbRole? Role { get; set; }

    public virtual TbShift? Shift { get; set; }

    public virtual TbState? State { get; set; }

}
