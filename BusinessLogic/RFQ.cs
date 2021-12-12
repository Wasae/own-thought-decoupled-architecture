using System;
using InterfaceBL.Repository;

namespace BusinessLogic
{
    public class RFQ : IRFQ
    {
        public string SupplierId {get;set;}
        public string buyername {get;set;}
        public string productname {get;set;}
        public string quantity {get;set;}
        public string price {get;set;}
        public string CompanyName {get;set;}
        public string productid {get;set;}
        string IRFQ.RFQ_Id {get;set;}
    }
}