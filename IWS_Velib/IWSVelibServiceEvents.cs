using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IWS_Velib
{
    interface IWSVelibServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void LastAvailableBikesCalculated(string city, string station, VelibStation result);

        [OperationContract(IsOneWay = true)]
        void LastAvailableBikesCalculationFinished();


    }
}
