using Microsoft.VisualStudio.TestTools.UnitTesting;
using DailyProgrammer.Intermediate.Challenge_360;
using System.Numerics;
using DailyProgrammerTests.Properties;

namespace DailyProgrammerTests.Intermediate.Challenge_360
{
    [TestClass]
    public class NearestAeroplane_Tests
    {
        private const float EifelTowerLatitude = 48.8584f;
        private const float EifelTowerLongitude = 2.2945f;
        private const float JFKAirportLatitude = 40.6413f;
        private const float JFKAirportLongitude = -73.7781f;

        [TestMethod]
        public void FindNearestAeroplane_EifelTower_Euclidean()
        {
            AeroplaneInformation nearestPlane = FindNearestAeroplane_360.GetNearestAeroplane(EifelTowerLatitude, EifelTowerLongitude, GetAeroplaneLocationsFromFile(), true);
            Assert.AreEqual("LBT521  ", nearestPlane.Callsign);
        }

        [TestMethod]
        public void FindNearestAeroplane_JFKAirport_Euclidean()
        {
            AeroplaneInformation nearestPlane = FindNearestAeroplane_360.GetNearestAeroplane(JFKAirportLatitude, JFKAirportLongitude, GetAeroplaneLocationsFromFile(), true);
            Assert.AreEqual("DAL845  ", nearestPlane.Callsign);
        }

        [TestMethod]
        public void FindNearestAeroplane_EifelTower_Geodesic()
        {
            AeroplaneInformation nearestPlane = FindNearestAeroplane_360.GetNearestAeroplane(EifelTowerLatitude, EifelTowerLongitude, GetAeroplaneLocationsFromFile(), false);
            Assert.AreEqual("LBT521  ", nearestPlane.Callsign);
        }

        [TestMethod]
        public void FindNearestAeroplane_JFKAirport_Geodesic()
        {
            AeroplaneInformation nearestPlane = FindNearestAeroplane_360.GetNearestAeroplane(JFKAirportLatitude, JFKAirportLongitude, GetAeroplaneLocationsFromFile(), false);
            Assert.AreEqual("DAL845  ", nearestPlane.Callsign);
        }

        private static string GetAeroplaneLocationsFromFile()
        {
            return Resources.AeroPlaneInfo;
        }
    }
}
