using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;

namespace BeverageManagement.Forms.Activity.Widgets
{
    public abstract class QueueingWidget<T> : DevExpress.XtraEditors.XtraUserControl, IActivityReceiver
    {
        public QueueingWidget( )
        {
            this.VisibleChanged += QueueWidget_VisibleChanged;
        }

        protected abstract void ProcessItem( T _item );

        #region Implementation of IActivityReceiver

        public List<Type> MessageType { get; protected set; }
        private BlockingCollection<T> m_Items = new BlockingCollection<T>();
        private Task m_Processor = null;
        protected bool m_Running;


        public bool AddActivityItem(object _item)
        {
            bool rc = false;

            if (_item is T && this.Visible )
            {
                m_Items.Add((T)_item);
            }

            return rc;
        }

        #endregion



        protected void ProcessItems()
        {
            while (true == m_Running)
            {
                Application.DoEvents();
                T item;
                if (true == m_Items.TryTake(out item, 500))
                {
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker)(() => ProcessItem(item)));
                    }
                    else
                    {
                        ProcessItem(item);
                    }
                    Application.DoEvents();
                }

                // hokey way to reduce the number of items we're trying to process
                if (m_Items.Count > 25)
                {
                    m_Items.TryTake(out item);
                    m_Items.TryTake(out item);

                    if (m_Items.Count > 50)
                    {
                        m_Items.TryTake(out item);
                        m_Items.TryTake(out item);
                        m_Items.TryTake(out item);
                    }
                }
            }
        }


        private void QueueWidget_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                m_Running = false;
                if (null != m_Processor && m_Processor.Status == TaskStatus.Running)
                {
                    m_Processor.Wait(250);
                }
            }
            else
            {
                m_Running = true;
                if (null == m_Processor || m_Processor.Status != TaskStatus.Running)
                {
                    m_Processor = Task.Factory.StartNew(ProcessItems);
                }
            }
        }


        protected string ConvertedVolume( double _volume )
        {
            var volume = BLManagerFactory.Get().ConvertPourToUnits(_volume);
            var units = BLManagerFactory.Get().GetDefaultSizeType().Abbreviation;
            return string.Format("{0:0.00} {1}", volume, units);
        }
    }
}
