

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace InstamojoAPI
{
    public class InstamojoImplementation : Instamojo
    {
         
        private volatile static Instamojo uniqueInstance; // for singleton design pattern
        static readonly object _locker = new object(); // for multithreading purpose

        /** The client id. */
        private String clientId;

        /** The client secret. */
        private String clientSecret;

        /** The api endpoint. */
        private String apiEndpoint;

        /** The auth endpoint. */
        private String authEndpoint;

        /** The access token. */
        private static AccessTokenResponse accessToken;

        private static long tokenCreationTime;

        private InstamojoImplementation() { }

        private InstamojoImplementation(String clientId, String clientSecret, String apiEndpoint, String authEndpoint)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.apiEndpoint = apiEndpoint;
            this.authEndpoint = authEndpoint;
        }

        /**
     * Gets api.
     *
     * @param clientId     the client id
     * @param clientSecret the client secret
     * @return the api
     * @throws IOException the io exception
     */
        public static async Task<Instamojo> getApi(String clientId, String clientSecret, String apiEndpoint, String authEndpoint)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                throw new BaseException("Please enter ClientId");
            }
            if (string.IsNullOrEmpty(clientSecret))
            {
                throw new BaseException("Please enter clientSecret");
            }
            if (string.IsNullOrEmpty(apiEndpoint))
            {
                throw new BaseException("Please enter apiEndpoint");
            }
            if (string.IsNullOrEmpty(authEndpoint))
            {
                throw new BaseException("Please enter authEndpoint");

            }
            return await getInstamojoAsync(clientId, clientSecret, apiEndpoint, authEndpoint);
        }

        public static async Task<Instamojo> getApiAsync(String clientId, String clientSecret)
        {
            return await getApi(clientId, clientSecret, InstamojoConstants.INSTAMOJO_API_ENDPOINT, InstamojoConstants.INSTAMOJO_AUTH_ENDPOINT);
        }

        private static async Task<Instamojo> getInstamojoAsync(String clientId, String clientSecret, String apiEndpoint, String authEndpoint)
        {
            if (uniqueInstance == null)
            {
               
                
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new InstamojoImplementation(clientId, clientSecret, apiEndpoint, authEndpoint);
                        accessToken = await getAccessTokenAsync(clientId, clientSecret, authEndpoint);
                    }
                
            }
            else
            {
                if (isTokenExpired())
                {
                    
                    
                        if (isTokenExpired())
                        {
                            accessToken = await getAccessTokenAsync(clientId, clientSecret, authEndpoint);
                        }
                    
                }
            }
            return uniqueInstance;
        }

        private static bool isTokenExpired()
        {
            long durationInSeconds = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - tokenCreationTime;
            if (durationInSeconds >= accessToken.expires_in)
            {
                return true;
            }

            return false;
        }

        private static async Task<AccessTokenResponse> getAccessTokenAsync(string clientId, string clientSecret, string authEndpoint)
        {
            AccessTokenResponse objPaymentRequestDetailsResponse;
            try
            {
                using (var client =  new HttpClient())
                {

                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    ServicePointManager.DefaultConnectionLimit = 9999;

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(authEndpoint),
                        Headers ={
                            { "accept", "application/json" }
                                                            },
                        Content = new FormUrlEncodedContent(new Dictionary<string, string>
                                {
                                      { "grant_type",InstamojoConstants.GRANT_TYPE },
                                      {"client_id",clientId },
                                      {"client_secret",clientSecret }
                                }),
                    };

                    //var values = new NameValueCollection();
                    //values["client_id"] = clientId;
                    //values["client_secret"] = clientSecret;
                    //values["grant_type"] = InstamojoConstants.GRANT_TYPE;

                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                        objPaymentRequestDetailsResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(body);
                        Console.WriteLine(body);
                    }
                  
                    //var responseString = Encoding.Default.GetString(response);
                 
                  



                    if (string.IsNullOrEmpty(objPaymentRequestDetailsResponse.access_token))
                    {
                        throw new BaseException("Could not get the access token due to " + objPaymentRequestDetailsResponse.error);
                    }
                }
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message);
            }
            catch (WebException ex)
            {
                objPaymentRequestDetailsResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(ex.Message);
            }

            tokenCreationTime = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
            return objPaymentRequestDetailsResponse;
        }

        /**
        * @param string Path
        * @return string adds the Path to endpoint with.
        */
        private string build_api_call_url(string Path)
        {
            if (Path.LastIndexOf("/?") == -1 && Path.LastIndexOf("?") == -1 && Path.LastIndexOf("/") == -1)
            {
                apiEndpoint = apiEndpoint + Path + "/";
                return apiEndpoint;
            }
            return apiEndpoint + Path;
        }

        /**
         * @param method: string httpWebRequest Method
         * @param path: string path for building API URL
         * @param PostData: object 
         * @return JSON string
        */
        private string api_call(string method, string path, object PostData = null)
        {
            string URL = build_api_call_url(path);

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            myHttpWebRequest.Accept = "application/json";
            myHttpWebRequest.ContentType = "application/json";

            // Build Headers
            myHttpWebRequest.Headers["Authorization"] = accessToken.token_type + " " + accessToken.access_token;

            myHttpWebRequest.Method = method;

            if (method == "POST")
            {
                //JavaScriptSerializer ser = new JavaScriptSerializer();
                //ser.RegisterConverters(new JavaScriptConverter[] { new JavaScriptConverters.NullPropertiesConverter() });


                string strJSONData = JsonConvert.SerializeObject(PostData);


                //ser.Serialize(PostData);
                using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(strJSONData);
                    streamWriter.Flush();
                }
            }
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(myHttpWebResponse.GetResponseStream()))
            {
                var streamRead = streamReader.ReadToEnd().Trim();
                return streamRead;
            }
        }

        //private static T JsonDeserialize<T>(string jsonString)
        //{
        //    return new JavaScriptSerializer().Deserialize<T>(jsonString);
        //}

        private static string showErrorMessage(string errorMessage)
        {
            return errorMessage;
        }

        /********************************   Request a Payment Order  *******************************************/

        #region Creating a payment Order
        /**
        * @param objPaymentRequest: PaymentOrder object .
        * @return objPaymentCreateResponse: CreatePaymentOrderResponse object.
        */
        public CreatePaymentOrderResponse createNewPaymentRequest(PaymentOrder objPaymentRequest)
        {
            if (objPaymentRequest == null)
            {
                throw new ArgumentNullException(typeof(PaymentOrder).Name, "PaymentOrder Object Can not be Null ");
            }

            bool isInValid = objPaymentRequest.validate();
            if (isInValid)
            {
                throw new InvalidPaymentOrderException();
            }

            try
            {
                string stream = api_call("POST", "gateway/orders/", objPaymentRequest);
                CreatePaymentOrderResponse objPaymentCreateResponse = JsonConvert.DeserializeObject<CreatePaymentOrderResponse>(stream);

                return objPaymentCreateResponse;
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (BaseException ex)
            {
                throw new BaseException(ex.Message, ex.InnerException);
            }
            catch (UriFormatException ex)
            {
                throw new UriFormatException(ex.Message, ex.InnerException);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse err = ex.Response as HttpWebResponse;
                    if (err != null)
                    {
                        string htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                        throw new InvalidPaymentOrderException(htmlResponse);
                    }
                }
                throw new WebException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        #endregion

        # region  Get All your Payment Orders List
        /**
        * @param strPaymentRequestId: string id as provided by paymentRequestCreate, paymentRequestsList, webhook or redirect.
        * @return objPaymentRequestStatus: PaymentCreateResponse object.
        */
        public PaymentOrderListResponse getPaymentOrderList(PaymentOrderListRequest objPaymentOrderListRequest)
        {
            if (objPaymentOrderListRequest == null)
            {
                throw new ArgumentNullException(typeof(PaymentOrderListRequest).Name, "PaymentOrderListRequest Object Can not be Null");
            }
            string queryString = "", stream = "";

            if (string.IsNullOrEmpty(objPaymentOrderListRequest.id))
            {
                queryString = "id=" + objPaymentOrderListRequest.id;
            }
            if (string.IsNullOrEmpty(objPaymentOrderListRequest.transaction_id))
            {
                string transQuery = "transaction_id=" + objPaymentOrderListRequest.transaction_id;
                queryString += string.IsNullOrEmpty(queryString) ? transQuery : ("&" + transQuery);
            }
            if (objPaymentOrderListRequest.page != 0)
            {
                string pageQuery = "page=" + objPaymentOrderListRequest.page;
                queryString += string.IsNullOrEmpty(queryString) ? pageQuery : ("&" + pageQuery);
            }
            if (objPaymentOrderListRequest.limit != 0)
            {
                string limitQuery = "limit=" + objPaymentOrderListRequest.limit;
                queryString += string.IsNullOrEmpty(queryString) ? limitQuery : ("&" + limitQuery);
            }
            try
            {
                if (queryString != "")
                {
                    stream = api_call("GET", "gateway/orders/?" + queryString, objPaymentOrderListRequest);
                }
                else
                {
                    stream = api_call("GET", "gateway/orders/", null);
                }

                PaymentOrderListResponse objPaymentRequestStatusResponse = JsonConvert.DeserializeObject<PaymentOrderListResponse>(stream);
                return objPaymentRequestStatusResponse;
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (BaseException ex)
            {
                throw new BaseException(ex.Message, ex.InnerException);
            }
            catch (UriFormatException ex)
            {
                throw new UriFormatException(ex.Message, ex.InnerException);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse err = ex.Response as HttpWebResponse;
                    if (err != null)
                    {
                        string htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                        throw new WebException(err.StatusDescription + " " + htmlResponse);
                    }
                }
                throw new WebException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        #endregion

        # region  Get details of this payment order by Order Id
        public PaymentOrderDetailsResponse getPaymentOrderDetails(string strOrderId)
        {
            if (string.IsNullOrEmpty(strOrderId))
            {
                throw new ArgumentNullException(typeof(string).Name, "Order Id Can not be Null or Empty");
            }
            try
            {
                string stream = api_call("GET", "gateway/orders/id:" + strOrderId + "/", null);
                PaymentOrderDetailsResponse objPaymentRequestDetailsResponse = JsonConvert.DeserializeObject<PaymentOrderDetailsResponse>(stream);
                return objPaymentRequestDetailsResponse;
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (BaseException ex)
            {
                throw new BaseException(ex.Message, ex.InnerException);
            }
            catch (UriFormatException ex)
            {
                throw new UriFormatException(ex.Message, ex.InnerException);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse err = ex.Response as HttpWebResponse;
                    if (err != null)
                    {
                        string htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                        throw new WebException(err.StatusDescription + " " + htmlResponse);
                    }
                }
                throw new WebException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        #endregion

        # region  Get details of this payment order by Transaction Id
        public PaymentOrderDetailsResponse getPaymentOrderDetailsByTransactionId(string strTransactionId)
        {
            if (string.IsNullOrEmpty(strTransactionId))
            {
                throw new ArgumentNullException(typeof(string).Name, "Transaction Id Can not be Null or Empty");
            }
            try
            {
                string stream = api_call("GET", "gateway/orders/transaction_id:" + strTransactionId + "/", null);
                PaymentOrderDetailsResponse objPaymentRequestDetailsResponse = JsonConvert.DeserializeObject<PaymentOrderDetailsResponse>(stream);
                return objPaymentRequestDetailsResponse;
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (BaseException ex)
            {
                throw new BaseException(ex.Message, ex.InnerException);
            }
            catch (UriFormatException ex)
            {
                throw new UriFormatException(ex.Message, ex.InnerException);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse err = ex.Response as HttpWebResponse;
                    if (err != null)
                    {
                        string htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                        throw new WebException(err.StatusDescription + " " + htmlResponse);
                    }
                }
                throw new WebException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region Creating a Refund
        /**
        * @param objPaymentRequest: PaymentOrder object .
        * @return objPaymentCreateResponse: CreatePaymentOrderResponse object.
        */
        public CreateRefundResponce createNewRefundRequest(Refund objCreateRefund)
        {
            if (objCreateRefund == null)
            {
                throw new ArgumentException("Refund object can not be null.");
            }
            if (objCreateRefund.payment_id == null)
            {
                throw new ArgumentNullException(typeof(Refund).Name, "PaymentId cannot be null ");
            }
            bool isInValid = objCreateRefund.validate();
            if (isInValid)
            {
                throw new InvalidPaymentOrderException();
            }
            try
            {
                string stream = api_call("POST", "payments/" + objCreateRefund.payment_id + "/refund/", objCreateRefund);
                CreateRefundResponce objRefundResponse = JsonConvert.DeserializeObject<CreateRefundResponce>(stream);
                return objRefundResponse;
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
            catch (BaseException ex)
            {
                throw new BaseException(ex.Message, ex.InnerException);
            }
            catch (UriFormatException ex)
            {
                throw new UriFormatException(ex.Message, ex.InnerException);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse err = ex.Response as HttpWebResponse;
                    if (err != null)
                    {
                        string htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                        throw new WebException(err.StatusDescription + " " + htmlResponse);
                    }
                }
                throw new WebException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        #endregion

    }
}
