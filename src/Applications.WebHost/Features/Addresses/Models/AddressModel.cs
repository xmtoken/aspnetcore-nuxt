namespace AspNetCoreNuxt.Applications.WebHost.Features.Addresses.Models
{
    /// <summary>
    /// 住所の情報を表します。
    /// </summary>
    public class AddressModel
    {
        /// <summary>
        /// 7 桁の郵便番号を取得または設定します。
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 都道府県名、市区町村名、町域名を含む住所を取得または設定します。
        /// </summary>
        public string Address { get; set; }
    }
}
