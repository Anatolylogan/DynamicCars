namespace WebDynamicCars.Session
{
    public class ClientSessionService
    {
        private int? _currentClientId;

        public void Login(int clientId)
        {
            _currentClientId = clientId;
        }

        public void Logout()
        {
            _currentClientId = null;
        }

        public int? GetCurrentClientId()
        {
            return _currentClientId;
        }

        public bool IsLoggedIn()
        {
            return _currentClientId.HasValue;
        }
    }
}
