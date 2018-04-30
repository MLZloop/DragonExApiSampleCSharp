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
    class DragonExPublicApis
    {
        /// <summary>
        /// Lock Obj
        /// </summary>
        private static object lockObj = new object();

        private static DragonExPublicApis instance = null;

        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static DragonExPublicApis Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new DragonExPublicApis();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Private Constructor
        /// </summary>
        private DragonExPublicApis()
        {
        }

        /// <summary>
        /// 4.获取所有货币
        /// 请求方式：GET
        /// url：https://{host}/api/v1/coin/all/
        /// 传入值：无
        /// 返回值data信息：
        ///     字段名	数据类型	说明
        ///     coin_id	int	        货币ID
        ///     code	string	    货币code
        /// </summary>
        /// <returns>Result object</returns>
        public XElement GetAllCoins()
        {
            string apiRelativePath = "/api/v1/coin/all/";
            string result = ApiQuery(apiRelativePath, null);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");
            return data;
        }

        /// <summary>
        /// 6.获取所有交易对
        /// 请求方式：GET
        /// url：https://{host}/api/v1/symbol/all/
        /// 传入值：无
        /// 返回值data信息：
        ///     字段名	    数据类型	说明
        ///     symbol_id   int         交易对ID
        ///     symbol      string      交易对
        /// </summary>
        /// <returns>Result object</returns>
        public XElement GetAllSymbols()
        {
            string apiRelativePath = "/api/v1/symbol/all/";
            string result = ApiQuery(apiRelativePath, null);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");
            return data;
        }

        /// <summary>
        /// 7.获取k线
        /// 请求方式：GET
        /// url：https://{host}/api/v1/market/kline/
        /// 传入值：
        ///     字段名	数据类型	说明
        ///     symbol_id int 交易对ID
        ///     st int 起始时间，从当前时间开始时可不传或传0，否则传unix时间戳(纳秒)
        ///     direction int 查询方向：1-从起始时间往后查，2-从起始时间往前查，默认2
        ///     count   int 查询条数，最大100，默认10
        ///     kline_type  int k线类型：1-1min线， 2-5min线， 3-15min线， 4-30min线， 5-60min线， 6-1day线.默认1min
        /// 返回值data信息：
        ///     字段名	数据类型	说明
        ///     columns[] 下述列表每个位置的数据代表的意义
        ///     list[] kline数据
        ///     list信息：
        ///         字段名	数据类型	说明
        ///         amount string 交易额
        ///         close_price string 收盘价
        ///         max_price string 最高价
        ///         min_price string 最低价
        ///         open_price string 开盘价
        ///         pre_close_price string 上一个收盘价
        ///         timestamp int 秒级时间戳
        ///         usdt_amount string 对应的USDT交易额
        ///         volume string 交易量
        /// </summary>
        /// <returns></returns>
        public XElement GetMarketKLine(int symbolId, int startTime=0, 
            int direction=2, int count=10, int kLineType=1)
        {
            string apiRelativePath = "/api/v1/market/kline/";

            // Set parameters
            IDictionary<string, object> req = new Dictionary<string, object>();
            req.Add("symbol_id", symbolId);
            req.Add("st", startTime);
            req.Add("direction", direction);
            req.Add("count", count);
            req.Add("kline_type", kLineType);

            string result = ApiQuery(apiRelativePath, req);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");
            return data;
        }

        /// <summary>
        /// 8.获取买盘
        /// 请求方式：GET
        /// url：https://{host}/api/v1/market/buy/
        /// 传入值：
        ///     字段名	数据类型	说明
        ///     symbol_id int 交易对ID
        /// 返回值data信息：
        ///     字段名	数据类型	说明
        ///     price string 价格
        ///     volume string 数量
        /// </summary>
        /// <returns></returns>
        public XElement GetMarketBuy(int symbolId)
        {
            string apiRelativePath = "/api/v1/market/buy/";

            // Set parameters
            IDictionary<string, object> req = new Dictionary<string, object>();
            req.Add("symbol_id", symbolId);

            string result = ApiQuery(apiRelativePath, req);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");
            return data;
        }

        /// <summary>
        /// 9.获取卖盘
        /// 请求方式：GET
        /// url：https://{host}/api/v1/market/sell/
        /// 传入值：
        ///     字段名	数据类型	说明
        ///     symbol_id int 交易对ID
        /// 返回值data信息：
        ///     字段名	数据类型	说明
        ///     price string 价格
        ///     volume string 数量
        /// </summary>
        /// <returns></returns>
        public XElement GetMarketSell(int symbolId)
        {
            string apiRelativePath = "/api/v1/market/sell/";

            // Set parameters
            IDictionary<string, object> req = new Dictionary<string, object>();
            req.Add("symbol_id", symbolId);

            string result = ApiQuery(apiRelativePath, req);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");
            return data;
        }

        /// <summary>
        /// 10.获取实时行情
        /// 请求方式：GET
        /// url：https://{host}/api/v1/market/real/
        /// 传入值：
        ///     字段名	数据类型	说明
        ///     symbol_id int 交易对ID
        /// 返回值data信息：
        /// 	字段名	数据类型	说明
        /// 	close_price	string	收盘价
        /// 	current_volume	string	至今交易量
        /// 	max_price	string	最高价
        /// 	min_price	string	最低价
        /// 	open_price	string	开盘价
        /// 	price_base	string	本交易区基础货币对USDT的价格(如eth交易区下,此即为eth对usdt的)
        /// 	price_change	string	价格变化
        /// 	price_change_rate	string	价格变化百分比
        /// 	symbol_id	int	交易对ID
        /// 	timestamp	int	秒级时间戳
        /// 	total_amount	string	总交易额(24h)
        /// 	total_volume	string	总交易量(24h)
        /// 	usdt_amount	string	对应的USDT总交易量(24h)
        /// </summary>
        /// <returns></returns>
        public XElement GetMarketReal(int symbolId)
        {
            string apiRelativePath = "/api/v1/market/real/";

            // Set parameters
            IDictionary<string, object> req = new Dictionary<string, object>();
            req.Add("symbol_id", symbolId);

            string result = ApiQuery(apiRelativePath, req);
            result = "{data:" + result + "}";
            XNode node = JsonConvert.DeserializeXNode(result, "Root");
            var data = node.Document.Root.Element("data");
            return data;
        }

        /// <summary>
        /// Query API Use Http Get
        /// </summary>
        /// <param name="apiRelativePath">API Relative Path</param>
        /// <param name="req">Request parameters</param>
        /// <returns>Api Result</returns>
        private string ApiQuery(string apiRelativePath, IDictionary<string, object> req)
        {
            var apiPath = string.Format(DragonExConstants.API_URL_FORMAT,
                DragonExConstants.API_BASE_URL, apiRelativePath);
            string queryStr = "";
            if (req != null && req.Count > 0)
            {
                queryStr = "?" + ToQueryString(req);
            }
            apiPath += queryStr;

            var wb = WebRequest.Create(apiPath);

            wb.ContentType = "application/json";
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

        /// <summary>
        /// Get query string
        /// </summary>
        /// <param name="dic">Parameter Dictionary</param>
        /// <returns>Query string</returns>
        private static string ToQueryString(IDictionary<string, object> dic)
        {
            var array = (from key in dic.Keys
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(dic[key].ToString())))
                .ToArray();
            return string.Join("&", array);
        }
    }
}
