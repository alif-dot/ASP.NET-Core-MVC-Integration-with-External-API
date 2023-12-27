namespace JwtToken.Models
{
    public class PosDataModel
    {
        public string customer_name { get; set; }
        public string customer_phone { get; set; }
        public int pay_value { get; set; }
        public int store_id { get; set; }
        public int invoice_id { get; set; }
        public int herlan_cash_discount { get; set; }
    }
}
