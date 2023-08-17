using GiveFreely.Models;
using GiveFreely.Models.Report;

namespace GiveFreely.DataAccess.DTOAdapter
{
    public static class UserAdapter
    {
        public static Schema.Customer ToDBModel(this Customer customer)
        {
            if (customer == null)
                return null;

            var loco =  new Schema.Customer()
            {
                IdCustomer = customer.IdCustomer,
                Name = customer.Name,
                IdAffiliate = customer.IdAffiliate
            };
            return loco;
        }

        public static Customers ToModel(this Schema.Customer dbCustomer)
        {
            if (dbCustomer == null)
                return null;


            return new Customers()
            {
                IdCustomer = dbCustomer.IdCustomer,
                Name = dbCustomer.Name,
                IdAffiliate = dbCustomer.IdAffiliate,
                Affiliate = dbCustomer.Affiliate != null ? new Affiliate()
                {
                    IdAffiliate = dbCustomer.IdAffiliate,
                    Name = dbCustomer.Affiliate.Name
                } : null
            };

        }

        public static List<Customer> ToModel(this List<Schema.Customer> dbCustomer)
        {
            if (dbCustomer == null)
                return null;


            List<Customer> customers = new List<Customer>();
            foreach (Schema.Customer customer in dbCustomer)
            {
                customers.Add(customer.ToModel());
            }
            return customers;

        }

        public static List<CustomerReport> ToReportModel(this List<Schema.Customer> dbCustomer)
        {
            if (dbCustomer == null)
                return null;


            List<CustomerReport> customers = new List<CustomerReport>();
            foreach (Schema.Customer customer in dbCustomer)
            {
                customers.Add(new CustomerReport()
                {
                     Commision = 0,
                     Id = customer.IdCustomer,
                     Name= customer.Name
                });
            }
            return customers;

        }
    }
}
