namespace GameZone.Models
{
    public class GameDevice
    {
        public Game game { get; set; } = default!;
        public int gameID { get; set; }

        public Device device { get; set; } = default!;
        public int deviceID { get; set; }

    }
}
