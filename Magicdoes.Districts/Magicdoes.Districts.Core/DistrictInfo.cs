using System;
using System.Collections.Generic;

namespace Magicdoes.Districts.Core
{
    /// <summary>
    /// 区域信息
    /// </summary>
    public class DistrictInfo
    {
        public DistrictInfo() => Children = new List<DistrictInfo>();

        /// <summary>
        /// 城市编码
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 行政区划级别
        /// </summary>
        public DistrictLevels Level { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 行政区边界坐标点
        /// 当一个行政区范围，由完全分隔两块或者多块的地块组成，每块地的 polyline 坐标串以 | 分隔 。如北京 的 朝阳区
        /// </summary>
        public string Polyline { get; set; }

        /// <summary>
        /// 城市中心点（部分街道不能获取）
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<DistrictInfo> Children { get; set; }
    }
}
