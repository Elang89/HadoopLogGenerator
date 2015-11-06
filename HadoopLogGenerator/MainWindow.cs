using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Threading;

namespace HadoopLogGenerator
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void createLogButton_Click(object sender, EventArgs e)
        {

            this.createLogButton.Enabled = false;
            Task task1 = Task.Factory.StartNew(() => insertLogs(1, 20000));
            Task task2 = Task.Factory.StartNew(() => insertLogs(2, 20000));
            Task task3 = Task.Factory.StartNew(() => insertLogs(3, 20000));
            Task task4 = Task.Factory.StartNew(() => insertLogs(4, 20000));


        }

        private void insertLogs(int id, int cycleSleepTime)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49496");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            LogGeneratorObject logObject = new LogGeneratorObject();
            StringContent jsonContent;
            HttpResponseMessage response;

            for (int i = 0; i < 125000; i++)
            {
                try
                {
                    Console.WriteLine("task {0} has begun inserting log.", id);
                    JObject jObject = logObject.generateJson();
                    string json = JsonConvert.SerializeObject(jObject);
                    jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
                    response = client.PostAsync("/Logs/Create", jsonContent).Result;
                    Console.WriteLine("task {0} has inserted log", id);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.ToString());
                }
            }
        }
    }
}
