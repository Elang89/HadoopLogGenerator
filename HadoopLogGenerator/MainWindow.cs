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

namespace HadoopLogGenerator
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            LogGeneratorObject logObject = LogGeneratorObject.getInstance();
            String json = logObject.generateJson();
            Console.WriteLine(json);
            JObject jObject = JObject.Parse(json);
            Console.WriteLine(jObject["errorDate"].ToString());
        }
    }
}
