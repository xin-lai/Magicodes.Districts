namespace Magicdoes.Districts.Core
{
    /// <summary>
    /// 行政区划级别
    /// </summary>
    public enum DistrictLevels
    {
        /// <summary>
        /// 国家
        /// </summary>
        country,
        /// <summary>
        /// 省份（直辖市会在province和city显示）
        /// </summary>
        province,
        /// <summary>
        /// 市（直辖市会在province和city显示）
        /// </summary>
        city,
        /// <summary>
        /// 区县
        /// </summary>
        district,
        /// <summary>
        /// 街道
        /// </summary>
        street
    }
}