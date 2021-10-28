using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleMqttClient
{
    public partial class Form1 : Form
    {
        private readonly ControlLog controlLog = new();
        private InterfaceMqtt interfaceMqtt;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            interfaceMqtt = new(this);
            PanelLog.Controls.Add(controlLog);

            textBoxAddress.Text = RegistryMqtt.Address;
            textBoxPort.Text = RegistryMqtt.Port.ToString();
            textBoxId.Text = RegistryMqtt.Id;
            textBoxPassword.Text = RegistryMqtt.Password;
            textBoxTopic.Text = RegistryMqtt.Topic;
        }

        public void SetLogText(string text)
        {
            if (controlLog != null)
            {
                controlLog.SetLogText(text);
            }
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            string address = textBoxAddress.Text;
            if (address.Length <= 0)
            {
                MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                    "주소 입력이 필요합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(textBoxPort.Text, out int port))
            {
                MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                    "포트 입력 값이 잘못되었습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string id = textBoxId.Text;
            if (id.Length <= 0)
            {
                MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                    "아이디 입력이 필요합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string password = textBoxPassword.Text;
            if (password.Length <= 0)
            {
                MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                    "패스워드 입력이 필요합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string topic = textBoxTopic.Text;
            if (topic.Length <= 0)
            {
                MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                    "토픽 입력이 필요합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (interfaceMqtt != null)
            {
                bool isConnecnt = false;
                if (interfaceMqtt.mqttClient == null)
                {
                    interfaceMqtt.ConnectMQTTServer(address, port, id, password, topic);
                    isConnecnt = true;
                }
                else if (!interfaceMqtt.mqttClient.IsConnected)
                {
                    interfaceMqtt.ConnectMQTTServer(address, port, id, password, topic);
                    isConnecnt = true;
                }

                if (isConnecnt)
                {
                    RegistryMqtt.Address = address;
                    RegistryMqtt.Port = port;
                    RegistryMqtt.Id = id;
                    RegistryMqtt.Password = password;
                    RegistryMqtt.Topic = topic;
                }
                else
                {
                    MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                        "이미 서버와 연결 중 입니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ButtonDisconnect_Click(object sender, EventArgs e)
        {
            if (interfaceMqtt != null)
            {
                if (interfaceMqtt.mqttClient != null)
                {
                    interfaceMqtt.DisconnectMQTTServer();
                }
            }
        }

        private void ButtonChangeTopic_Click(object sender, EventArgs e)
        {
            if (textBoxTopic.Text.Length > 0)
            {
                string topic = textBoxTopic.Text;
                if (RegistryMqtt.Topic != topic)
                {
                    if (interfaceMqtt != null)
                    {
                        if (interfaceMqtt.mqttClient != null)
                        {
                            if (interfaceMqtt.mqttClient.IsConnected)
                            {
                                var task = Task.Run(() => interfaceMqtt.ChangeUnsubscribeAsync(topic));
                                task.ContinueWith(x =>
                                {
                                    if (x.Result)
                                    {
                                        RegistryMqtt.Topic = topic;
                                    }
                                    else
                                    {
                                        MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                                            "토픽 변경 실패했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }

                                }, TaskScheduler.FromCurrentSynchronizationContext());
                            }
                            else
                            {
                                MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                                    "서버와 연결 중이 아닙니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                                "서버와 연결 중이 아닙니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                            "서버와 연결 중이 아닙니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                        "토픽이 동일하여 변경할 수 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                    "토픽 입력이 필요합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            if (textBoxSendData.Text.Length > 0)
            {
                if (interfaceMqtt != null)
                {
                    var task = Task.Run(() => interfaceMqtt.SendData(textBoxSendData.Text));
                    task.ContinueWith(x =>
                    {
                        bool rslt = x.Result;
                        if (!rslt)
                        {
                            MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                                "데이터 전송 실패했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
            else
            {
                MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true },
                    "데이터 입력이 필요합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            textBoxSendData.Text = string.Empty;
        }
    }
}
