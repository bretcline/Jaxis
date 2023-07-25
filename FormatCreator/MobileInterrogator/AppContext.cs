using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Jaxis.RFID.Readers;
using LFI.RFID.Format;
using LFI.RFID.Editor;

namespace MobileInterrogator
{
    // TODO: Break out some of the functions into separate classes
    public class AppContext
    {
        public AppContext()
        {
            // Get the name of the folder we are running out of
            string fullExeNameWith = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            string installPath = System.IO.Path.GetDirectoryName(fullExeNameWith);

            // Initialize the format manager
            string formatPath = System.IO.Path.Combine(installPath, "Formats");
            formatManager = new FormatManager(formatPath); 

            // TOOD: Initialize RFID Reader
            IRFIDConfig Config = new MockConfig( );
            Config.FormatDefinitionPath = formatPath;

            reader = new MockReader( );
            reader.ConfigureDevice( Config );
        }
        
        public void ClearState()
        {
            currentTagData = null;
        }

        public bool InitiateTagRead()
        {
            // TODO: Trigger the RFID Reader to fetch the data
            // TODO: Load the data into the TagData cache
            // TODO: Return T/F to indicate success
            currentTagData = reader.ReadTag( );

            return true;
        }

        public bool InitiateTagWrite( )
        {
            reader.WriteTag( CurrentTagData );
            return true;
        }

        public bool CreateNewTagData(Guid formatID)
        {
            if (!formatManager.IsKnownFormat(formatID))
                return false;

            currentTagData = new TagData();
            currentTagData.FormatID = formatID;
            return true;
        }

        public TagData CurrentTagData
        {
            get { return currentTagData; }
        }

        public FormatDef CurrentTagFormat
        {
            get 
            {
                if (currentTagData == null)
                    return null;
                else
                    return GetFormatDef(currentTagData.FormatID);
            }
        }

        public FormatDef GetFormatDef(Guid formatID)
        {
            return formatManager.GetFormatByID(formatID);
        }

        public IList<FormatDef> AvailableFormats
        {
            get { return new List<FormatDef>(formatManager.GetAvailableFormats()); }
        }

        public ValueEditState ValueEditState { get { return valueEditState; } }

        #region Member Variables

        private FormatManager formatManager = null;
        private IRFIDReader reader = null;
        private TagData currentTagData = null;
        private ValueEditState valueEditState = new ValueEditState();

        #endregion
    }
}