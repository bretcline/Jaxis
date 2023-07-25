using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;

namespace JaxisExtensions
{
    public static class Extensions
    {
        static Dictionary<int, BaseView> m_Views = new Dictionary<int, BaseView>();

        public static void CustomizeView( this BaseView view, bool _readonly = true )
        {
            if (!m_Views.ContainsKey( view.GetHashCode() ) && view.RowCount > 0 )
            {
                m_Views[view.GetHashCode()] = view;

                ColumnView cView = view as ColumnView;
                foreach (GridColumn c in cView.Columns)
                {
                    if (c.FieldName.EndsWith("ID"))
                    {
                        c.VisibleIndex = -1;
                    }
                    else
                    {
                        c.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        c.BestFit();
                        c.OptionsColumn.ReadOnly = _readonly;
                    }
                }
            }
        }
    }
}
