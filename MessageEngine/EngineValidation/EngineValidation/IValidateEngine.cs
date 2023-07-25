using System.Runtime.Serialization;
using System.ServiceModel;

namespace Jaxis.Engine.Validation
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IValidateEngine" in both code and config file together.
    [ServiceContract]
    public interface IValidateEngine
    {

        [OperationContract]
        bool Validate(ValidationData _configData);
    }

}
