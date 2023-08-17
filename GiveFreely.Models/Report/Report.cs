namespace GiveFreely.Models.Report
{
    public class Report
    { 
        public DateTime ReportDate { get; set; }

        public int Number { get; set; }
        public string Name { get; set; }

        public int CountCustomers { get; set; }

        public List<CustomerReport> Customers { get; set; }

        public decimal TotalCommision { get; set;}
    }
}
