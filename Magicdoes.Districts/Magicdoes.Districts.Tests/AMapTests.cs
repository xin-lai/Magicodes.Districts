using System;
using System.Threading.Tasks;
using Magicdoes.Districts.Amap;
using Xunit;
using Shouldly;
using System.Linq;
using Magicdoes.Districts.Tests.Helper;

namespace Magicdoes.Districts.Tests
{
    public class AMapTests : TestBase
    {
        public AMapTests()
        {
            var settings = TestConfigHelper.LoadConfig("amap");
            DistrictsProvider = new AmapDistrictsProvider(settings.ApiKey);
        }
        [Fact]
        public async Task GetDistricts_TestsAsync()
        {
            var result = await DistrictsProvider.GetDistricts();
            result.ShouldNotBeNull();

            //一级行政区（省级行政区）：34个（23个省、5个自治区、4个直辖市、2个特别行政区） ...台湾
            result[0].Children.Count.ShouldBeGreaterThanOrEqualTo(34);

            //二级行政区（地级行政区）：334个（294个地级市、7个地区、30个自治州、3个盟）
            result[0].Children.Sum(p => p.Children.Count).ShouldBeGreaterThanOrEqualTo(334);

        }

        [Fact]
        public async Task GetDistrictsByKeywords_TestsAsync()
        {
            var result = await DistrictsProvider.GetDistricts("湖南");
            result.ShouldNotBeNull();

            //截止2017年9月12日，湖南省共计划分为14个地区（13地级市和1自治州），122个县级行政区包括35个市辖区、17个县级市、63个县和7个自治县
            result[0].Children.Count.ShouldBeGreaterThanOrEqualTo(14);
            result[0].Children.Sum(p => p.Children.Count).ShouldBeGreaterThanOrEqualTo(122);

        }
    }
}
