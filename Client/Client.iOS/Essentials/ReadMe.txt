经测试 iOS 项目的 DependencyService 无法在 Util.XamariN.iOs 中实现, 程序运行时

Util.XamariN.Essentials.IDisplayInfoUtils match = Xamarin.Forms.DependencyService.Get<Util.XamariN.Essentials.IDisplayInfoUtils>();
if (match == null)
{
	throw new Exception("未能找到实现 Util.XamariN.Essentials.IDisplayInfoUtils 的依赖");
}
else
{
	return match.GetDisplayInfo();
}

match 为 null, 但将相关代码移动到 Client.iOS 后, 则能成功找到