using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Text;

/*
 * Link to reddit problem
 * https://old.reddit.com/r/dailyprogrammer/comments/8i5zc3/20180509_challenge_360_intermediate_find_the/
 */
namespace DailyProgrammer.Intermediate.Challenge_360
{
    public static class FindNearestAeroplane_360
    {
        const double EARTH_RADIUS = 6371.0088; // Radius of earth in km

        public static AeroplaneInformation GetNearestAeroplane(float locationLatitude, float locationLongitude, string aeroplaneStates, bool useEuclideanDistance = true)
        {
            AeroplaneAPIResponse planeInformation = ConvertAPIResponseToObject(aeroplaneStates);
            AeroplaneInformation closestPlane = planeInformation.States[0];
            double closestPlaneDist = GetDistance(locationLatitude, locationLongitude, planeInformation.States[0].Latitude, planeInformation.States[0].Longitude, useEuclideanDistance);
            for(int i = 1; i < planeInformation.States.Count; i++)
            {
                
                // Check if both the lat and long are valid otherwise skip and go to next plane
                if (Math.Abs(planeInformation.States[i].Longitude) > 180 || Math.Abs(planeInformation.States[i].Latitude) > 90)
                {
                    continue;
                }
                else
                {
                    double distance = GetDistance(locationLatitude, locationLongitude, planeInformation.States[i].Latitude, planeInformation.States[i].Longitude, useEuclideanDistance);
                    if (distance < closestPlaneDist)
                    {
                        closestPlane = planeInformation.States[i];
                        closestPlaneDist = distance;
                    }
                }
            }

            return closestPlane;
        }

        public static string GetAeroplaneLocationsFromApi()
        {
            const string apiUrl = "https://opensky-network.org/api/states/all";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream);
                    return reader.ReadToEnd();
                }
            }
            catch(WebException ex)
            {
                throw ex;
            }
        }

        private static AeroplaneAPIResponse ConvertAPIResponseToObject(string apiResponseString)
        {
            RawApiResponse json = JsonConvert.DeserializeObject<RawApiResponse>(apiResponseString);
            return new AeroplaneAPIResponse(json.Time, json.States);
        }

        private static double GetDistance(float locationLatitude, float locationLongitude, float planeLatitude, float planeLongitude, bool euclidean = true)
        {
            if (euclidean)
            {
                return GetEuclideanDistance(locationLatitude, locationLongitude, planeLatitude, planeLongitude);
            }
            else
            {
                return GetGeodesicDistance(locationLatitude, locationLongitude, planeLatitude, planeLongitude);
            }
        }

        private static double GetEuclideanDistance(float locationLatitude, float locationLongitude, float planeLatitude, float planeLongitude)
        {
            return Math.Sqrt(Math.Pow(locationLatitude - planeLatitude, 2) + Math.Pow(locationLongitude - planeLongitude, 2));
        }

        /*
         * Geodesic distance formula and explanation
         * https://en.wikipedia.org/wiki/Great-circle_distance
         */
        private static double GetGeodesicDistance(float locationLatitude, float locationLongitude, float planeLatitude, float planeLongitude)
        {
            double centralAngle = Math.Acos(
                Math.Sin(ConvertDegreesToRadians(locationLongitude)) * Math.Sin(ConvertDegreesToRadians(planeLongitude)) + 
                Math.Cos(ConvertDegreesToRadians(locationLongitude)) * Math.Cos(ConvertDegreesToRadians(planeLongitude)) * Math.Cos(ConvertDegreesToRadians(locationLatitude - planeLatitude)));
            return EARTH_RADIUS * centralAngle;
        }

        private static double ConvertDegreesToRadians(float degrees)
        {
            return (Math.PI / 180) * degrees;
        }

    }

    /*
     * Documentation for descriptions of api response properties
     * https://opensky-network.org/apidoc/rest.html
     */
    public enum AeroplaneData
    {
        Icao24 = 0,
        Callsign = 1,
        OriginCountry = 2,
        TimePosition = 3,
        LastContact = 4,
        Longitude = 5,
        Latitude = 6,
        BaroAltitude = 7,
        OnGround = 8,
        Velocity = 9,
        TrueTrack = 10,
        VerticalRate = 11,
        Sensors = 12,
        GeoAltitude = 13,
        Squack = 14,
        Spi = 15,
        PositionSource =16, 
    }

    public class RawApiResponse
    {
        public int Time { get; set; }
        public JArray States { get; set; }
    }


    /*
     * Probably could use just the raw response but using this allows use of custom object to represent the plane information and can deal with some of the conditionally null values
     */
    public class AeroplaneAPIResponse
    {
        public int Time { get; set; }
        public List<AeroplaneInformation> States { get; set; }

        public AeroplaneAPIResponse(int time, JArray states)
        {
            this.Time = time;
            this.States = new List<AeroplaneInformation>();

            for(int i = 0; i < states.Count; i++)
            {
                this.States.Add(new AeroplaneInformation((JArray)states[i]));
            }
        }
    }

    /*
     * This isn't needed but it allows the use of primitive types rather than JArray and JToken etc
     * 
     * Note: not all properties are initialized as they are not needed to get the distance
     */
    public class AeroplaneInformation
    {
        public string Icao24 { get; set; }
        public string Callsign { get; set; }
        public string OriginCountry { get; set; }
        public int TimePosition { get; set; }
        public int LastContact { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float BaroAltitude { get; set; }
        public bool OnGround { get; set; }
        public float Velocity { get; set; }
        public float TrueTrack { get; set; }
        public float VerticalRate { get; set; }
        public int[] Sensors { get; set; }
        public float GeoAltitude { get; set; }
        public string Squack { get; set; }
        public bool Spi { get; set; }
        public int PositionSource { get; set; }

        public AeroplaneInformation(JArray information)
        {
            this.Icao24 = (string)information[(int)AeroplaneData.Icao24];
            this.Callsign = (string)information[(int)AeroplaneData.Callsign];

            if (information[(int)AeroplaneData.Longitude].Type == JTokenType.Float && information[(int)AeroplaneData.Latitude].Type == JTokenType.Float)
            {
                this.Longitude = information[(int)AeroplaneData.Longitude].Value<float>();
                this.Latitude = information[(int)AeroplaneData.Latitude].Value<float>();
            }
            else
            {
                this.Longitude = 999;
                this.Latitude = 999;
            }

        }
    }
}
