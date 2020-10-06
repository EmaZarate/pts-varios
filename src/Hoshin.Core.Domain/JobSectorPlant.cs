namespace Hoshin.Core.Domain
{
    public class JobSectorPlant
    {
        public int PlantID { get; set; }
        public int SectorID { get; set; }
        public int JobID { get; set; }
        public int JobPlantSupID { get; set; }
        public int JobSupID { get; set; }
        public int JobSectorSupID { get; set; }
        public Domain.Job.Job Job { get; set; }
        public SectorPlant SectorPlant { get; set; }
    }
}