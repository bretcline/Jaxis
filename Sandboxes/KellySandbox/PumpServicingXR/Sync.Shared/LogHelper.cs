using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFI.Sync.Shared
{
	public static class LogHelper
	{
		//----------------------------------------------------------------------
		/// <summary>
		/// Forces the log to display a message no matter what log4net logging level is set
		/// </summary>
		public static void ForceLog(string toLog)
		{
			log4net.ILog log = log4net.LogManager.GetLogger("ApplicationState");
			log4net.Repository.Hierarchy.Hierarchy hierachy = ((log4net.Repository.Hierarchy.Hierarchy)log.Logger.Repository);
			log4net.Core.Level currentLevel = hierachy.Root.Level;
			hierachy.Root.Level = log4net.Core.Level.Info;

			log.Info(toLog);
			hierachy.Root.Level = currentLevel;
		}
	}
}
