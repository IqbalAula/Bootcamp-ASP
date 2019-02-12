using BootcampASP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootcampASP.Models
{
    public class Transaction : BaseModel
    {
        public DateTimeOffset TransactionDate { get; set; }        
    }
}