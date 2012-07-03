using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Shows the evolution of signups for a product. Represented as a collection of ProductsSignups objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "report_products_signups_evolution_response")]
    public class ProductsSignupsEvolutionResponse : Collection<ProductsSignups>
    {
    }

    /// <summary>
    /// Represents number of signups for a Product 
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "products_signups_item")]
    public class ProductsSignups
    {
        /// <summary>
        /// Id of the Product for which signups are evaluated
        /// </summary>
        [DataMember(Name = "product_id", IsRequired = false)]
        public int ProductId { get; set; }

        /// <summary>
        /// Name of the Product for which signups are evaluated
        /// </summary>
        [DataMember(Name = "product", IsRequired = false)]
        public string Product { get; set; }

        /// <summary>
        /// Date for when signups are evaluated
        /// </summary>
        [DataMember(Name = "date", IsRequired = false)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Number of signups for the Product
        /// </summary>
        [DataMember(Name = "signups", IsRequired = false)]
        public decimal? Signups { get; set; }

        public ProductsSignups()
        {
            this.Date = DateTime.MinValue.ToUniversalTime();
        }
    }

    /// <summary>
    /// Shows the evolution of revenues for a product. Represented as a collection of ProductsRevenues objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "report_products_revenues_evolution")]
    public class ProductsRevenuesEvolutionResponse : Collection<ProductsRevenues>
    {
    }

    /// <summary>
    /// Represents total revenues for a Product  
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "products_revenues_item")]
    public class ProductsRevenues
    {
        /// <summary>
        /// Id of the Product for which revenues are evaluated
        /// </summary>
        [DataMember(Name = "product_id", IsRequired = false)]
        public int ProductId { get; set; }

        /// <summary>
        /// Name of the Product for which revenues are evaluated
        /// </summary>
        [DataMember(Name = "product", IsRequired = false)]
        public string Product { get; set; }

        /// <summary>
        /// Date for when revenues are evaluated
        /// </summary>
        [DataMember(Name = "date", IsRequired = false)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Total amount of revenues
        /// </summary>
        [DataMember(Name = "revenues", IsRequired = false)]
        public decimal? Revenues { get; set; }

        /// <summary>
        /// Code of the Currency used to evaluate the revenues
        /// </summary>
        [DataMember(Name = "currency_code", IsRequired = false)]
        public string CurrencyCode { get; set; }

        public ProductsRevenues()
        {
            this.Date = DateTime.MinValue.ToUniversalTime();
        }
    }

    /// <summary>
    /// Represents total amount of revenues report data
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "report_total_revenue_amount", ItemName = "total_revenue_amount_item")]
    public class TotalRevenueAmountResponse : Collection<RevenueAmount>
    {
    }

    /// <summary>
    /// Represents revenue's amount report data
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "item")]
    public class RevenueAmount
    {
        /// <summary>
        /// Amount of the revenue
        /// </summary>
        [DataMember(Name = "amount", IsRequired = false)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Code of the Currency used to evaluate the revenue
        /// </summary>
        [DataMember(Name = "currency_code", IsRequired = false)]
        public string CurrencyCode { get; set; }
    }

    /// <summary>
    /// Represents report data about count of active Subscriptions
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "report_active_subscriptions_count")]
    public class ActiveSubscriptionsCountResponse
    {
        /// <summary>
        /// Count of active Subscriptions
        /// </summary>
        [DataMember(Name = "count", IsRequired = false)]
        public int Count { get; set; }
    }

    /// <summary>
    /// Represents report data about count of total amount of subscribers
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "report_total_subscribers_count")]
    public class TotalSubscribersCountResponse
    {
        /// <summary>
        /// Count of total subscribers
        /// </summary>
        [DataMember(Name = "count", IsRequired = false)]
        public int Count { get; set; }
    }

    /// <summary>
    /// Represents report data about total revenues amount
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "report_today_revenue_amount", ItemName = "today_revenue_amount_item")]
    public class TodayRevenueAmountResponse : Collection<RevenueAmount>
    {
    }

    /// <summary>
    /// Represents report data about count of total new subscribers
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "report_today_new_subscribers_count")]
    public class TodayNewSubscribersCountResponse
    {
        [DataMember(Name = "count", IsRequired = false)]
        public int Count { get; set; }
    }

    /// <summary>
    /// Represents evolution of signups for a specific date. Implemented as collection of ReportIntValueItem objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "report_signups_evolution", ItemName = "signups_item")]
    public class SignupsEvolutionResponse : Collection<ReportIntValueItem>
    {
    }

    /// <summary>
    /// Represents evolution of revenues for a specific date. Implemented as collection of ReportIntValueItem objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "report_revenues_evolution", ItemName = "revenues_item")]
    public class RevenuesEvolutionResponse : Collection<ReportDecimalValueItem>
    {
    }

    /// <summary>
    /// Represents evolution of customers for a specific date. Implemented as collection of ReportIntValueItem objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "report_customers_evolution", ItemName = "customers_item")]
    public class CustomersEvolutionResponse : Collection<ReportIntValueItem>
    {
    }

    /// <summary>
    /// Represents evolution of subscriptions for a specific date. Implemented as collection of ReportIntValueItem objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "report_subscriptions_evolution", ItemName = "subscriptions_item")]
    public class SubscriptionsEvolutionResponse : Collection<ReportIntValueItem>
    {
    }

    /// <summary>
    /// Represents report data for specific date
    /// </summary>
    [DataContract(Namespace = "v1.0")]
    public class ReportIntValueItem
    {
        /// <summary>
        /// Date for which report data is eveluated
        /// </summary>
        [DataMember(Name = "date", IsRequired = false)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Value of the report data
        /// </summary>
        [DataMember(Name = "value", IsRequired = false)]
        public int Value { get; set; }

        public ReportIntValueItem()
        {
            this.Date = DateTime.MinValue.ToUniversalTime();
        }
    }

    /// <summary>
    /// Represents report data for specific date
    /// </summary>
    [DataContract(Namespace = "v1.0")]
    public class ReportDecimalValueItem
    {
        /// <summary>
        /// Date for which report data is eveluated
        /// </summary>
        [DataMember(Name = "date", IsRequired = false)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Value of the report data
        /// </summary>
        [DataMember(Name = "value", IsRequired = false)]
        public decimal Value { get; set; }

        /// <summary>
        /// Code of the Currency used to evaluate the report data
        /// </summary>
        [DataMember(Name = "currency_code", IsRequired = false)]
        public string CurrencyCode { get; set; }

        public ReportDecimalValueItem()
        {
            this.Date = DateTime.MinValue.ToUniversalTime();
        }
    }

    /// <summary>
    /// Represents a evloution of a Site's activity. Implemented as a collection of SiteSummary objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "report_sites_summary")]
    public class SitesSummaryResponse : Collection<SiteSummary>
    {
    }

    /// <summary>
    /// Repesents summary of a Site's activity
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "summary")]
    public class SiteSummary
    {
        public SiteSummary()
        {
            this.TodayRevenues = new List<RevenueAmount>();
            this.TotalRevenues = new List<RevenueAmount>();
        }

        /// <summary>
        /// Id of the Site for which activity report is evaluated
        /// </summary>
        [DataMember(Name = "site_id", IsRequired = false)]
        public int SiteId { get; set; }

        /// <summary>
        /// Name of the Site for which activity report is evaluated
        /// </summary>
        [DataMember(Name = "site_name", IsRequired = false)]
        public string SiteName { get; set; }

        /// <summary>
        /// Number of today's new Subscriptions
        /// </summary>
        [DataMember(Name = "today_new_subscriptions", IsRequired = false)]
        public int TodayNewSubsctiptions { get; set; }

        /// <summary>
        /// Amount of today's total revenues
        /// </summary>
        [DataMember(Name = "today_revenues", IsRequired = false)]
        public List<RevenueAmount> TodayRevenues { get; set; }

        /// <summary>
        /// Amount of total revenues
        /// </summary>
        [DataMember(Name = "total_revenues", IsRequired = false)]
        public List<RevenueAmount> TotalRevenues { get; set; }

        /// <summary>
        /// Number of total subscribers
        /// </summary>
        [DataMember(Name = "total_subscribers", IsRequired = false)]
        public int TotalSubscribers { get; set; }
    }
}
