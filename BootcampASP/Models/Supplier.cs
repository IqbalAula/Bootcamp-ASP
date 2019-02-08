using BootcampASP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootcampASP.Models
{
    public class Supplier : BaseModel
    {
        public string Name { get; set; }
        public DateTimeOffset JoinDate { get; set; }
    }
}