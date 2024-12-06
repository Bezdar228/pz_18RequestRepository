﻿using System;
using System.Collections.Generic;

namespace pz_18Request.Models;

public partial class Technician
{
    public int TechnicianId { get; set; }

    public string FullName { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
