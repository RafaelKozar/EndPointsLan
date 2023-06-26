using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPoints.Dto
{
    public class EndPointGyrDto
    {
        public string SerialNumber { get; set; }
        public EnumMeterModel MeterModelId { get; set; }
        public int MeterNumber { get; set; }
        public string MeterFirmwareVersion { get; set; }
        public EnumSwitchState SwitchState { get; set; }
    }
}
