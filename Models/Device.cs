namespace NetworkManagerApi.Models
{
    public class Device
    {
        private readonly int id;
        private readonly string name;

        public Device(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id => id;

        public string Name => name;
    }
}