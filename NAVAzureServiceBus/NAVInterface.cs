using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAVAzureServiceBus.Classes;

namespace NAVAzureServiceBus
{
    class NAVInterface
    {
        //public object GetNAVOrder()
        public NAVOrder GetNAVOrder()
        {
            NAVOrder order = new NAVOrder();
            order.OrderNo = "OV1";
            order.OrderDate = DateTime.Now;
            order.CustomerNo = "C0001";
            List<NAVOrderLine> lines = new List<NAVOrderLine>();
            NAVOrderLine line = new NAVOrderLine();
            line.RowNo = 1;
            line.ItemNo = "ITEM1";
            line.Quantity = 5;
            lines.Add(line);
            line = new NAVOrderLine();
            line.RowNo = 2;
            line.ItemNo = "ITEM2";
            line.Quantity = 13;
            lines.Add(line);

            order.Lines = lines;

            return order;
        }
    }
}
