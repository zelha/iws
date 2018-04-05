 
using System.Collections.Generic;
using System.ServiceModel;
using System;

namespace IWS_Velib
{
    public class IWSVelibImpl : IWSVelib
    { 
        JDCCaller jDC = JDCCaller.GetJDCCaller();

        static Action<string, string, VelibStation> m_Event1 = delegate { };

        static Action m_Event2 = delegate { };


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
            VelibStation result = jDC.getStation(city, stationName);

            m_Event1(city, stationName, result);
            m_Event2();
            return result;
        }


 

        public void SubscribeCalculatedEvent()
        {
            IWSVelibServiceEvents subscriber =
               OperationContext.Current.GetCallbackChannel<IWSVelibServiceEvents>();

            m_Event1 += subscriber.LastAvailableBikesCalculated;  
        }

        public void SubscribeCalculationFinishedEvent()
        {
            IWSVelibServiceEvents subscriber = 
               OperationContext.Current.GetCallbackChannel<IWSVelibServiceEvents>();
            m_Event2 += subscriber.LastAvailableBikesCalculationFinished;    

        }


    }
}


