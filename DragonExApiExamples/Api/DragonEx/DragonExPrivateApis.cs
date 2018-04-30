using DragonExApiExamples.Api.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace DragonExApiExamples.Api.DragonEx
{
    class DragonExPrivateApis
    {
        /// <summary>
        /// Token
        /// </summary>
        private string token;

        /// <summary>
        /// Token expire Time
        /// </summary>
        private long tokenExpireTime = 0;

        /// <summary>
        /// Lock Obj
        /// </summary>
        private static object lockObj = new object();

        /// <summary>
        /// Instance
        /// </summary>
        private static DragonExPrivateApis instance = null;

        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static DragonExPrivateApis Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new DragonExPrivateApis();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Private Constructor
        /// </summary>
        private DragonExPrivateApis()
        {
        }

        /// <summary>
        /// 2.获取token
        /// 请求方式：GET
        /// url：https://{host}/api/v1/token/new/
        /// 传入值：无
        /// 返回值data信息：
        ///     字段名	数据类型	说明
        ///     token	string	token，后续需在headers中附带此字段
        ///     expire_time	int	此token过期时间，为Unix时间戳
        /// </summary>
        /// <returns></returns>
        private void SetToken()
        {
            string apiRelativePath = "/api/v1/token/new/";

            string result = ApiPostQuery(apiRelativePath, null, true);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data").Element("data");

            token = data.Element("token").Value;
            tokenExpireTime = long.Parse(data.Element("expire_time").Value);
        }

        /// <summary>
        /// 3.获取token状态
        /// 请求方式：GET
        /// url：https://{host}/api/v1/token/status/
        /// 传入值：无
        /// 返回值data信息：
        ///     字段名	数据类型	说明
        ///     uid	int	用户ID
        /// </summary>
        /// <returns></returns>
        public XElement GetTokenStatus()
        {
            string apiRelativePath = "/api/v1/token/status/";

            string result = ApiPostQuery(apiRelativePath, null);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");

            return data;
        }

        /// <summary>
        /// 5.获取用户拥有的货币信息(只返回用户有的币种)
        /// 请求方式：POST
        /// url：https://{host}/api/v1/user/own/
        /// 传入值：无
        /// 返回值data信息：
        /// 	字段名	数据类型	说明
        /// 	coin_id	int	货币ID
        /// 	code	string	货币code
        /// 	volume	string	用户拥有该币种的总数
        /// 	frozen	string	该币种被冻结的数目
        /// </summary>
        /// <returns></returns>
        public XElement GetUserOwn()
        {
            string apiRelativePath = "/api/v1/user/own/";

            string result = ApiPostQuery(apiRelativePath, null);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");

            return data;
        }

        /// <summary>
        /// 11.下买单
        /// 请求方式：POST
        /// url：https://{host}/api/v1/order/buy/
        /// 传入值：
        /// 	字段名	数据类型	说明
        /// 	symbol_id	int	交易对ID
        /// 	price	string	价格
        /// 	volume	string	数量
        /// 返回值data信息：
        /// 	字段名	数据类型	说明
        /// 	order_id	int	订单ID
        /// 	price	string	下单价格
        /// 	status	int	订单状态
        /// 	timestamp	int	Unix时间戳
        /// 	trade_volume	string	成交数量
        /// 	volume	string	下单数量
        /// </summary>
        /// <returns></returns>
        public XElement BuyOrder(int symbolId, string price, string volume)
        {
            string apiRelativePath = "/api/v1/order/buy/";

            // Set parameters
            IDictionary<string, string> req = new Dictionary<string, string>();
            req.Add("symbol_id", symbolId.ToString());
            req.Add("price", price);
            req.Add("volume", volume);

            string result = ApiPostQuery(apiRelativePath, req);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");

            return data;
        }

        /// <summary>
        /// 12.下卖单
        /// 请求方式：POST
        /// url：https://{host}/api/v1/order/sell/
        /// 传入值：
        /// 	字段名	数据类型	说明
        /// 	symbol_id	int	交易对ID
        /// 	price	string	价格
        /// 	volume	string	数量
        /// 返回值data信息：
        /// 	字段名	数据类型	说明
        /// 	order_id	int	订单ID
        /// 	price	string	下单价格
        /// 	status	int	订单状态
        /// 	timestamp	int	Unix时间戳
        /// 	trade_volume	string	成交数量
        /// 	volume	string	下单数量
        /// </summary>
        /// <returns></returns>
        public XElement SellOrder(int symbolId, string price, string volume)
        {
            string apiRelativePath = "/api/v1/order/sell/";

            // Set parameters
            IDictionary<string, string> req = new Dictionary<string, string>();
            req.Add("symbol_id", symbolId.ToString());
            req.Add("price", price);
            req.Add("volume", volume);

            string result = ApiPostQuery(apiRelativePath, req);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");

            return data;
        }

        /// <summary>
        /// 13.取消订单
        /// 请求方式：POST
        /// url：https://{host}/api/v1/order/cancel/
        /// 传入值：
        /// 	字段名	数据类型	说明
        /// 	symbol_id	int	交易对ID
        /// 	order_id	string	订单ID
        /// 返回值data信息：
        /// 	字段名	数据类型	说明
        /// 	order_id	int	订单ID
        /// 	price	string	下单价格
        /// 	status	int	订单状态
        /// 	timestamp	int	Unix时间戳
        /// 	trade_volume	string	成交数量
        /// 	volume	string	下单数量
        /// </summary>
        /// <returns></returns>
        public XElement CancelOrder(int symbolId, string orderId)
        {
            string apiRelativePath = "/api/v1/order/cancel/";

            // Set parameters
            IDictionary<string, string> req = new Dictionary<string, string>();
            req.Add("symbol_id", symbolId.ToString());
            req.Add("order_id", orderId.ToString());

            string result = ApiPostQuery(apiRelativePath, req);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");

            return data;
        }

        /// <summary>
        /// 14.获取用户委托记录详情
        /// 请求方式：POST
        /// url：https://{host}/api/v1/order/detail/
        /// 传入值：
        /// 	字段名	数据类型	说明
        /// 	symbol_id	int	交易对ID
        /// 	order_id	string	订单ID
        /// 返回值data信息：
        /// 	字段名	数据类型	说明
        /// 	order_id	int	订单ID
        /// 	order_type	int	订单类型：1-买单，2-卖单
        /// 	price	string	下单价格
        /// 	status	int	订单状态
        /// 	symbol_id	int	交易对ID
        /// 	timestamp	int	Unix时间戳
        /// 	trade_volume	string	成交数量
        /// 	volume	string	下单数量
        /// </summary>
        /// <returns></returns>
        public XElement GetOrderDetail(int symbolId, string orderId)
        {
            string apiRelativePath = "/api/v1/order/detail/";

            // Set parameters
            IDictionary<string, string> req = new Dictionary<string, string>();
            req.Add("symbol_id", symbolId.ToString());
            req.Add("order_id", orderId.ToString());

            string result = ApiPostQuery(apiRelativePath, req);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");

            return data;
        }

        /// <summary>
        /// 15.获取用户委托记录
        /// 请求方式：POST
        /// url：https://{host}/api/v1/order/history/
        /// 传入值：
        /// 	字段名	数据类型	说明
        /// 	symbol_id	int	交易对ID
        /// 	direction	int	搜索方向：1-从起始时间往后查，2-从起始时间往前查，默认2
        /// 	start	int	起始时间：从当前时间开始时可不传或传0，否则传unix时间戳(纳秒)
        /// 	count	int	数量， 默认10
        /// 	status	int	订单状态：0-所有状态，1-未成交，2-已成交，3-已取消，4-失败
        /// 返回值data信息：
        /// 	字段名	数据类型	说明
        /// 	list	{}	订单列表
        /// 	count	int	总条数
        /// 	list信息:
        /// 		字段名	数据类型	说明
        /// 		order_id	int	订单ID
        /// 		order_type	int	订单类型：1-买单，2-卖单
        /// 		price	string	下单价格
        /// 		status	int	订单状态
        /// 		symbol_id	int	交易对ID
        /// 		timestamp	int	时间戳
        /// 		trade_volume	string	已成交数量
        /// 		volume	string	下单数量
        /// </summary>
        /// <returns></returns>
        public XElement GetOrderHistory(int symbolId, int direction = 2, int start = 0, 
            int count = 10, int status = 0)
        {
            string apiRelativePath = "/api/v1/order/history/";

            // Set parameters
            IDictionary<string, string> req = new Dictionary<string, string>();
            req.Add("symbol_id", symbolId.ToString());
            req.Add("direction", direction.ToString());
            req.Add("start", start.ToString());
            req.Add("count", count.ToString());
            req.Add("status", status.ToString());

            string result = ApiPostQuery(apiRelativePath, req);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");

            return data;
        }

        /// <summary>
        /// 16.获取用户成交记录
        /// 请求方式：POST
        /// url：https://{host}/api/v1/deal/history/
        /// 传入值：
        /// 	字段名	数据类型	说明
        /// 	symbol_id	int	交易对ID
        /// 	direction	int	搜索方向：1-从起始时间往后查，2-从起始时间往前查，默认2
        /// 	start	int	起始时间：从当前时间开始时可不传或传0，否则传unix时间戳(纳秒)
        /// 	count	int	数量， 默认10
        /// 返回值data信息：
        /// 	字段名	数据类型	说明
        /// 	list	{}	订单列表
        /// 	list信息:
        /// 		字段名	数据类型	说明
        /// 		charge	string	此次交易手续费
        /// 		deal_type	int	成交类型: 1-买，2-卖
        /// 		order_id	int	订单ID
        /// 		order_type	int	订单类型：1-买单，2-卖单
        /// 		price	string	下单价格
        /// 		symbol_id	int	交易对ID
        /// 		timestamp	int	时间戳
        /// 		trade_id	int	成交ID
        /// 		volume	string	成交数量
        /// </summary>
        /// <returns></returns>
        public XElement GetDealHistory(int symbolId, int direction = 2, int start = 0, int count = 10)
        {
            string apiRelativePath = "/api/v1/deal/history/";

            // Set parameters
            IDictionary<string, string> req = new Dictionary<string, string>();
            req.Add("symbol_id", symbolId.ToString());
            req.Add("direction", direction.ToString());
            req.Add("start", start.ToString());
            req.Add("count", count.ToString());

            string result = ApiPostQuery(apiRelativePath, req);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");

            return data;
        }

        /// <summary>
        /// Query API Use Http Post
        /// </summary>
        /// <param name="apiRelativePath">API Relative Path</param>
        /// <param name="req">Request parameters</param>
        /// <returns>Api Result</returns>
        private string ApiPostQuery(string apiRelativePath, IDictionary<string, string> req, bool isGettingToken = false)
        {
            // If not getting token or token expired, get token again
            if (!isGettingToken && (String.IsNullOrEmpty(token) || tokenExpireTime < GetTimestamp()))
            {
                SetToken();
            }

            var apiPath = string.Format(DragonExConstants.API_URL_FORMAT, 
                DragonExConstants.API_BASE_URL, apiRelativePath);

            var wb = (HttpWebRequest)WebRequest.Create(apiPath);

            string method = "POST";
            Dictionary<string, object> headerDict = new Dictionary<string, object>();
            headerDict.Add("Content-Type", "application/json");
            DateTime dt = DateTime.Now;
            headerDict.Add("date", GetGMTDateStr(dt));
            string jsonData = "";
            if (req != null && req.Count > 0)
            {
                jsonData = JsonConvert.SerializeObject(req);
                string encryptSha1 = EncryptUtil.EncryptSHA1(jsonData);
                //Error will return from server when add Content-Sha1, so I comment out it.
                //wb.Headers.Add("Content-Sha1", encryptSha1);
                //headerDict.Add("Content-Sha1", encryptSha1);
            }

            wb.Method = method;
            wb.ContentType = headerDict["Content-Type"].ToString();
            wb.KeepAlive = true;
            wb.Headers.Add("Charset", "UTF-8");

            wb.Date = dt;

            if (!isGettingToken)
            {
                wb.Headers.Add("token", token);
            }
            
            var sign = GetSign(DragonExConstants.SECRET_KEY, method, apiRelativePath, headerDict);
            string auth = String.Format("{0}:{1}", DragonExConstants.ACCESS_KEY, sign);
            wb.Headers.Add("auth", auth);

            if (req != null && req.Count > 0)
            {
                var reqBuffer = Encoding.UTF8.GetBytes(jsonData);
                wb.ContentLength = reqBuffer.Length;
                using (var wbReq = wb.GetRequestStream())
                {
                    wbReq.Write(reqBuffer, 0, reqBuffer.Length);
                    wbReq.Flush();
                    wbReq.Close();
                    using (var res = wb.GetResponse())
                    {
                        using (var rs = res.GetResponseStream())
                        {
                            using (StreamReader sr = new StreamReader(rs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            else
            {
                using (var res = wb.GetResponse())
                {
                    using (var rs = res.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(rs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get sign string
        /// </summary>
        /// <param name="secret_key"></param>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <param name="headers"></param>
        /// <returns>Signed string</returns>
        private static string GetSign(String secret_key, String method, String path, Dictionary<string, object> headers)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(method).Append("\n");

            if (headers.ContainsKey("Content-Sha1"))
            {
                sb.Append(headers["Content-Sha1"]);
            }

            sb.Append("\n")
            .Append(headers["Content-Type"]).Append("\n")
            .Append(headers["date"]).Append("\n");

            sb.Append(path);

            String hamcsha1 = SignWithHmacSha1(secret_key, sb.ToString());
            return hamcsha1;

        }

        /// <summary>
        /// Sign with Hmac sha1
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="message">Message data</param>
        /// <returns>Signed string</returns>
        private static string SignWithHmacSha1(string key, string message)
        {
            using (HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(key)))
            {
                byte[] b = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return Convert.ToBase64String(b);
            }
        }

        /// <summary>
        /// Convert byte to string
        /// </summary>
        /// <param name="buff">Byte buffers</param>
        /// <returns>String</returns>
        private static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary).ToLowerInvariant();
        }

        /// <summary>
        /// Get GMT string
        /// </summary>
        /// <returns>GMT String</returns>
        private static string GetGMTDateStr(DateTime dt)
        {
            return dt.ToUniversalTime().ToString("r");
        }

        /// <summary>
        /// Get current timestamp
        /// </summary>
        /// <returns>Current timestamp</returns>
        public static long GetTimestamp()
        {
            var d = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return (long)d / 1000;
        }

        /// <summary>
        /// Get query string
        /// </summary>
        /// <param name="dic">Parameter Dictionary</param>
        /// <returns>Query string</returns>
        private static string ToQueryString(IDictionary<string, string> dic)
        {
            var array = (from key in dic.Keys
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(dic[key])))
                .ToArray();
            return string.Join("&", array);
        }
    }
}
