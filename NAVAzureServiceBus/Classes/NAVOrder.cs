﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NAVAzureServiceBus.Classes
{
    [DataContract]
    class NAVOrder
    {
        [DataMember]
        public string OrderNo { get; set; }

        [DataMember]
        public string CustomerNo { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public List<NAVOrderLine> Lines { get; set; }
    }
}
