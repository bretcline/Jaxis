using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Readers.Identec
{
    public enum FormulaTypes
    {
        FreePour,
        NozzlePour,
    }

    public interface IEngineSettings
    {
        // Device Setup
        string IPAddress { get; }

        Int16 Port { get; }

        Int16 SleepTime { get; }

        // Formula Setup
        string Formula { get; }

        Dictionary<string, string> FormulaValues { get; }
    }

    public class EngineSettings : IEngineSettings
    {
        // Device Setup
        public string IPAddress { get; set; }

        public Int16 Port { get; set; }

        public Int16 SleepTime { get; set; }

        // Formula Setup
        public string Formula { get; set; }

        public Dictionary<FormulaTypes, string> Formulas { get; set; }

        public Dictionary<string, string> FormulaValues { get; set; }

        public EngineSettings( )
        {
            FormulaValues = new Dictionary<string, string>( );
            Formulas = new Dictionary<FormulaTypes, string>( );
        }
    }
}