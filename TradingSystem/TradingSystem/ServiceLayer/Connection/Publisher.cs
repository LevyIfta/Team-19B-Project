using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.ServiceLayer.Connection
{
    public static class Publisher
    {

        private static Dictionary<string, List<DecodedMessge>> alarmList { get; set; } = new Dictionary<string, List<DecodedMessge>>();
        private static List<Tuple<Func<string>, Action<DecodedMessge>, int>> subscribers { get; set; } = new List<Tuple<Func<string>, Action<DecodedMessge>, int>>();
        private static int _counter { get; set; } = 0;
        private static int counter { get { _counter++; return _counter; } }
        private static object SubscriberLock = new object();

        public static void sendAlarm(string userName, string title, string content)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.ALARM;
            msg.name = title;
            msg.param_list = new string[] { content };

            lock (SubscriberLock)
            {
                foreach (var item in subscribers)
                {
                    if (item.Item1.Invoke().Equals(userName))
                    {
                        item.Item2.Invoke(msg);
                        return;
                    }
                }

                if (!alarmList.ContainsKey(userName))
                    alarmList.Add(userName, new List<DecodedMessge>());
                alarmList[userName].Add(msg);
            }
            

        }

        public static int subscribe(Func<string> nameGetter, Action<DecodedMessge> sendFunc)
        {
            int id;
            Tuple<Func<string>, Action<DecodedMessge>, int> subscriber;
            lock (SubscriberLock)
            {
                id = counter;
                subscriber = new Tuple<Func<string>, Action<DecodedMessge>, int>(nameGetter, sendFunc, id);
                subscribers.Add(subscriber);
            }
            awaken(subscriber);     
            return id;

        }


        public static void awaken(int serialNum)
        {
            Tuple<Func<string>, Action<DecodedMessge>, int> subscriber = null;
            lock (SubscriberLock)
            {
                foreach (var item in subscribers)
                {
                    if(item.Item3 == serialNum)
                    {
                        subscriber = item;
                        break;
                    }
                }
            }
            if (subscriber != null)
                awaken(subscriber);
        }

        private static void awaken(Tuple<Func<string>, Action<DecodedMessge>, int> subscriber)
        {
            List<DecodedMessge> msgs;
            lock(SubscriberLock)
            {
                string username = subscriber.Item1.Invoke();
                if (!alarmList.ContainsKey(username))
                    return;
                msgs = alarmList[username];
                alarmList[username] = new List<DecodedMessge>();
            }
            foreach (DecodedMessge msg in msgs)
            {
                subscriber.Item2.Invoke(msg);
            }
        }


    }

}
