using System;
using System.Net;
using System.Security.Permissions;
using System.Threading;

namespace Antize_TFT.Class
{
    class MyData
    {
        private const string AntizeVer = "1.56";
        private Thread NewThread;

        public string AddStats()
        {
            this.NewThread = new Thread(this.MyNewThread);
            this.NewThread.Start();

            return AntizeVer;
        }

        private void MyNewThread()
        {
        
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        private void KillTheThread()
        {
            this.NewThread.Abort();
        }
    }
}