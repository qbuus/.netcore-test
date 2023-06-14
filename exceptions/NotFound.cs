using Microsoft.Identity.Client;

namespace API.exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
