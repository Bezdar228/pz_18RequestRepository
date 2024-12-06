﻿using System;
using System.Collections.Generic;

namespace pz_18Request.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
