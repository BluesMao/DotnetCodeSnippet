using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sov.IronPay.Core
{
    public static class GenericObjectExtensions
    {
        public static T Execute<T>(this Func<T> Function, int Attempts = 3, int RetryDelay = 0, int TimeOut = int.MaxValue)
        {
            if (Function == null)
                throw new ArgumentNullException("Function");
            Exception Holder = null;
            long Start = System.Environment.TickCount;
            while (Attempts > 0)
            {
                try
                {
                    return Function();
                }
                catch (Exception e) { Holder = e; }
                if (System.Environment.TickCount - Start > TimeOut)
                    break;
                Thread.Sleep(RetryDelay);
                --Attempts;
            }
            throw Holder;
        }

        public static void Execute(this Action Action, int Attempts = 3, int RetryDelay = 0, int TimeOut = int.MaxValue)
        {
            if (Action == null)
                throw new ArgumentNullException("Function");
            Exception Holder = null;
            long Start = System.Environment.TickCount;
            while (Attempts > 0)
            {
                try
                {
                    Action();
                }
                catch (Exception e) { Holder = e; }
                if (System.Environment.TickCount - Start > TimeOut)
                    break;
                Thread.Sleep(RetryDelay);
                --Attempts;
            }
            if (Holder != null)
                throw Holder;
        }
    }
}
