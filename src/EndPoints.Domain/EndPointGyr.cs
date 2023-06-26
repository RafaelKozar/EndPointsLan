using System.ComponentModel.DataAnnotations;

namespace EndPoints.Domain
{
    public class EndPointGyr
    {
        [Key]
        public string SerialNumber { get; set; }    
        public int MeterModelId { get; set; }   
        public int MeterNumber { get; set; }
        public string MeterFirmwareVersion { get; set; }
        public int SwitchState { get; set; }    

            
    }
}