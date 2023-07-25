using System;

namespace Jaxis.Engine.Validation
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ValidateEngine" in code, svc and config file together.
    public class ValidateEngine : IValidateEngine
    {
        public bool Validate( ValidationData _data)
        {
            bool rc = false;
            try
            {
                rc = true;
            }
            catch (Exception)
            {
                
            }
            return rc;
        }
    }
}
