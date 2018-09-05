using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace hiktool
{
    class Program
    {
        static Int32 devId;
        static string devIp;
        static string devPort;
        static string userName;
        static string Password;
        static string conCom;
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 5)
                {
                    Console.WriteLine("-------------************-------------");
                    Console.WriteLine("本工具供微信PHP调用\n");
                    Console.WriteLine("共需调用五个参数使其正常运行");
                    Console.WriteLine("-------------************-------------\n");
                    Console.WriteLine("一、设备的IP ");
                    Console.WriteLine("二、端口");
                    Console.WriteLine("三、用户名");
                    Console.WriteLine("四、密码");
                    Console.WriteLine("五、控制指令\n");
                    Console.WriteLine("-------------************-------------");
                    Console.WriteLine("具体控制指令：\n");
                    Console.WriteLine("摄像头拉近： in    摄像头拉远： out");
                    Console.WriteLine("摄像头向上： up    摄像头向下： down");
                    Console.WriteLine("摄像头向左： left  摄像头向右： right");
                    Console.WriteLine("-------------************-------------\n");
                    Console.ReadLine();
                }
                else
                {
                    devIp = args[0];
                    devPort = args[1];
                    userName = args[2];
                    Password = args[3];
                    conCom = args[4];
                    switch (conCom)
                    {
                        case "up": conCom = "21"; break;
                        case "down": conCom = "22"; break;
                        case "left": conCom = "23"; break;
                        case "right": conCom = "24"; break;
                        case "in": conCom = "11"; break;
                        case "out": conCom = "12"; break;
                        default: break;
                    }
                    CHCNetSDK.NET_DVR_Init();
                    CHCNetSDK.NET_DVR_DEVICEINFO_V30 devInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();
                    devId = CHCNetSDK.NET_DVR_Login_V30(devIp, Convert.ToInt16(devPort), userName, Password, ref devInfo);
                    CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(devId, 1, Convert.ToUInt16(conCom), 0u, 4u);
                    Thread.Sleep(1000);
                    CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(devId, 1, Convert.ToUInt16(conCom), 1u, 4u);
                    CHCNetSDK.NET_DVR_Logout(devId);
                    CHCNetSDK.NET_DVR_Cleanup();
                }
            }
            catch
            {
                Console.WriteLine("参数错误，请检查！！");
                Console.ReadLine();
            }
        }
    }
}
