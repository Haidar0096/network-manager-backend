namespace NetworkManagerApi.Models
{
    public class Port
    {
        private readonly int _Id;
        private readonly int _Number;
        private readonly int _DeviceId;

        public Port(int id, int number, int deviceId)
        {
            _Id = id;
            _Number = number;
            _DeviceId = deviceId;
        }

        public int Id => _Id;

        public int Number => _Number;

        public int DeviceId => _DeviceId;
    }
}