namespace GameZone.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Icon { get; set; } = string.Empty;
    }
}