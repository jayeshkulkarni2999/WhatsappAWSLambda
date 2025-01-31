using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Azure.Core;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace WHATSAPP_PROD
{
    public class Function
    {
        //Whastapp URL = https://app.yellowmessenger.com/api/engagements/notifications/v2/push?bot=
        public static string whatsappUrl = System.Environment.GetEnvironmentVariable("whatsappURL");
        public static string tableName = System.Environment.GetEnvironmentVariable("tableName");
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<string> FunctionHandler(dynamic jsonstring, ILambdaContext context)
        {
            try
            {
                var body = jsonstring;
                Request objRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<Request>(body.ToString()); 
                if (objRequest != null)
                {
                    string apikey = objRequest.apiKey.ToString().Trim();

                    Function function = new Function();
                    objRequest = function.GetData(apikey, objRequest).Result;
                }
                if (objRequest.exceptionmsg != null)
                {
                    return Task.Run(() =>
                    {
                        return objRequest.exceptionmsg;
                    });
                }
                var response = CallAPI(objRequest);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> CallAPI(Request request)
        {
            try
            {
                input inputs = new input();
                user_details user = new user_details();
                user.number = request.whatsAppNumber;
                notifications notifications = new notifications();
                notifications.namespaces = request.NAMESPACE;
                notifications.sender = request.SENDER; 
                notifications.templateId = request.templateName;
                notifications.type = request.TYPE;
                notifications.paramss = request.templateParams;

                inputs.notification = notifications;
                inputs.userDetails = user;

                using (var client = new HttpClient())
                {
                string url = (whatsappUrl + request.BOT_ID);
                    string payload = Task.Run(() => JsonConvert.SerializeObject(inputs)).Result;
                    var content = new StringContent(payload, Encoding.UTF8, "application/json");
                    content.Headers.Add("x-api-key", request.x_api_key);
                    var response = client.PostAsync(url, content).Result;
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Request> GetData(string apikey, Request request)
        {
            try
            {
                var client = new AmazonDynamoDBClient();
                var table = Table.LoadTable(client, tableName);
                var item = await table.GetItemAsync(apikey);
                if (item != null)
                {
                    if (item.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(item["NAMESPACE"]))
                        {
                            request.NAMESPACE = item["NAMESPACE"];
                        }
                        if (!string.IsNullOrEmpty(item["SENDER"]))
                        {
                            request.SENDER = item["SENDER"];
                        }
                        if (!string.IsNullOrEmpty(item["TYPE"]))
                        {
                            request.TYPE = item["TYPE"];
                        }
                        if (!string.IsNullOrEmpty(item["BOT_ID"]))
                        {
                            request.BOT_ID = item["BOT_ID"];
                        }
                        if (!string.IsNullOrEmpty(item["x-api-key"]))
                        {
                            request.x_api_key = item["x-api-key"];
                        }
                    }
                    else
                    {
                        request.exceptionmsg = "No match found for this API_KEY";
                    }
                }
                else
                {
                    request.exceptionmsg = "Please check the API_KEY provided";
                }
                return request;
            }
            catch (Exception ex)
            {
                request.exceptionmsg = ex.Message + " Please check the api key provided";
                return request;
                //throw ex;
            }
        }
    }
}
