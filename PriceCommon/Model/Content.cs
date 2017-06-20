using System;


namespace PriceCommon.Model
{
    /// <summary>
    /// ��������� ������
    /// </summary>
    public class Content
    {
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
        public string Uri { get; set; }

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
    }
}