using System;
using System.Collections.Generic;
using BevClasses;
using SubSonic.Repository;
using System.Linq;
using System.Threading;
using Jaxis.Util.Log4Net;

namespace BevWCFDB
{
    // NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in App.config.
    public class WCFDB : IWCFDB
    {
        private static Mutex m_DBMutex = new Mutex();
        private static string m_SQLDB = "BevData";
        private static SimpleRepositoryOptions m_RepoOptions = SimpleRepositoryOptions.RunMigrations;

        public WCFDB()
        {
#if DEBUG
            List<Bottle> Bottles = GetBottles();
            if (0 == Bottles.Count)
            {
                try
                {
                    m_DBMutex.WaitOne();
                    Beverage Bev = new Beverage { Label = "Label", Price = 100, Size = 250 };
                    AddUpdateBeverage(Bev);
                    Bottle Bot = new Bottle { Beverage = Bev, Cart = "Cart", QuantityLeft = 150, Tag = "Tag" };
                    AddUpdateBottle(Bot);
                }
                catch (Exception exp)
                {
                    Log.WriteException("WCFDB.WCFDB", exp);
                }
                finally
                {
                    m_DBMutex.ReleaseMutex();
                }
            }
#endif
        }


        public string AddUpdatePour(Pour _Pour)
        {
            string rc = null;

            try
            {
                m_DBMutex.WaitOne();
                if (_Pour.BottleID != _Pour.Bottle.ID)
                    _Pour.BottleID = _Pour.Bottle.ID;
                SimpleRepository Repo = new SimpleRepository(m_SQLDB, m_RepoOptions);
                if (Repo.Exists<Pour>(P => P.ID.Equals(_Pour.ID)))
                {
                    Repo.Update(_Pour);
                }
                else
                {
                    Repo.Add(_Pour);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("WCFDB.AddUpdatePour", exp);
            }
            finally
            {
                m_DBMutex.ReleaseMutex();
            }
            return rc;
        }

        public List<Pour> GetPours()
        {
            List<Pour> rc = new List<Pour>();

            try
            {
                m_DBMutex.WaitOne();
                SimpleRepository Repo = new SimpleRepository(m_SQLDB, m_RepoOptions);
                IQueryable<Pour> Ps = Repo.All<Pour>();
                if (0 < Ps.Count())
                {
                    rc = Ps.ToList();
                }
                foreach (Pour P in rc)
                {
                    P.Bottle = GetBottle(P.BottleID);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("WCFDB.GetPours", exp);
            }
            finally
            {
                m_DBMutex.ReleaseMutex();
            }

            return rc;
        }

        public Pour GetPour(Guid _ID)
        {
            Pour rc = null;
            try
            {
                m_DBMutex.WaitOne();
                SimpleRepository Repo = new SimpleRepository(m_SQLDB, m_RepoOptions);
                rc = Repo.All<Pour>().Where(P => P.ID.Equals(_ID)).FirstOrDefault();
                if (null != rc)
                {
                    rc.Bottle = GetBottle(rc.BottleID);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("WCFDB.GetPour", exp);
            }
            finally
            {
                m_DBMutex.ReleaseMutex();
            }
            return rc;
        }

        public string AddUpdateBottle(Bottle _Bottle)
        {
            string rc = null;

            try
            {
                m_DBMutex.WaitOne();
                if (_Bottle.BeverageID != _Bottle.Beverage.ID)
                    _Bottle.BeverageID = _Bottle.Beverage.ID;
                SimpleRepository Repo = new SimpleRepository(m_SQLDB, m_RepoOptions);
                if (Repo.Exists<Bottle>(B => B.ID.Equals(_Bottle.ID)))
                {
                    Repo.Update(_Bottle);
                }
                else
                {
                    Repo.Add(_Bottle);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("WCFDB.AddUpdateBottle", exp);
            }
            finally
            {
                m_DBMutex.ReleaseMutex();
            }

            return rc;
        }

        public List<Bottle> GetBottles()
        {
            List<Bottle> rc = new List<Bottle>();

            try
            {
                m_DBMutex.WaitOne();
                SimpleRepository Repo = new SimpleRepository(m_SQLDB, m_RepoOptions);
                IQueryable<Bottle> Bs = Repo.All<Bottle>();
                if (0 < Bs.Count())
                {
                    rc = Bs.ToList();
                }
                foreach (Bottle B in rc)
                {
                    B.Beverage = GetBeverage(B.BeverageID);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("WCFDB.GetBottles", exp);
            }
            finally
            {
                m_DBMutex.ReleaseMutex();
            }
            return rc;
        }

        public Bottle GetBottle(Guid _ID)
        {
            Bottle rc = null;
            try
            {
                m_DBMutex.WaitOne();
                SimpleRepository Repo = new SimpleRepository(m_SQLDB, m_RepoOptions);
                rc = Repo.All<Bottle>().Where(B => B.ID.Equals(_ID)).FirstOrDefault();
                if (null != rc)
                {
                    rc.Beverage = GetBeverage(rc.BeverageID);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("WCFDB.GetBottle", exp);
            }
            finally
            {
                m_DBMutex.ReleaseMutex();
            }
            return rc;
        }

        public Bottle GetBottleForTag(string _Tag)
        {
            Bottle rc = null;
            try
            {
                m_DBMutex.WaitOne();
                SimpleRepository Repo = new SimpleRepository(m_SQLDB, m_RepoOptions);
                rc = Repo.All<Bottle>().Where(B => B.Tag.Equals(_Tag)).FirstOrDefault();
                if (null != rc)
                {
                    rc.Beverage = GetBeverage(rc.BeverageID);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("WCFDB.GetBottleForTag", exp);
            }
            finally
            {
                m_DBMutex.ReleaseMutex();
            }
            return rc;
        }

        public string RemoveBottle(Bottle _Bottle)
        {
            string rc = null;
            try
            {
                m_DBMutex.WaitOne();
                SimpleRepository Repo = new SimpleRepository(m_SQLDB, m_RepoOptions);
                Repo.Delete<Bottle>(_Bottle.ID);
            }
            catch (Exception exp)
            {
                Log.WriteException("WCFDB.RemoveBottle", exp);
            }
            finally
            {
                m_DBMutex.ReleaseMutex();
            }
            return rc;
        }

        public string AddUpdateBeverage(Beverage _Beverage)
        {
            string rc = null;
            try
            {
                m_DBMutex.WaitOne();
                SimpleRepository Repo = new SimpleRepository(m_SQLDB, m_RepoOptions);
                if (Repo.Exists<Beverage>(B => B.ID.Equals(_Beverage.ID)))
                {
                    Repo.Update(_Beverage);
                }
                else
                {
                    Repo.Add(_Beverage);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("WCFDB.AddUpdateBeverage", exp);
            }
            finally
            {
                m_DBMutex.ReleaseMutex();
            }

            return rc;
        }

        public List<Beverage> GetBeverages()
        {
            List<Beverage> rc = new List<Beverage>();

            try
            {
                m_DBMutex.WaitOne();
                SimpleRepository Repo = new SimpleRepository(m_SQLDB, m_RepoOptions);
                IQueryable<Beverage> Bs = Repo.All<Beverage>();
                if (0 < Bs.Count())
                {
                    rc = Bs.ToList();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("WCFDB.GetBeverages", exp);
            }
            finally
            {
                m_DBMutex.ReleaseMutex();
            }

            return rc;
        }

        public Beverage GetBeverage(Guid _ID)
        {
            Beverage rc = null;
            try
            {
                m_DBMutex.WaitOne();
                SimpleRepository Repo = new SimpleRepository(m_SQLDB, m_RepoOptions);
                rc = Repo.All<Beverage>().Where(B => B.ID.Equals(_ID)).FirstOrDefault();
            }
            catch (Exception exp)
            {
                Log.WriteException("WCFDB.GetBeverage()", exp);
            }
            finally
            {
                m_DBMutex.ReleaseMutex();
            }
            return rc;
        }
    }
}
