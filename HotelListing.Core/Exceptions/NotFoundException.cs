namespace HotelListing.Core.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name , object key) : base($"{name} With id ({key}) was not found")
        {

        }
    }
}
