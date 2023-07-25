using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EngineConfigVersionData;
using Jaxis.Util.Log4Net;

namespace EngineConfigVersions
{

    public static class EngineVersions
    {

        private static List<EngineVersionData> m_List = new List<EngineVersionData>();
        private static Mutex m_Mutex = new Mutex();

        public static void AddVersion(EngineVersionData _Version)
        {
            try
            {
                m_Mutex.WaitOne();
                m_List.Add(_Version);
            }
            catch (Exception exp)
            {
                Log.WriteException("EngineVersions::AddVersion", exp);
            }
            finally
            {
                m_Mutex.ReleaseMutex();
            }
        }

        public static int Count()
        {
            int rc = 0;
            try
            {
                m_Mutex.WaitOne();
                rc = m_List.Count();
            }
            catch (Exception exp)
            {
                Log.WriteException("EngineVersions::Count", exp);
            }
            finally
            {
                m_Mutex.ReleaseMutex();
            }
            return rc;
        }

        public static EngineVersionData GetNext()
        {
            EngineVersionData rc = null;
            try
            {
                m_Mutex.WaitOne();
                if (0 != m_List.Count)
                {
                    rc = m_List[0];
                    m_List.Remove(rc);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("EngineVersions::GetNext", exp);
            }
            finally
            {
                m_Mutex.ReleaseMutex();
            }
            return rc;
        }
    }
}
