 
using System.Collections.Generic;
 

namespace IWS_Velib
{
    public class IWSVelibImpl : IWSVelib
    { 
    JDCCaller jDC = JDCCaller.GetJDCCaller();

        List<string> IWSVelib.GetListStations(string city)
        {
            List<string> stationNames =new List<string>(); 
            List<VelibStation> stations = jDC.getCity(city);

            foreach(VelibStation v in stations)
            {
                stationNames.Add(v.Name);
            }
            return stationNames;
        }

        VelibStation IWSVelib.getStation(string city,string stationName)
        {
            return jDC.getStation(city, stationName);
        }
        
    }
}


