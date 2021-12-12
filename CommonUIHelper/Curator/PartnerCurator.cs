using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CommonUIHelper.Repository;
using InterfaceBL.Repository;
using Microsoft.Extensions.Configuration;
using ObjectFactory;

namespace CommonUIHelper.Curator
{
    public class PartnerCurator
    {
        private IConfiguration _configuration;
        private PartnerRepository _repository;

        public PartnerCurator(IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = new PartnerRepository(_configuration);
        }

        public async Task<List<IProduct>> GetUerProducts(int userid)
        {
            var t = await _repository.GetUerProducts(userid);
            List<IProduct> products = new List<IProduct>();
            if (t != null && t.Rows.Count != 0)
            {
                foreach (DataRow dr in t.Rows)
                {
                    var product = (IProduct)Factory.Create("Products", _configuration);
                    product.ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]);
                    product.ProductName = dr["ProductName"] == DBNull.Value ? "" : Convert.ToString(dr["ProductName"]);
                    product.ProductSKU = dr["ProductSKU"] == DBNull.Value ? "" : Convert.ToString(dr["ProductSKU"]);
                    product.Quantity = dr["Quantity"] == DBNull.Value ? "0" : Convert.ToString(dr["Quantity"]);
                    product.Price = dr["Price"] == DBNull.Value ? "0" : Convert.ToString(dr["Price"]);
                    products.Add(product);
                }
            }
            return products;
        }

        public async Task<List<IUser>> GetUserCustomers(int userid)
        {
            var t = await _repository.GetUserCustomers(userid);
            List<IUser> customers = new List<IUser>();
            if (t != null && t.Rows.Count != 0)
            {
                foreach (DataRow dr in t.Rows)
                {
                    var customer = (IUser)Factory.Create("Users", _configuration);
                    customer.ID = dr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id"]);
                    customer.UserName = dr["userName"] == DBNull.Value ? "" : Convert.ToString(dr["userName"]);
                    customers.Add(customer);
                }
            }
            return customers;
        }

        public async Task<List<IRFQ>> GetUserRFQ(int userid)
        {
            var t = await _repository.GetUserRFQ(userid);
            List<IRFQ> rfqs = new List<IRFQ>();
            if (t != null && t.Rows.Count != 0)
            {
                foreach (DataRow dr in t.Rows)
                {
                    var rfq = (IRFQ)Factory.Create("RFQ", _configuration);
                    rfq.RFQ_Id = dr["RFQ_Id"] == DBNull.Value ? "" : Convert.ToString(dr["RFQ_Id"]);
                    rfq.buyername = dr["CompanyName"] == DBNull.Value ? "" : Convert.ToString(dr["CompanyName"]);
                    rfqs.Add(rfq);
                }
            }
            return rfqs;
        }

        public async Task<List<IRFQ>> GetUserRFQDetails(string rfqid,int userid)
        {
            var t = await _repository.GetUserRFQDetails(rfqid,userid);
            List<IRFQ> rfqs = new List<IRFQ>();
            if (t != null && t.Rows.Count != 0)
            {
                foreach (DataRow dr in t.Rows)
                {
                    var rfq = (IRFQ)Factory.Create("RFQ", _configuration);
                    rfq.RFQ_Id = dr["RFQ_Id"] == DBNull.Value ? "" : Convert.ToString(dr["RFQ_Id"]);
                    rfq.SupplierId = dr["SupplierId"] == DBNull.Value ? "" : Convert.ToString(dr["SupplierId"]);
                    rfq.buyername = dr["buyername"] == DBNull.Value ? "" : Convert.ToString(dr["buyername"]);
                    rfq.productname = dr["productname"] == DBNull.Value ? "" : Convert.ToString(dr["productname"]);
                    rfq.quantity = dr["quantity"] == DBNull.Value ? "" : Convert.ToString(dr["quantity"]);
                    rfq.price = dr["price"] == DBNull.Value ? "" : Convert.ToString(dr["price"]);
                    rfqs.Add(rfq);
                }
            }
            return rfqs;
        }
    }
}