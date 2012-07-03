
namespace WebApp.Classes.Prices
{
    public class MeteredPrice
    {
        public decimal StartQuantity { get; set; }
        public decimal? EndQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string UnitName { get; set; }
        public string CurrencyCode { get; set; }

        public override string ToString()
        {
            var price = this.UnitPrice == 0 ? "" : string.Format(", {0:0.##} {1}", this.UnitPrice, this.CurrencyCode);
            if (this.EndQuantity.HasValue)
            {
                return string.Format("{0}{1} - {2}{3}{4}", this.StartQuantity, this.UnitName, this.EndQuantity.Value, this.UnitName, price);
            }
            else
            {
                return string.Format("Unlimited{0}", price);
            }

        }
    }
}