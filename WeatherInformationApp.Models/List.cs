using System.Collections.Generic;

namespace WeatherInformationApp.Models
{
    public class List
    {
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public Wind Wind { get; set; }
        public string Dt_Txt { get; set; }
    }
}
