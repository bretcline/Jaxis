using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace JaxisPubSubManager
{
    public abstract class PublishService<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_args"></param>
        protected static void FireEvent(params object[] _args)
        {
            StackFrame stackFrame = new StackFrame(1);
            string methodName = stackFrame.GetMethod().Name;

            //Parse out explicit interface implementation
            if (methodName.Contains("."))
            {
                string[] parts = methodName.Split('.');
                methodName = parts[parts.Length - 1];
            }

            PublishTransient( methodName, _args );
            PublishPersistent( methodName, _args );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="_args"></param>
        static void PublishTransient(string methodName, params object[] _args)
        {
            T[] subscribers = SubscriptionManager<T>.GetSubscriberList(methodName);
            Publish(subscribers, false, methodName, _args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="_args"></param>
        static void PublishPersistent( string methodName, params object[] _args )
        {
            T[] subscribers = SubscriptionManager<T>.GetPersistentList( methodName );
            Publish( subscribers, true, methodName, _args );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_subscribers"></param>
        /// <param name="_closeSubscribers"></param>
        /// <param name="_methodName"></param>
        /// <param name="_args"></param>
        static void Publish(T[] _subscribers, bool _closeSubscribers, string _methodName, params object[] _args)
        {
            WaitCallback fire = delegate(object subscriber)
            {
                Invoke(subscriber as T, _methodName, _args);
                if( _closeSubscribers )
                {
                    using( subscriber as IDisposable )
                    { }
                }
            };

            Action<T> queueUp = delegate(T subscriber)
            {
                ThreadPool.QueueUserWorkItem(fire, subscriber);
            };

            Array.ForEach(_subscribers, queueUp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_subscriber"></param>
        /// <param name="_methodName"></param>
        /// <param name="_args"></param>
        static void Invoke(T _subscriber, string _methodName, object[] _args)
        {
            Type type = typeof(T);
            MethodInfo methodInfo = type.GetMethod(_methodName);
            try
            {
                methodInfo.Invoke(_subscriber, _args);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
    }
}
