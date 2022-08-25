namespace NetworkManagerApi.Models
{
    public class Device
    {
        private readonly int _Id;
        private readonly string _Name;

        public Device(int id, string name)
        {
            _Id = id;
            _Name = name;
        }

        public int Id => _Id;

        public string Name => _Name;
    }
}