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
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49496");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            LogGeneratorObject logObject = LogGeneratorObject.getInstance();
            JObject jObject = logObject.generateJson();
            string json = JsonConvert.SerializeObject(jObject);
            Console.WriteLine(json);
            StringContent jsonContent;
            HttpResponseMessage response;

            try
            {
                jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = client.PostAsync("/Logs/Create", jsonContent).Result;
                Console.WriteLine(response);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message.ToString());
            }
            

        }
    }
}
