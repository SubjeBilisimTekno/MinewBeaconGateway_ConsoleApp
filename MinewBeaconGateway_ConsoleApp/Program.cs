using System;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using MQTTnet.Client;

namespace MinewBeaconGW_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string clientId = Guid.NewGuid().ToString();

            uPLibrary.Networking.M2Mqtt.MqttClient mqttClient = new uPLibrary.Networking.M2Mqtt.MqttClient("broker.hivemq.com");
            mqttClient.MqttMsgPublishReceived += client_recievedMessage;
            mqttClient.Connect(clientId);
            Console.WriteLine("subscribe to : /gw/ac233fc04a91/status");
            mqttClient.Subscribe(new String[] { "/gw/ac233fc04a91/status" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            //mqttClient.Subscribe(new String[] { "/gw/ac233fc04a91/action" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            //mqttClient.Subscribe(new String[] { "/gw/ac233fc04a91/action/response" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        }

        private static void client_recievedMessage(object sender, MqttMsgPublishEventArgs e)
        {
            #region mac
            //"mac":"AC233F52BD2F" GOKNUR --
            //"mac":"AC233FA202DE" SANEM // 
            //"mac":"AC233F52B87A","bleName":"AYKUT"
            //"mac":"AC233F52B883","bleName":"HAKKI"
            //"mac":"AC233F52B86F","bleName":"SUBJECT"
            //"mac":"AC233F52B865","bleName":"SAID"
            //"mac":"AC233F52B85B","bleName":"BETUL"
            //"mac":"AC233F52B86E","bleName":"DELAL"
            #endregion            
            #region raw
            ////-*--------------------------------------------------------------
            //using (var reader = new JsonTextReader(new StringReader(System.Text.Encoding.Default.GetString(e.Message))))
            //{
            //    //while (reader.Read())
            //    //{
            //    //    Console.WriteLine("{0} - {1} - {2}",
            //    //                     reader.TokenType, reader.ValueType, reader.Value);

            //    //}               
            //}
            ////-*--------------------------------------------------------------
            ////-*--------------------------------------------------------------
            //var message = System.Text.Encoding.Default.GetString(e.Message);
            //System.Console.WriteLine("Message received: " + message + " \n");
            ////-*--------------------------------------------------------------
            ///
            //var myJsonString = File.ReadAllText(message);
            //JObject json = JObject.Parse(myJsonString);

            //System.Console.WriteLine("Message received: " + json);
            //throw new NotImplementedException();
            #endregion
            #region officedataiBeacon
            //var json5 = System.Text.Encoding.Default.GetString(e.Message);
            //List<Item> jsons = JsonConvert.DeserializeObject<List<Item>>(json5);

            //foreach (Item dets2 in jsons)
            //{
            //    if (dets2.type != "Unknown")
            //    {
            //        Console.WriteLine("Time :" + dets2.timestamp + " Type :" + dets2.type + " Mac :" + dets2.mac + " Name :" + dets2.bleName + " rssi :" + dets2.rssi);
            //    }

            //}
            #endregion
            var message = System.Text.Encoding.Default.GetString(e.Message);
            List<Item> jsons = JsonConvert.DeserializeObject<List<Item>>(message);
            foreach (Item element in jsons)
            {

                if (element.type != "Unknown")
                {
                    Console.WriteLine("Message received: Timestamp: " + element.timestamp + " Mac :" + element.mac + " rssi :" + element.rssi + " Name :" + element.bleName + " \n");
                }
                if (element.mac == "AC233F52BD2F")
                {
                    Console.WriteLine("Message received: Timestamp: " + element.timestamp + " Mac :" + element.mac + " rssi :" + element.rssi + " Name :GOKNUR " + " \n");
                }
                if (element.mac == "AC233FA202DE")
                {
                    Console.WriteLine("Message received: Timestamp: " + element.timestamp + " Mac :" + element.mac + " rssi :" + element.rssi + " Name :SANEM " + " \n");
                }

            }
            ////-*--------------------------------------------------------------           

        }

        public class Item
        {
            public DateTime timestamp;
            public string mac;
            public string rssi;

            public string type;
            public string bleName;
            public string ibeaconMajor;
            public string rawData;
        }
    }
}
