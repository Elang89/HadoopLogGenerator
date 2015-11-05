using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace HadoopLogGenerator
{
    class LogGeneratorObject
    {
        private Assembly assembly;
        private String resourceName;
        private List<String> errorCodes;
        private List<String> errorMessages;
        private List<String> stackTraces;
        private String result;
        private Stream stream;
        private Random random = new Random();
        private Random dateRandom = new Random();
        private StreamReader reader;
        private static LogGeneratorObject logObject;
        private List<List<String>> logList;

        private LogGeneratorObject()
        {
            logList = new List<List<String>>();
            resourceName = "HadoopLogGenerator.Resources.ProductList.json";
            assembly = Assembly.GetExecutingAssembly();
            stream = assembly.GetManifestResourceStream(resourceName);
            reader = new StreamReader(stream);
            result = reader.ReadToEnd();
            reader.Close();
            JObject jsonObject = JObject.Parse(result);
            JArray jsonArray = (JArray)jsonObject["products"];
            List<String> list;
            generateLogDetailList();
            String name;
            String version;
            foreach (var i in jsonArray)
            {
                name = i["name"].ToString();
                version = i["version"].ToString();
                list = new List<String>(createErrorDetails(name, version));
                logList.Add(list);
            }
        }
        
        private List<String> createErrorDetails(String product, String version)
        {
            DateTime start = new DateTime(2014, 1, 1);

            int range = (DateTime.Today - start).Days;
            start = start.AddDays(dateRandom.Next(range));

            List <String> errorDetailList = new List<String>();
            int number = 0;
            number = random.Next(0, errorCodes.Count - 1);
            String[] errorDetailListArray = { start.ToString(), product, version,
                errorCodes[number], errorMessages[number], stackTraces[number]};
            errorDetailList.AddRange(errorDetailListArray);
            return errorDetailList;
        }

        private void generateLogDetailList()
        {
            String [] arrayErrorCodes = { "AccessViolation", "FileUploadException",
                                "ConnectionErrorException", "IOException",
                                "SqlException", "ArgumentNullException",
                                "FormatException", "NotSupportedException"};
            String [] arrayErrorMsgs = {"Attempted to read or write protected memory. This is often an indication that other memory is corrupt.",
                                        "There is not enough space on the disk.",
                                        "Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host.",
                                        "The file exists.", "Server could not be found", "Value cannot be null.",  "Input string was not in a correct format.",
                                        "NotSupportedException" };
            errorCodes = new List<String>(arrayErrorCodes);
            errorMessages = new List<String>(arrayErrorMsgs);
            stackTraces = new List<String>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            String resourceName = "HadoopLogGenerator.Resources.StackTraceExamples.txt";
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            StreamReader reader = new StreamReader(stream);
            String line;
            String paragraph = "";
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                paragraph += line;

                if (String.IsNullOrWhiteSpace(line))
                {
                    stackTraces.Add(paragraph);
                    paragraph = "";
                }
            }

        }

        public static LogGeneratorObject getInstance()
        {
            if (logObject == null)
            {
                logObject = new LogGeneratorObject();
            }
            return logObject;
        }

        public JObject generateJson()
        {
            int number = random.Next(0, logList.Count-1);
            List<String> randomList = new List<String>(logList[number]);
            JObject jsonObject = new JObject(
                new JProperty("errorDate", randomList[0]),
                new JProperty("product", randomList[1]),
                new JProperty("version", randomList[2]),
                new JProperty("errorCode", randomList[3]),
                new JProperty("errorMessage", randomList[4]),
                new JProperty("stackTrace", randomList[5]));

            return jsonObject;
        }
     
    }
}
