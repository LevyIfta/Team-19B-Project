using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ClientProject.Connection
{
    class Alarmanager
    {
        private static Queue<DecodedMessge> nonAlarmMsg { get; set; } = new Queue<DecodedMessge>();
        private static AutoResetEvent waitlock { get; set; } = new AutoResetEvent(false);

        private static void handleAlarm(DecodedMessge alarm)
        {
            MessageBox.Show(alarm.param_list[0], alarm.name);
        }

        public static void start()
        {
            while(true)
            {
                byte[] ans_e = Connection.ConnectionManager.readMessageCon();
                DecodedMessge ans = Connection.Decoder.decode(ans_e);
                if (ans.type == msgType.ALARM)
                    handleAlarm(ans);
                else
                {
                    lock (nonAlarmMsg)
                    {
                        nonAlarmMsg.Enqueue(ans);
                        waitlock.Set();
                    }
                }
            }
        }
        public static DecodedMessge getMsg()
        {
            while (nonAlarmMsg.Count == 0)
                waitlock.WaitOne();
            lock(nonAlarmMsg)
            {
                return nonAlarmMsg.Dequeue();
            }
        }
    }
}
