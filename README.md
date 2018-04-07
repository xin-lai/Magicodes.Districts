# Magicodes.Districts
      行政区域信息核心库。以便于获取最新的行政区域数据用于自动更新业务数据库等。
      官方网址：http://xin-lai.com
      开源库地址:https://github.com/xin-lai
      博客地址：http://www.cnblogs.com/codelove/
      交流QQ群：85318032
      小店地址：https://shop113059108.taobao.com/
      最新框架（完全支持.NET Core）：https://gitee.com/xl_wenqiang/Magicodes.Admin.Core
      已更新为.NET标准库，支持.NET Core
	  已编写单元测试，可以自行配置。

	  目前只支持通过高德API获取。后续再对接其他服务商。

## 高德接口配置
		请先申请高德的开发者Key。
        var districtsProvider = new AmapDistrictsProvider(settings.ApiKey);

## 获取中国省市区街道信息

        var result = await DistrictsProvider.GetDistricts();

		具体见单元测试：

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