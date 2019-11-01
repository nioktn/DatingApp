using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace DatingApp.Tests.Clients
{
    public class ApiCallsClient
    {
        private string baseUrl;
        //private string sessionApiPath = CommonApiPaths.SessionIdApiPath;
        private string username;
        private string password;
        private string sessionId;
        private int port;
        public TimeSpan ResponseTime { private set; get; }

        public ApiCallsClient(string baseUrl, int port)
        {
            this.baseUrl = baseUrl;
            this.port = port;
        }

        //public string StartSession(string username, string password)
        //{
        //    this.username = username;
        //    this.password = password;
        //    GetSessionId();
        //    return sessionId;
        //}

        //private void GetSessionId()
        //{
        //    try
        //    {
        //        string sessionBody = $"{{\"username\":\"{this.username}\",\"password\":\"{this.password}\"}}";
        //        var response = ExecuteWebRequest(Method.POST, GetEndPoint(sessionApiPath), sessionBody);
        //        //LogUtil.WriteDebug($"Response is {response.StatusCode}");
        //        var sessionID = JsonConvert.DeserializeObject<dynamic>(response.Content).sessionId;
        //        sessionId = sessionID.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        //LogUtil.WriteDebug(ex.GetType().Name + ": " + ex.Message);
        //        //throw;
        //    }
        //}

        //public IRestResponse GetSessionId(string body)
        //{
        //    return ExecuteWebRequest(Method.POST, GetEndPoint(sessionApiPath), body);
        //}

        /// <summary>
        /// Method gets one mandatory and one optional parameter
        /// Passed endPoint is concatenated with this.baseUrl 
        /// If some id is required in endpoint - it should be passed as second parameter
        /// </summary>
        /// <param name="endPoint">endPoint to communicate with necessary service</param>
        /// <param name="id">identifier (e.g. ndc11 or UIID)</param>
        /// <returns>string</returns>
        public string GetEndPoint(string endPoint, string id = null)
        {
            Regex regex = new Regex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.?[a-z0-9]{2,6}\b", RegexOptions.IgnoreCase);
            return regex.Match(baseUrl).Value + ":" + port + endPoint + id;
        }

        /// <summary>
        /// Executes any webRequest with certain parameters
        /// </summary>
        /// <param name="method"></param>
        /// <param name="endPoint"></param>
        /// <param name="body"></param>
        /// <param name="headers"></param>
        /// <returns>IRestResponse</returns>
        private IRestResponse ExecuteWebRequest(Method method, string endPoint, [Optional] string body, [Optional] Dictionary<string, string> headers, [Optional] Dictionary<string, object> additionalParams)
        {
            //LogUtil.WriteDebug($"Creation of WebRequest for endpoint: {endPoint}");
            var client = new RestClient(endPoint);

            //LogUtil.WriteDebug($"RequestMethod is {method.ToString()}");
            var request = new RestRequest { Method = method };

            if (method != Method.GET && method != Method.DELETE)
            {
                request.AddParameter("application/json", body, ParameterType.RequestBody);
            }
            AddSessionIdToHeaders(request);
            AddHeaders(request, headers);
            AddParameters(request, additionalParams);
            DateTime timeNow = DateTime.UtcNow;
            var response = client.Execute(request);
            ResponseTime = DateTime.UtcNow - timeNow;
            //LogUtil.log.Info($"Response time is {ResponseTime.ToString(@"ss\.fff")} s");
            //LogUtil.WriteDebug($"Response status is {response.StatusCode}" + Environment.NewLine);
            //if (!response.IsSuccessful)
                //LogUtil.log.Error(response.Content);
            return response;
        }

        /// <summary>
        /// Method is used for uploading files
        /// </summary>
        /// <param name="method">Put or Post verbs can be used for uploading file</param>
        /// <param name="endPoint">endPoint to communicate with necessary service</param>
        /// <param name="filePath">path to file to be uploaded</param>
        /// <param name="headers">if specific headers are required for sending the request- this headers should be used</param>
        /// <param name="additionalParams">if specific parameters are required for sending the request- this parameters should be used</param>
        /// <returns></returns>
        public IRestResponse UploadFileWebRequest(Method method, string endPoint, string filePath,
            [Optional] Dictionary<string, string> headers, [Optional] Dictionary<string, object> additionalParams)
        {
            //LogUtil.WriteDebug($"Creation of UploadFileWebRequest for endpoint: {endPoint}");
            var client = new RestClient(endPoint);
            //LogUtil.WriteDebug($"RequestMethod is {method.ToString()}");
            var request = new RestRequest { Method = method, AlwaysMultipartFormData = true };
            AddSessionIdToHeaders(request);
            AddHeaders(request, headers);
            request.AddParameter("file_name", Path.GetFileName(filePath));
            request.AddParameter("file_format", Path.GetExtension(filePath));
            request.AddFile("file", File.ReadAllBytes(filePath), Path.GetFileName(filePath), "application/octet-stream");
            AddParameters(request, additionalParams);
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //LogUtil.WriteDebug(response.StatusCode + ": " + response.Content);
            }
            return response;
        }

        private void AddParameters(RestRequest request, Dictionary<string, object> additionalParams)
        {
            if (additionalParams != null)
            {
                foreach (var parameter in additionalParams)
                {
                    if (parameter.Key != "filter")
                    {
                        request.AddParameter(parameter.Key, parameter.Value);
                    }
                    else
                    {
                        request.AddParameter(parameter.Key, parameter.Value, ParameterType.QueryStringWithoutEncode);
                    }

                    //LogUtil.WriteDebug($"Request Parameters: {parameter.Key} {parameter.Value}");
                }
            }
        }

        private void AddHeaders(RestRequest request, Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                    //LogUtil.WriteDebug($"Request Headers: {header.Key} {header.Value}");
                }
            }
        }

        private void AddSessionIdToHeaders(RestRequest request)
        {
            if (!string.IsNullOrEmpty(sessionId))
            {
                request.AddHeader("Authorization", $"SessionID {sessionId}");
            }
        }

        private IRestResponse RetrySendingRequestIfUnauthorized(IRestResponse response, Method method, string endPoint, [Optional] Dictionary<string, string> headers, [Optional] string body, [Optional] Dictionary<string, object> additionalParams)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //GetSessionId();
                return ExecuteWebRequest(method, endPoint, headers: headers, body: body, additionalParams: additionalParams);
            }

            return response;
        }

        public IRestResponse SendGetRequest(string endPoint, [Optional] Dictionary<string, string> headers, [Optional] Dictionary<string, object> parameters)
        {
            var method = Method.GET;
            IRestResponse response = ExecuteWebRequest(method, endPoint, headers: headers, additionalParams: parameters);
            response = RetrySendingRequestIfUnauthorized(response, method, endPoint, headers, additionalParams: parameters);
            return response;
        }

        public T SendGetRequest<T>(string endPoint, [Optional] Dictionary<string, string> headers, [Optional] Dictionary<string, object> parameters)
        {
            var method = Method.GET;
            IRestResponse response = ExecuteWebRequest(method, endPoint, headers: headers, additionalParams: parameters);
            response = RetrySendingRequestIfUnauthorized(response, method, endPoint, headers, additionalParams: parameters);
            return new JsonDeserializer().Deserialize<T>(response);
        }

        public IRestResponse SendPostRequest(string endPoint, [Optional] Dictionary<string, string> headers, string body)
        {
            var method = Method.POST;
            IRestResponse response = ExecuteWebRequest(method, endPoint, body, headers);
            response = RetrySendingRequestIfUnauthorized(response, method, endPoint, headers, body);
            return response;
        }

        public T SendPostRequest<T>(string endPoint, [Optional] Dictionary<string, string> headers, string body)
        {
            var method = Method.POST;
            IRestResponse response = ExecuteWebRequest(method, endPoint, body, headers);
            response = RetrySendingRequestIfUnauthorized(response, method, endPoint, headers, body);
            return new JsonDeserializer().Deserialize<T>(response);
        }

        public IRestResponse SendPutRequest(string endPoint, [Optional] Dictionary<string, string> headers, string body)
        {
            var method = Method.PUT;
            IRestResponse response = ExecuteWebRequest(method, endPoint, body, headers);
            response = RetrySendingRequestIfUnauthorized(response, method, endPoint, headers, body);
            return response;
        }

        public T SendPutRequest<T>(string endPoint, [Optional] Dictionary<string, string> headers, string body)
        {
            var method = Method.PUT;
            IRestResponse response = ExecuteWebRequest(method, endPoint, body, headers);
            response = RetrySendingRequestIfUnauthorized(response, method, endPoint, headers, body);
            return new JsonDeserializer().Deserialize<T>(response);
        }

        public IRestResponse SendDeleteRequest(string endPoint, [Optional] Dictionary<string, string> headers)
        {
            var method = Method.DELETE;
            IRestResponse response = ExecuteWebRequest(method, endPoint, headers: headers);
            response = RetrySendingRequestIfUnauthorized(response, method, endPoint, headers);
            return response;
        }

        public IRestResponse SendPatchRequest(string endPoint, [Optional] Dictionary<string, string> headers, string body)
        {
            var method = Method.PATCH;
            IRestResponse response = ExecuteWebRequest(method, endPoint, body, headers);
            response = RetrySendingRequestIfUnauthorized(response, method, endPoint, headers, body);
            return response;
        }
    }
}
