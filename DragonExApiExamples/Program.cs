using DragonExApiExamples.Api.DragonEx;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonExApiExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Test for getting all coins api.");
            var ele = DragonExPublicApis.Instance.GetAllCoins();
            System.Console.WriteLine("The result of getting all coins api is:");
            System.Console.WriteLine(JsonConvert.SerializeObject(ele));

            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Test for getting all symbols api.");
            ele = DragonExPublicApis.Instance.GetAllSymbols();
            System.Console.WriteLine("The result of getting all symbols api is:");
            System.Console.WriteLine(JsonConvert.SerializeObject(ele));

            // btc_usdt symbol_id = 101
            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Test for getting k line for btc_usdt.");
            ele = DragonExPublicApis.Instance.GetMarketKLine(101);
            System.Console.WriteLine("The result of getting k line for btc_usdt is:");
            System.Console.WriteLine(JsonConvert.SerializeObject(ele));

            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Test for getting market buy for btc_usdt.");
            ele = DragonExPublicApis.Instance.GetMarketBuy(101);
            System.Console.WriteLine("The result of getting market buy for btc_usdt is:");
            System.Console.WriteLine(JsonConvert.SerializeObject(ele));

            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Test for getting market sell for btc_usdt.");
            ele = DragonExPublicApis.Instance.GetMarketSell(101);
            System.Console.WriteLine("The result of getting market sell for btc_usdt is:");
            System.Console.WriteLine(JsonConvert.SerializeObject(ele));

            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Test for getting market real for btc_usdt.");
            ele = DragonExPublicApis.Instance.GetMarketReal(101);
            System.Console.WriteLine("The result of getting market real for btc_usdt is:");
            System.Console.WriteLine(JsonConvert.SerializeObject(ele));

            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Test for getting token status.");
            var ele2 = DragonExPrivateApis.Instance.GetTokenStatus();
            System.Console.WriteLine("The result of getting token status is:");
            System.Console.WriteLine(JsonConvert.SerializeObject(ele2));

            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Test for getting user own.");
            ele2 = DragonExPrivateApis.Instance.GetUserOwn();
            System.Console.WriteLine("The result of getting user own is:");
            System.Console.WriteLine(JsonConvert.SerializeObject(ele2));

            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Test for getting deal history.");
            ele2 = DragonExPrivateApis.Instance.GetDealHistory(101);
            System.Console.WriteLine("The result of getting deal history is:");
            System.Console.WriteLine(JsonConvert.SerializeObject(ele2));

            System.Console.ReadKey();
        }
    }
}
