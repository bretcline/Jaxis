using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace POSDescriptions
{
    class PullDescriptions
    {
        protected Dictionary<string, int> m_KnownAlias = new Dictionary<string, int>();
        protected List<string> m_POSAlias = new List<string>();
        static void Main(string[] args)
        {
            new PullDescriptions("POSReader.txt", "KnownAliases.txt");
        }

        private void AddKnownItem( string item )
        {
            item = item.ToUpper();
            if (!m_KnownAlias.ContainsKey(item))
            {
                m_KnownAlias[item] = 0;
            }
            m_KnownAlias[item]++;
        }


        public PullDescriptions( string _file, string _knownAliases )
        {
            using (var reader = new StreamReader(_knownAliases))
            {
                var line = reader.ReadLine();
                while (null != line)
                {
                    if (line.Contains(","))
                    {
                        var items = line.Split(',');
                        foreach (var item in items)
                        {
                            AddKnownItem(item);
                        }
                    }
                    else
                    {
                        AddKnownItem(line);
                    }
                    line = reader.ReadLine();
                }
            }



            using (var reader = new StreamReader(_file))
            {
                var line = reader.ReadLine();
                while (null != line)
                {
                    if (line.Contains("Description:"))
                    {
                        m_POSAlias.Add(line.Replace("Description:", "").Replace("*", "").Replace("#", "").Trim());
                    }
                    line = reader.ReadLine();
                }
            }

            var list = m_POSAlias.Distinct().ToList();
            list.Sort();
            using (var writer = new StreamWriter("POS Aliases.txt"))
            {
                foreach (var label in list)
                {
                    if(!m_KnownAlias.ContainsKey(label.ToUpper()))
                    {
                        writer.WriteLine( label );
                    }
                }
            }
        }
    }
}
