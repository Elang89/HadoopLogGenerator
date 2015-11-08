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

            Task task1 = Task.Factory.StartNew(() => insertLogs(1));
            //Task task2 = Task.Factory.StartNew(() => insertLogs(2));
            //Task task3 = Task.Factory.StartNew(() => insertLogs(3));

            this.createLogButton.Enabled = false;

            /*if (task1.IsCompleted && task2.IsCompleted && task3.IsCompleted)
            {
                this.createLogButton.Enabled = false;
            }*/

        }

        private void insertLogs(int id)
        {
            
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(360);
            client.BaseAddress = new Uri("http://localhost:49496");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            StringContent jsonContent;
            HttpResponseMessage response;
            try
            {
                string json = buildJsonLog(id);
                jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = client.PostAsync("/Logs/Create", jsonContent).Result;
                var contents = response.Content.ReadAsStringAsync();
                Console.WriteLine(contents.Result);
                Console.WriteLine("Task {0} finished", id);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
        }

        private string buildJsonLog(int id)
        {
            LogGeneratorObject logObject = new LogGeneratorObject();
            JObject jObject;
            JArray finalObject = new JArray();
            string jsonLog;
            for (int i = 0; i < 500000; i++)
            {
                Console.WriteLine("Task {0} inserting log number " + i.ToString(), id);
                Console.WriteLine("----------------------------");
                jObject = logObject.generateJson();
                finalObject.Add(jObject);
            }
            jsonLog = JsonConvert.SerializeObject(finalObject);
            return jsonLog;
        }
    }
}
