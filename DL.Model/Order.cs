using System;
using System.Collections.Generic;

namespace DL.Model
{
    public class Order
    {

        public string OrderNo { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? EndTime { get; set; }

        public decimal PlanQty { get; set; }

        public decimal? ScanQty { get; set; }

        public decimal DiffQty
        {
            get
            {
                decimal r = this.PlanQty;
                if (this.ScanQty.HasValue)
                {
                    r = this.ScanQty.Value;
                }
                return r;
            }
        }

        public List<Carton> CartonList { get; set; }
    }

}
