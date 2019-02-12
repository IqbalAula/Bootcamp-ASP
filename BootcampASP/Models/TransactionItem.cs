using BootcampASP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootcampASP.Models
{
    public class TransactionItem : BaseModel
    {
        public int? Quantity { get; set; }
        public virtual Item Items { get; set; }
        public virtual Transaction Transacations { get; set; }
    }
}