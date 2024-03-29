﻿using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Wake_On_Lan_Lite
{

    //Class that retrieves the mac addresses from the mac.json file
    class fileControl
    {

        private const string PATH = @"C:\Wake On Lan Lite";
        private const string PATHFILE = @"C:\Wake On Lan Lite\mac_address.json";

        //Function that creates the folder and the file if they don't exist
        public void createFileIfNotExist()
        {
            List<int> id = new List<int>();
            List<string> name = new List<string>();
            List<string> address = new List<string>();
            id.Add(0);
            name.Add("+");
            address.Add("aa:bb:cc:dd:ee:ff");
            var objects = new { id, name, address };

            if (Directory.Exists(PATH))
            {
                if (!File.Exists(PATHFILE))
                {
                    File.Create(PATHFILE).Dispose();
                    File.WriteAllText(PATHFILE, JsonConvert.SerializeObject(objects));
                }
            }
            else
            {
                Directory.CreateDirectory(PATH);
                File.Create(PATHFILE).Dispose();
                File.WriteAllText(PATHFILE, JsonConvert.SerializeObject(objects));
            }
        }

        //Function that add a new device in the json file
        public void addAddress(Device device)
        {
            List<Device> devices = getAllAddresses();
            devices.Add(device);

            List<int> id = new List<int>();
            List<string> name = new List<string>();
            List<string> address = new List<string>();

            foreach(Device d in devices)
            {
                id.Add(d.ID);
                name.Add(d.NAME);
                address.Add(d.ADDRESS); ;
            }

            var objects = new { id, name, address };          

            File.WriteAllText(PATHFILE, JsonConvert.SerializeObject(objects));
        }

        //Function that delete a device
        public void deleteAddress(Device device, MainWindow main)
        {
            List<Device> devices = getAllAddresses();
            foreach(Device d in devices.ToList())
            {
                if(d.ID == device.ID)
                {
                    devices.Remove(d);
                }
            }

            List<int> id = new List<int>();
            List<string> name = new List<string>();
            List<string> address = new List<string>();

            foreach (Device d in devices)
            {
                id.Add(d.ID);
                name.Add(d.NAME);
                address.Add(d.ADDRESS); ;
            }

            var objects = new { id, name, address };

            File.WriteAllText(PATHFILE, JsonConvert.SerializeObject(objects));
        }

        //Function that update the selected device
        public void updateAddress(Device device)
        {
            List<Device> devices = getAllAddresses();

            var updateDevice = devices.FirstOrDefault(x => x.ID == device.ID);
            updateDevice.NAME = device.NAME;
            updateDevice.ADDRESS = device.ADDRESS;

            List<int> id = new List<int>();
            List<string> name = new List<string>();
            List<string> address = new List<string>();

            foreach (Device d in devices)
            {
                id.Add(d.ID);
                name.Add(d.NAME);
                address.Add(d.ADDRESS); ;
            }

            var objects = new { id, name, address };

            File.WriteAllText(PATHFILE, JsonConvert.SerializeObject(objects));
        }

        //Function that return the list of all the devices stocked in the json file
        public List<Device> getAllAddresses()
        {
            List<Device> devices = new List<Device>();
            JObject data = JObject.Parse(File.ReadAllText(PATHFILE));
            List<int> id = data["id"].Select(t => (int)t).ToList();
            List<string> name = data["name"].Select(t => (string)t).ToList();
            List<string> address = data["address"].Select(t => (string)t).ToList();
            for(int i = 0; i < id.Count; i ++)
            {
                devices.Add(new Device() { ID = id[i], NAME = name[i], ADDRESS = address[i] });
            }

            return devices;
        }
    }
}
