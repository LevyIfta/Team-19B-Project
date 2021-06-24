using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ClientWeb.Connection
{
    class Alarmanager
    {
        private static Queue<DecodedMessge> nonAlarmMsg { get; set; } = new Queue<DecodedMessge>();
        private static AutoResetEvent waitlock { get; set; } = new AutoResetEvent(false);

        private static void handleAlarm(DecodedMessge alarm)
        {
            MessageBox.Show( alarm.param_list[0],  alarm.name);

        }
        private static void brah(System.Windows.Controls.Page p)
        {
            
        }

        public static void start()
        {
            try
            {
                while (true)
                {
                    byte[] ans_e = Connection.ConnectionManager.readMessageCon();
                    DecodedMessge ans = Connection.Decoder.decode(ans_e);
                    if (ans.type == msgType.ALARM)
                        handleAlarm(ans);
                    else
                    {
                        lock (nonAlarmMsg)
                        {

                            //  MessageBox.Show("not an alaram: return value " + ans.name);


                            nonAlarmMsg.Enqueue(ans);
                            waitlock.Set();
                        }
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("and Error has occured", "oops! seems something went wrong. please try again later");
                
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
