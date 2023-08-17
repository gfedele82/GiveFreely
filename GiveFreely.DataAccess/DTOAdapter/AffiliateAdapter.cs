using GiveFreely.Models;

namespace GiveFreely.DataAccess.DTOAdapter
{
    public static class AffiliateAdapter
    {
        public static Schema.Affiliate ToDBModel(this Affiliate affiliate)
        {
            if (affiliate == null)
                return null;

            return new Schema.Affiliate()
            {
                IdAffiliate = affiliate.IdAffiliate,
                Name = affiliate.Name
            };

        }

        public static Affiliates ToModel(this Schema.Affiliate dbAffiliate)
        {
            if (dbAffiliate == null)
                return null;


            return new Affiliates()
            {
                IdAffiliate = dbAffiliate.IdAffiliate,
                Name = dbAffiliate.Name,
                Customer = dbAffiliate.Customers.ToModel()
            };
        }
    }
}
