﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class LeaveType : BaseEntity
    {
        //[StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public int DefaultDays { get; set; }
    }
}
