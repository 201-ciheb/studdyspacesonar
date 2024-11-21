using PHIASPACE.RTDMS.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PHIASPACE.RTDMS.DAL.Service;

namespace PHIASPACE.RTDMS.Helpers
{
    public class TreeHelper
    {

        private readonly ZoneService _zoneService;

        public TreeHelper(ZoneService zoneService)
        {
            _zoneService = zoneService;
        }

        public async Task<JObject> GetTree1Async()
        {
            var main = new JObject();
            var zones =  _zoneService.GetZones();  // Fetch zones
            var counties = await _zoneService.GetCountiesAsync();  // Fetch counties asynchronously
            main.Add("text", "Kenya");

            // Get distinct zones
            var distinctZones = zones.GroupBy(z => z.Id).Select(g => g.First());

            JArray zoneArray = new JArray();
            foreach (var zone in distinctZones)
            {
                var zoneObject = new JObject();
                zoneObject.Add("text", zone.Name);  // Zone name
                JArray countyArray = new JArray();

                // Get counties that belong to the current zone by ZoneId
                var zoneCounties = counties.Where(c => c.ZoneId == zone.Id).ToList();

                foreach (var county in zoneCounties)
                {
                    JObject countyObject = new JObject();
                    countyObject.Add("text", county.CountyName);  // County name

                    // Ensure the county code is correctly generated
                    var countyCode = county.CountyCode ?? "Unknown";  // Handle cases where county code might be null
                    countyObject.Add("id", "2_" + countyCode);

                    countyArray.Add(countyObject);
                }

                // Only add children if there are counties
                if (countyArray.Count > 0)
                {
                    zoneObject.Add("children", countyArray);
                }

                zoneArray.Add(zoneObject);
            }

            main.Add("children", zoneArray);

            // Log the resulting tree structure
            Console.WriteLine("Generated Tree: " + main.ToString());

            return main;
        }
        public async Task<JObject> GetTreeAsync(string location_id = null)
        {
            var sel_object = new JObject();
            sel_object.Add("opened", true);
            sel_object.Add("selected", true);

            var main = new JObject();
            main.Add("text", "Kenya");
            main.Add("id", "0");

            if (location_id != null && location_id == "0_0")
            {
                main.Add("state", sel_object);
            }
            else if (location_id != null)
            {
                var s_object = new JObject();
                s_object.Add("opened", true);
                main.Add("state", s_object);
            }

            // Fetch zone and county data
            var tree_data =  _zoneService.GetZones();
            var zone_data =  _zoneService.GetZones();
            var county_data = await _zoneService.GetCountiesAsync();

            // Get distinct zones
            var zones = tree_data.Select(e => e.Name).Distinct();
            Console.WriteLine("Distinct Zones: " + JsonConvert.SerializeObject(zones));

            JArray zone_array = new JArray();

            foreach (var zone in zones)
            {
                // Filter counties based on ZoneId and ensure it is tied correctly
                var zoneEntity = zone_data.FirstOrDefault(e => e.Name == zone);
                var zoneCounties = county_data.Where(c => c.ZoneId == zoneEntity.Id).ToList();

                var zoneObject = new JObject();
                zoneObject.Add("text", zone);
                zoneObject.Add("id", "1_" + zoneEntity.Code);

                if (location_id != null && location_id == "1_" + zoneEntity.Code)
                {
                    zoneObject.Add("state", sel_object);
                }

                JArray countyArray = new JArray();

                foreach (var county in zoneCounties)
                {
                    var countyObject = new JObject();
                    countyObject.Add("text", county.CountyName);  // County name
                    countyObject.Add("id", "2_" + (county.CountyCode ?? "Unknown"));  // Ensure county code

                    // You can add additional subregion or EA data here if necessary
                    // For now, we'll just attach counties under each zone

                    countyArray.Add(countyObject);
                }

                if (countyArray.Count > 0)
                {
                    zoneObject.Add("children", countyArray);
                }

                zone_array.Add(zoneObject);
            }

            main.Add("children", zone_array);

            // Log the final tree structure
            Console.WriteLine("Generated Tree: " + main.ToString());

            return main;
        }

        public static JObject GetTree2()
        {
            var main = new JObject();

            var tree_data = new List<dynamic>();// Utils.GetTreeData();
            main.Add("text", "Kenya");

            //get distinct counties from the data
            var counties = tree_data.Select(e => e.county_name).Distinct();
            JArray county_array = new JArray();
            foreach (var county in counties)
            {
                JObject subregion_object = new JObject();
                subregion_object.Add("text", county);
                var eas = new List<dynamic>(); // Utils.GetEa();
                var ea_array = new JArray();

                foreach (var ea in eas.Where(e => e.CountyName == county))
                {
                    var jea = new JObject();
                    jea.Add("text", ea.Id.ToString("D4") + " - " + ea.EaName);
                    ea_array.Add(jea);
                }

                JObject county_object = new JObject();
                county_object.Add("text", county);
                county_object.Add("children", ea_array);
                county_array.Add(county_object);
            }

            main.Add("children", county_array);
            var value = main.ToString();
            return main;
        }

        public JObject GetZoneTree()
        {
            var sel_object = new JObject();
            sel_object.Add("opened", true);
            sel_object.Add("selected", true);

            var main = new JObject();

            var tree_data = new List<dynamic>(); //Utils.GetTreeData();
            main.Add("text", "Kenya");

            var zone_data = _zoneService.GetZones();
            var county_data = _zoneService.GetCountiesAsync();

            var zones = tree_data.Select(e => e.zone).Distinct();
            JArray zone_array = new JArray();
            foreach (var zone in zones)
            {
                var jobject = new JObject();
                jobject.Add("text", zone);
                var zone_code = "null";// zone_data.FirstOrDefault(e => e.Name == zone).Code;
                jobject.Add("id", zone_code);

                zone_array.Add(jobject);
            }
            main.Add("children", zone_array);
            return main;
        }

        public JObject GetWebTree()
        {
            var sel_object = new JObject();
            sel_object.Add("opened", true);
            sel_object.Add("selected", true);

            var main = new JObject();

            var tree_data = new List<dynamic>();// _zoneService.GetTreeData();
            main.Add("text", "Kenya");

            var zone_data = _zoneService.GetZones();
            var county_data = _zoneService.GetCountiesAsync();
            var zones = tree_data.Select(e => e.zone).Distinct();

            JArray web_array = new JArray();

            for (int i = 1; i <= 6; i++)
            {
                var jobject = new JObject();
                jobject.Add("text", "Web " + i);
                jobject.Add("id", "web_" + i);

                JArray zone_array = new JArray();
                foreach (var zone in zones)
                {
                    var zone_object = new JObject();
                    zone_object.Add("text", zone);
                    string zone_code = null;//zone_data.FirstOrDefault(e => e.Name == zone).Code;
                    zone_object.Add("id", "web" + i + "_" + zone_code);

                    zone_array.Add(zone_object);
                }
                jobject.Add("children", zone_array);
                web_array.Add(jobject);
            }

            main.Add("children", web_array);
            return main;
        }

    }

}
