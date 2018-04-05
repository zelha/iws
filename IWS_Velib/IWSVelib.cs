using System.Collections.Generic; 
using System.Runtime.Serialization;
using System.ServiceModel;
 

namespace IWS_Velib
{
   
    [ServiceContract(CallbackContract = typeof(IWSVelibServiceEvents))]
    public interface IWSVelib
    {

        [OperationContract]
        List<string> GetListStations(string city);

        [OperationContract]
        VelibStation getStation(string city, string stationName);

 

        [OperationContract]
        void SubscribeCalculatedEvent();

        [OperationContract]
        void SubscribeCalculationFinishedEvent();
    }

    [DataContract]
    public class VelibStation
    {
   
        public string _name;
        public int _available_bikes;
        public int _available_bike_stands;
        public int _bike_stands;
        public bool _banking;

        public VelibStation(string name, int available_bikes, int available_bike_stands, int bike_stands, bool banking)
        {
            _name = name;
            _available_bikes = available_bikes;
            _available_bike_stands = available_bike_stands;
            _bike_stands = bike_stands;
            _banking = banking;
        }

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [DataMember]
        public int AvailableBikes
        {
            get { return _available_bikes; }
            set { _available_bikes = value; }
        }


        [DataMember]
        public int AvailableBikeStands
        {
            get { return _available_bike_stands; }
            set { _available_bike_stands = value; }
        }

        [DataMember]
        public int BikeStands
        {
            get { return _bike_stands; }
            set { _bike_stands = value; }
        }

        [DataMember]
        public bool Banking
        {
            get { return _banking; }
            set { _banking = value; }
        }
        
    }
}
