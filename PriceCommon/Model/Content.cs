using System;


namespace PriceCommon.Model
{
    /// <summary>
    /// ��������� ������
    /// </summary>
    public class Content
    {
        private string _uri;
        public bool Selected { get; set; }

        /// <summary>
        /// ������������ �������
        /// </summary>
        public string Name { get; set; }

        //[JsonIgnore]
        /// <summary>
        /// ���� �������
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// ���� �������
        /// </summary>
        public double Nprice => Convert.ToDouble(Price);

        /// <summary>
        /// ������ �� ��������
        /// </summary>
        public string Uri
        {
            get { return _uri; }
            set
            {
                _uri = value;
                if (!_uri.StartsWith("http")) Uri = $"http://zakupki.gov.ru/epz/contract/contractCard/payment-info-and-target-of-order.html?reestrNumber={_uri}";
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string Seller { get; set; }

        /// <summary>
        /// ���� � ����� ����� ���� (UTC)
        /// </summary>
        public long? CollectedAt { get; set; }

        /// <summary>
        /// ���� � ����� ����� ����
        /// </summary>
        public virtual DateTime Collected => new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToDouble(CollectedAt))/*.Date*/;

        /// <summary>
        /// ������������� �������
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// �������������
        /// </summary>
        public string Producer { get; set; }

        /// <summary>
        ///     ��������
        /// </summary>
        public string Phones { get; set; }

        /// <summary>
        ///     ����2
        /// </summary>
        public string Okpd2 { get; set; }

        /// <summary>
        ///     ������� ���������
        /// </summary>
        public string Okei { get; set; }

        /// <summary>
        ///     ������
        /// </summary>
        public string Currency { get; set; }

        public string prodStatus { get; set; }

    }
}