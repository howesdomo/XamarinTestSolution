namespace Client.Common
{
    public interface IWechatOpenSDK
    {
        bool OpenWechat();

        bool SendMsg(string msg, string description = "", WXScene scene = 0);

        bool SendImage(string imagePath, WXScene scene = 0);
    }
}
