using System;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "pricing_plan_payment_schedule")]
    public class ProductPricingPlanPaymentScheduleBase
    {
        #region Data Members

        /// <summary>
        /// Unique identifier for a Payment Schedule. Is generated and managed by database
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }

        /// <summary>
        /// Pricing plan payment schedule name
        /// </summary>
        [DataMember(Name = "name", IsRequired = false)]
        public string Name { get; set; }

        /// <summary>
        /// How frequently a payment is done for this product pricing plan.
        /// 1 = One time only
        /// 4 = Daily
        /// 8 = Weekly
        /// 16 = Monthly
        /// 32 = Monthly, relative to FrequencyRelativeInterval
        /// 64 = Yearly
        /// </summary>
        [DataMember(Name = "frequency_type", IsRequired = true)]
        public RecurringCyclePeriod FrequencyType { get; set; }

        /// <summary>
        /// Days that the payment is executed. Depends on the value of FrequencyType. 
        /// The default value is 0, which indicates that FrequencyInterval is unused.
        /// 
        /// Value of FrequencyType  Effect on FrequencyInterval	
        /// 1 (once)	            FrequencyInterval is unused (0)	
        /// 4 (daily)	            FrequencyInterval is unused (0)	
        /// 8 (weekly)	            FrequencyInterval is one or more of the following:
        ///                         1 = Sunday, 2 = Monday, 4 = Tuesday, 8 = Wednesday, 16 = Thursday,
        ///                         32 = Friday, 64 = Saturday,	
        ///                         For details see the enum WeekDays.Monday, WeekDays...
        ///16 (monthly)	            On the FrequencyInterval day of the month	
        ///32 (monthly, relative)   FrequencyInterval is one of the following: 
        ///                         1 = Sunday, 2 = Monday, 4 = Tuesday, 8 = Wednesday, 16 = Thursday,
        ///                         32 = Friday, 64 = Saturday
        ///64 (yearly)              the day of the year (from 1 to 366)
        /// </summary>
        [DataMember(Name = "frequency_interval", IsRequired = true)]
        public int FrequencyInterval { get; set; }

        /// <summary>
        /// When FrequencyInterval occurs in each month, 
        /// if FrequencyInterval is 32 (monthly relative). 
        /// Can be one of the following values:
        /// 0 = FrequencyRelativeInterval is unused
        /// 1 = First
        /// 2 = Second
        /// 4 = Third
        /// 8 = Fourth
        /// 16 = Last
        /// </summary>
        [DataMember(Name = "frequency_relative_interval", IsRequired = true)]
        public FrequencyOccurrence FrequencyRelativeInterval { get; set; }

        /// <summary>
        /// The pause between the payment intervals. Depends on the value of FrequencyType. 
        /// The default value is 0, which indicates that FrequencyRecurrenceFactor is unused.
        /// 
        /// Value of FrequencyType  Effect on FrequencyRecurrenceFactor
        /// 1 (once)	            FrequencyRecurrenceFactor is unused (0)
        /// 4 (daily)	            Every FrequencyRecurrenceFactor days
        /// 8 (weekly)	            Every FrequencyRecurrenceFactor weeks
        ///16 (monthly)	            Every FrequencyRecurrenceFactor months
        ///32 (monthly, relative)   Every FrequencyRecurrenceFactor months
        ///64 (yearly)              Every FrequencyRecurrenceFactor years
        /// </summary>
        [DataMember(Name = "frequency_recurrence_factor", IsRequired = true)]
        public int FrequencyRecurrenceFactor { get; set; }

        /// <summary>
        /// Subscription charge for one period
        /// </summary>
        [DataMember(Name = "subscription_period_change", IsRequired = false)]
        public decimal SubscriptionPeriodCharge { get; set; }

        /// <summary>
        /// The time/date when this payment will be executed first
        /// </summary>        
        internal Int64 StartDate { get; set; }

        /// <summary>
        /// Gets or sets the start date (offset from the subscription signup date).
        /// </summary>
        [DataMember(Name = "start_date_offset_days", IsRequired = false)]
        public int StartDateOffsetDays
        {
            get
            {
                if (this.StartDate == 0)
                    return 0;
                return TimeSpan.FromTicks((Int64)this.StartDate).Days;
            }
            set
            {
                this.StartDate = TimeSpan.FromDays(value).Ticks;
            }
        }

        /// <summary>
        /// The time/date after which the recurring payments will not be executed anymore
        /// </summary>
        internal Int64 EndDate { get; set; }

        /// <summary>
        /// Gets or sets the end date (offset from the subscription signup date).
        /// </summary>
        [DataMember(Name = "end_date_offset_days", IsRequired = false)]
        public int EndDateOffsetDays
        {
            get
            {
                if (this.EndDate == 0)
                    return 0;
                return TimeSpan.FromTicks((Int64)this.EndDate).Days;
            }
            set
            {
                this.EndDate = TimeSpan.FromDays(value).Ticks;
                //if (value != null && value.HasValue)
                //    this.EndDate = TimeSpan.FromDays(value.Value).Ticks;
                //else
                //    this.EndDate = null;
            }
        }

        #endregion
    }
}
