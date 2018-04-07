using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Magicdoes.Districts.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Magicdoes.Districts.Amap
{
    /// <summary>
    /// 高德地图API获取区域信息
    /// </summary>
    public class AmapDistrictsProvider : IDistrictsProvider
    {
        /// <summary>
        /// 用户在高德地图官网申请Web服务API类型KEY
        /// </summary>
        private string ApiKey { get; set; }

        /// <summary>
        /// 初始化高德地图提供程序
        /// </summary>
        /// <param name="apiKey">用户在高德地图官网申请Web服务API类型KEY</param>
        /// <param name="subdistrict">规则：设置显示下级行政区级数（行政区级别包括：国家、省/直辖市、市、区/县、乡镇/街道多级数据）可选值：0、1、2、3等数字</param>
        public AmapDistrictsProvider(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("请设置API KEY！", nameof(apiKey));
            }

            ApiKey = apiKey;
        }

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
        public async Task<List<DistrictInfo>> GetDistricts(int subdistrict = 4)
        {
            if (subdistrict < 0)
            {
                throw new ArgumentException("下级行政区级数必须为正数！", nameof(subdistrict));
            }

            using (var client = new HttpClient())
            {
                //使用高德地图 行政区域查询 API
                //http://lbs.amap.com/api/webservice/guide/api/district
                var content = await client.GetStringAsync($"http://restapi.amap.com/v3/config/district?key={ApiKey}&subdistrict={subdistrict}");
                var obj = JsonConvert.DeserializeObject<JObject>(content);
                if (obj["info"]?.ToString() == "OK")
                {
                    var districts = obj["districts"];
                    var list = new List<DistrictInfo>();
                    AddChildrenDistricts(districts, list, null);
                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 添加子集
        /// </summary>
        /// <param name="districts"></param>
        /// <param name="parent"></param>
        private void AddChildrenDistricts(JToken districts, List<DistrictInfo> list, DistrictInfo parent)
        {
            foreach (var item in districts)
            {
                //城市编码
                var citycode = item["citycode"]?.ToString();
                //区域编码
                var adcode = item["adcode"]?.ToString();
                //行政区边界坐标点
                var polyline = item["polyline"]?.ToString();
                var name = item["name"]?.ToString();
                //城市中心点
                var center = item["center"]?.ToString();
                var level = item["level"]?.ToString();

                var districtInfo = new DistrictInfo()
                {
                    AreaCode = adcode,
                    CityCode = citycode,
                    Name = name,
                    Level = (DistrictLevels)Enum.Parse(typeof(DistrictLevels), level),
                    Center = center,
                    Polyline = polyline
                };
                if (parent == null)
                {
                    list.Add(districtInfo);
                }
                else
                    parent.Children.Add(districtInfo);

                if (item["districts"].HasValues)
                {
                    AddChildrenDistricts(item["districts"], list, districtInfo);
                }
            }
        }

        public async Task<List<DistrictInfo>> GetDistricts(string keywords, int subdistrict = 3)
        {
            if (string.IsNullOrWhiteSpace(keywords))
            {
                throw new ArgumentException("关键字不能为空！", nameof(keywords));
            }

            if (subdistrict < 0)
            {
                throw new ArgumentException("下级行政区级数必须为正数！", nameof(subdistrict));
            }

            using (var client = new HttpClient())
            {
                //使用高德地图 行政区域查询 API
                //http://lbs.amap.com/api/webservice/guide/api/district

                var content = await client.GetStringAsync($"http://restapi.amap.com/v3/config/district?key={ApiKey}&subdistrict={subdistrict}&keywords={keywords}");
                var obj = JsonConvert.DeserializeObject<JObject>(content);
                if (obj["info"]?.ToString() == "OK")
                {
                    var districts = obj["districts"];
                    var list = new List<DistrictInfo>();
                    AddChildrenDistricts(districts, list, null);
                    return list;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
