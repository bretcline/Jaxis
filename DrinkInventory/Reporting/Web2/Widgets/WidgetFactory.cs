using System;
using System.Collections.Generic;

namespace Jaxis.DrinkInventory.Reporting.Web2.Widgets
{
    public class WidgetFactory
    {
        private static readonly IDictionary<Guid,IWidget> m_widgetMap;

        static WidgetFactory()
        {
            m_widgetMap = new Dictionary<Guid,IWidget>();
            
            var testWidgetIds = new[]
            {
                new Guid("1779B290-BE7D-442C-80B6-46478AFF94C5"), 
                new Guid("947A9771-74C6-4190-948F-087F5B293E8B"), 
                new Guid("7EE214C3-C111-4B39-B299-F992C0ACF7F1"), 
                new Guid("2418181C-067B-47F9-BDE7-8322E93AFB81"), 
                new Guid("968D553C-E1B1-4BCA-ABBD-5BC092FDA831"), 
                new Guid("525894D8-04FC-440E-9151-049BEF4AD001"), 
                new Guid("215760B7-0F85-4F41-867C-6A634328B02A"), 
                new Guid("E21E1B0D-C7F3-43D2-8143-C4F03ECB9E07"), 
                new Guid("0EACFD42-9852-4920-8585-84AA52F82E64")
            };

            for (var i = 0; i < testWidgetIds.Length; i++)
            {
                m_widgetMap.Add(testWidgetIds[i], new TestWidget { Id = testWidgetIds[i], Name = "Test Widget " + i });
            }

            IWidget widget = new RecentPoursWidget();
            m_widgetMap.Add(widget.Id, widget);
            widget = new TopFiveVolumes();
            m_widgetMap.Add(widget.Id, widget);
        }

        public static IWidget GetWidget(Guid _widgetId)
        {
            return m_widgetMap[_widgetId];
        }


        public static IEnumerable<IWidget> All
        {
            get { return m_widgetMap.Values; }
        }
    }
}
