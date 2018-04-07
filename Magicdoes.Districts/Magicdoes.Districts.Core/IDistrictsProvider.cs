using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Magicdoes.Districts.Core
{
    /// <summary>
    /// 区域提供程序
    /// </summary>
    public interface IDistrictsProvider
    {
        /// <summary>
        /// 获取行政区域信息
        /// </summary>
        /// <param name="subdistrict">规则：设置显示下级行政区级数（行政区级别包括：国家、省/直辖市、市、区/县、乡镇/街道多级数据）
        /// 可选值：0、1、2、3等数字，并以此类推
        ///0：不返回下级行政区；
        ///1：返回下一级行政区；
        ///2：返回下两级行政区；
        ///3：返回下三级行政区；</param>
        /// <returns></returns>
        Task<List<DistrictInfo>> GetDistricts(int subdistrict = 4);

        /// <summary>
        /// 根据关键字获取区域信息列表
        /// </summary>
        /// <param name="keywords">指定关键字</param>
        /// <param name="subdistrict">显示下级行政区级数（行政区级别包括：国家、省/直辖市、市、区/县4个级别） 可选值：0、1、2、3</param>
        /// <returns></returns>
        Task<List<DistrictInfo>> GetDistricts(string keywords, int subdistrict = 3);
    }
}
