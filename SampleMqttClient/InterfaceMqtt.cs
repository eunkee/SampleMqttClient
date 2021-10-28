using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SampleMqttClient
{
    public class InterfaceMqtt
    {
        private static Form1 Parentform;
        public IMqttClient mqttClient = null;
        private string currentTopic = string.Empty;

        public InterfaceMqtt(Form1 form)
        {
            Parentform = form;
        }

        public void ConnectMQTTServer(string address, int port, string id, string password, string topic)
        {
            if (mqttClient != null)
            {
                //DisconnectMQTTServer와 중복이지만 ContinueWith가 필요해서
                var task = Task.Run(() =>
                {
                    try
                    {
                        mqttClient.DisconnectAsync();
                        Task.Delay(TimeSpan.FromSeconds(1));
                    }
                    finally
                    {
                        mqttClient = null;
                    }
                });
                task.ContinueWith(x =>
                {
                    Task.Run(() => OnMQTTServer(address, port, id, password, topic));
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                Task.Run(() => OnMQTTServer(address, port, id, password, topic));
            }
        }

        public void DisconnectMQTTServer()
        {
            if (mqttClient != null)
            {
                var task = Task.Run(() =>
                {
                    try
                    {
                        mqttClient.DisconnectAsync();
                        Task.Delay(TimeSpan.FromSeconds(1));
                    }
                    finally
                    {
                        mqttClient = null;
                    }
                });
            }
        }

        //change Topic
        public async Task<bool> ChangeUnsubscribeAsync(string NewTopic)
        {
            bool rslt = false;
            try
            {
                if (mqttClient != null)
                {
                    if (mqttClient.IsConnected)
                    {
                        await mqttClient.UnsubscribeAsync(currentTopic);
                        await mqttClient.SubscribeAsync(NewTopic);
                        currentTopic = NewTopic;
                        rslt = true;
                    }
                }
            }
            finally { }

            return rslt;
        }

        //Connect + Topic Subscribe
        public async Task OnMQTTServer(string address, int port, string id, string password, string topic)
        {
            try
            {
                mqttClient = new MqttFactory().CreateMqttClient();
            }
            finally { }

            if (mqttClient != null)
            {
                currentTopic = topic;
                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer(address, port)
                    .WithCredentials(id, password)
                    .WithCleanSession()
                    .Build();
                try
                {
                    await mqttClient.ConnectAsync(options);
                    await mqttClient.SubscribeAsync(currentTopic);
                    Parentform.SetLogText("### CONNECTED WITH SERVER ###");

                    try
                    {
                        mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(e => 
                        {
                            Parentform.SetLogText("### CONNECTED WITH SERVER ###");
                            return Task.CompletedTask;
                        });

                        mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(/*async*/ e =>
                        {
                            Parentform.SetLogText("### DISCONNECTED WITH SERVER ###");

                            //reconnect
                            //await Task.Delay(TimeSpan.FromSeconds(5));
                            //try
                            //{
                            //    await mqttClient.ConnectAsync(options);
                            //    await mqttClient.SubscribeAsync(currentTopic);
                            //}
                            //catch (Exception ex)
                            //{
                            //    System.Diagnostics.Trace.WriteLine(ex.Message);
                            //    System.Diagnostics.Trace.WriteLine(ex.StackTrace);
                            //}
                        });

                        //받기
                        mqttClient.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(e =>
                        {
                            string topic = e.ApplicationMessage.Topic.Split('/')[^1];
                            try
                            {
                                string json = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                                Parentform.SetLogText($"{topic}: {json}");
                            }
                            catch { }
                        });
                    }
                    catch (Exception ex)
                    {
                        Parentform.SetLogText("### FAILED CONNECTED SERVER HANDLER ###");
                        System.Diagnostics.Trace.WriteLine(ex.Message);
                        System.Diagnostics.Trace.WriteLine(ex.StackTrace);
                    }
                }
                catch (Exception ex)
                {
                    Parentform.SetLogText("### FAILED CONNECTED SERVER ###");
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                    System.Diagnostics.Trace.WriteLine(ex.StackTrace);
                }
            }
        }

        //Client => Server: SendData
        public async Task<bool> SendData(string json)
        {
            bool rslt = false;
            try
            {
                if (mqttClient != null)
                {
                    if (mqttClient.IsConnected)
                    {
                        //class -> json
                        //string json = JsonConvert.SerializeObject(class);

                        //publish
                        var message = new MqttApplicationMessageBuilder()
                        .WithTopic(currentTopic)
                        .WithPayload(json)
                        .WithAtMostOnceQoS()
                        .WithRetainFlag(false)
                        .Build();
                        await mqttClient.PublishAsync(message);
                        rslt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                System.Diagnostics.Trace.WriteLine(ex.StackTrace);
            }
            return rslt;
        }
    }
}
