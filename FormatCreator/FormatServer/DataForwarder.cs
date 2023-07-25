using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LFI.RFID.Format;
using System.IO;
using System.Reflection;

namespace LFI.RFID.FormatServer
{
    public class DataForwarder
    {
        public DataForwarder()
        {
            forwarderFolder = "Forwarders";

            LoadAvailableForwarders();
        }

        public void Forward(TagData data)
        {
            foreach (IDataForwarder forwarder in forwarders)
            {
                try { forwarder.Forward(data); }
                catch { }
            }
        }

        private void LoadAvailableForwarders()
        {
            forwarders = new List<IDataForwarder>();
            
            if (!Directory.Exists(forwarderFolder)) return;

            // Look in the given directory for all DLLS
            List<string> dllFiles = new List<string>(System.IO.Directory.GetFiles(forwarderFolder, "*.dll"));

            // Check each DLL to see if it has any classes that implement IDataForwarder
            foreach (string fileName in dllFiles)
            {
                try // If an exception is thrown, skip the file and move on
                {
                    Assembly assembly = Assembly.LoadFrom(fileName);

                    // Get a list of types in the assembly
                    Type[] types = assembly.GetTypes();

                    // Check each type to see if it implements IDataForwarder
                    foreach (Type type in types)
                    {
                        try
                        {
                            // Skip abstract classes and interfaces
                            if (type.IsAbstract || type.IsInterface)
                            {
                                continue;
                            }

                            if (type.GetInterface(typeof(IDataForwarder).FullName) != null)
                            {
                                IDataForwarder forwarder = (IDataForwarder)Activator.CreateInstance(type);
                                forwarders.Add(forwarder);
                            }
                        }
                        catch
                        {
                            // TODO: Add a error log entry here
                            // Ignore the exception and process the remaining types
                        }
                    }
                }
                catch
                {
                    // Ingore the exception and process the remaining files
                }
            }            
        }

        private string forwarderFolder;
        private List<IDataForwarder> forwarders = null;
    }    
}
