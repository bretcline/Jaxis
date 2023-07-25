using System;
using System.ServiceModel;
using System.ServiceModel.Security;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;
using Jaxis.Util.Log4Net;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public class ServiceWrapper<TConcrete, TInterface> : IServiceWrapper<TInterface> where TConcrete : class, TInterface, IDisposable, new()
    {
        private TConcrete m_service;

        public ServiceResult WithService(Func<TInterface,ServiceResult> _action)
        {
            for (var i = 0; i < 2; i++)
            {
                if (m_service == null)
                    m_service = new TConcrete();

                try
                {
                    var result = _action(m_service);

                    if (result is ExceptionResult)
                    {
                        Log.Error(string.Format("Error in ServiceWrapper.WithService: Message = {0}", 
                            ((ExceptionResult)result).Message));
                        throw new ServiceException(((ExceptionResult) result).Message);
                    }

                    return result;
                }
                catch (Exception exception)
                {
                    if (exception is MessageSecurityException || 
                        exception is CommunicationObjectFaultedException ||
                        exception is EndpointNotFoundException)
                    {
                        m_service = null;
                        Log.Exception(exception);
                        if (i == 1)
                            throw new Exception("An error occurred communicating with the Reporting Service");
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return null;
        }

        public void Dispose()
        {
            m_service.Dispose();
        }
    }
}