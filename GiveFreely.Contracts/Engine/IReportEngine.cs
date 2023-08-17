using GiveFreely.Models.Report;

namespace GiveFreely.Contracts.Engine
{
    public interface IReportEngine
    {
        Task<Report> Generate(int idAffiliate);
    }
}
