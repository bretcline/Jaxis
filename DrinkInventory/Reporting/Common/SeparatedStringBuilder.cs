using System.Collections;
using System.Text;

namespace Jaxis.DrinkInventory.Reporting.Common
{
    public class SeparatedStringBuilder
    {
        private readonly StringBuilder m_builder;
        private readonly string m_separator;
        private readonly bool m_ignoreDuplicates;
        private readonly ArrayList m_alreadyAddedObjects;

        public SeparatedStringBuilder(string _separator) : this(_separator, false) { }

        public SeparatedStringBuilder(string _separator, bool _ignoreDuplicates)
        {
            m_builder = new StringBuilder();
            m_separator = _separator;
            m_ignoreDuplicates = _ignoreDuplicates;
            m_alreadyAddedObjects = new ArrayList();
        }

        public SeparatedStringBuilder Append(object _value)
        {
            if (IsNullOrEmpty(_value))
                return this;
            if (m_ignoreDuplicates && m_alreadyAddedObjects.Contains(_value))
                return this;

            m_builder.Append(_value);
            m_builder.Append(m_separator);

            if (m_ignoreDuplicates)
            {
                m_alreadyAddedObjects.Add(_value);
            }
            return this;
        }

        public static bool IsNullOrEmpty(object _value)
        {
            if (_value == null)
                return true;
            if (_value.ToString() == string.Empty)
                return true;
            return false;
        }

        public SeparatedStringBuilder AppendFormat(string _format, params object[] _args)
        {
            m_builder.AppendFormat(_format, _args);
            m_builder.Append(m_separator);
            return this;
        }

        public SeparatedStringBuilder AppendRange(IEnumerable _values)
        {
            foreach (object value in _values)
                Append(value);
            return this;
        }

        public SeparatedStringBuilder AppendRangeFormatted(string _format, params object[] _values)
        {
            foreach (object value in _values)
                Append(string.Format(_format, value));
            return this;
        }

        public override string ToString()
        {
            string result = m_builder.ToString();
            if (result.EndsWith(m_separator))
            {
                result = result.Substring(0, result.Length - m_separator.Length);
            }
            return result;
        }
    }
}