namespace Application.UseCase
{
    public class IdGenerator
    {
        private int _currentId;

        public IdGenerator(int startingId = 1)
        {
            _currentId = startingId;
        }

        public int GenerateId()
        {
            return _currentId++;
        }
    }
}
