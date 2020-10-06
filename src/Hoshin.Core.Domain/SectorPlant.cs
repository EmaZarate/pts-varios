namespace Hoshin.Core.Domain
{
    public class SectorPlant
    {
        public int PlantID { get; set; }
        public int SectorID { get; set; }
        public int ReferringJob { get; set; }
        public int ReferringJob2 { get; set; }
        public Plant.Plant Plant { get; set; }
        public Sector.Sector Sector { get; set; }
    }
}
