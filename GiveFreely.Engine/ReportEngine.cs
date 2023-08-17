using GiveFreely.Contracts.Engine;
using GiveFreely.DataAccess.DTOAdapter;
using GiveFreely.DataAccess.Interfaces;
using GiveFreely.Models.Report;
using Microsoft.Extensions.Logging;

namespace GiveFreely.Engine
{
    public class ReportEngine : IReportEngine
    {
        private readonly IAffiliateRepository _repositoryAffiliate;
        private readonly ICommisionRepository _repositoryCommision;
        private readonly ILogger<ReportEngine> _logger;

        public ReportEngine(IAffiliateRepository repositoryAffiliate,
            ICommisionRepository repositoryCommision,
           ILogger<ReportEngine> logger)
        {
            _repositoryAffiliate = repositoryAffiliate;
            _repositoryCommision = repositoryCommision; 
            _logger = logger;
        }

        public async Task<Report> Generate(int idAffiliate)
        {
            try
            {
                _logger.LogInformation($"Affiliate Id: {idAffiliate} to create report");
                Report report = new Report();
                var commisions = await _repositoryCommision.GetAsync();

                var affiliate = await _repositoryAffiliate.GetByIdAsync(idAffiliate);

                if (affiliate != null)
                {
                    report.ReportDate = DateTime.Now;
                    report.Number = affiliate.IdAffiliate;
                    report.CountCustomers = affiliate.Customers.Count;
                    report.Name = affiliate.Name;
                    var customers = affiliate.Customers.ToReportModel();

                    int cont = 1;
                    decimal total = 0;
                    foreach (var customer in customers)
                    {
                        var money = commisions.Where(p => cont >= p.FromCount && cont < (p.ToCount.HasValue ? p.ToCount.Value : int.MaxValue)).FirstOrDefault();
                        customer.Commision = money != null ? money.Money : 0;
                        total = total + customer.Commision;
                        cont++;
                    }
                    report.Customers = customers;
                    report.TotalCommision = total;

                }

                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Affiliate Id: {idAffiliate} to search error: {ex.Message}");
                return null;
            }


        }
    }
}
