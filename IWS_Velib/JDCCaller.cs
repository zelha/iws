using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IWS_Velib
{
    class JDCCaller
    {

        private Dictionary<string, City> cache;
        private static JDCCaller instance;

        private JDCCaller()
        {
            cache = new Dictionary<string, City>();
        }

        public static JDCCaller GetJDCCaller()
        {
            if (instance == null)
            {
                instance = new JDCCaller();
            }
            return instance;
        }

        private List<VelibStation> call(string city)
        {
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract="
                   + city + "&apiKey=6e0b0fc2be5ababcfad0e80ea1dea2ad81e6f4ac");
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();
            VelibStation[] listStations = JsonConvert.DeserializeObject<VelibStation[]>(responseFromServer);

            return listStations.OfType<VelibStation>().ToList();
        }

        private void Call(string _city,TimeSpan span)
        {
            if (cache.ContainsKey(_city))
            {
                City c = cache[_city];

                DateTime limit = c.timeStamp + span;
                if (limit < DateTime.Now)
                {
                    City updated = new City
                    {
                        stations = call(_city),
                        timeStamp = DateTime.Now
                    };
                    cache[_city] =  c;
                }
            }
            else
            {
                City c = new City
                {
                    stations = call(_city),
                    timeStamp = DateTime.Now
                };
                cache.Add(_city, c);
            }
        }

        public void CallForStations(string _city)
        {
            Call(_city, new TimeSpan(0, 5, 0));
        }
        public void CallForCity(string _city)
        {
            Call(_city, new TimeSpan(30, 0, 0, 0));
        }

        public List<VelibStation> getCity(string city)
        {
            CallForCity(city);

            if (!cache.ContainsKey(city))
            {
                System.Console.WriteLine("ERROR forgot to call?");
                throw new Exception("city not found");
            }
            return cache[city].stations;
        }

        public VelibStation getStation(string city, string station)
        {
            CallForStations(city);
            if (!cache.ContainsKey(city))
            {
                System.Console.WriteLine("ERROR forgot to call?");
            }
            City c = cache[city];
            foreach (VelibStation s in c.stations)
            {
                if (s.Name.ToLower().Contains(station.ToLower()))
                {
                    return s;
                }
            }
            throw new Exception("Station not found"); }
                      
        }
        public class City
        {
            public DateTime timeStamp { get; set; }
            public List<VelibStation> stations { get; set; }
        }
    }

