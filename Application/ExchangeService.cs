using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Exchange
{
    public class ExchangeService
    {
        HttpClient _client;
        Logger _logger;
        string _baseUrl;

        public ExchangeService(Logger logger, string baseUrl)
        {
            _client = new HttpClient();
            _baseUrl = baseUrl;
            _logger = logger;
        }

        public string[] ListCurrencies()
        {
            string uri = _baseUrl + "tables/a";
            HttpResponseMessage response = _client.GetAsync(uri).Result;

            if (!response.IsSuccessStatusCode)
            {
                _logger.Log(uri, response.StatusCode.ToString());
                throw new Exception("NBP response failure");
            }

            _logger.Log(uri, null);
            string responseString = response.Content.ReadAsStringAsync().Result;
            JToken responseObject = JToken.Parse(responseString);

            List<string> currencies = new List<string>();
            foreach (JToken currency in responseObject[0]["rates"])
                currencies.Add((string)currency["code"]);

            return currencies.ToArray();
        }

        public decimal GetRate(string currency)
        {
            string uri = _baseUrl + "rates/a/" + currency;
            HttpResponseMessage response = _client.GetAsync(uri).Result;

            if (!response.IsSuccessStatusCode)
            {
                _logger.Log(uri, response.StatusCode.ToString());
                throw new Exception("NBP response failure");
            }

            _logger.Log(uri, null);
            string responseString = response.Content.ReadAsStringAsync().Result;
            JToken responseObject = JToken.Parse(responseString);

            return (decimal)responseObject["rates"][0]["mid"];
        }

        public decimal Calculate(string inCurrency, string outCurrency, decimal amount)
        {
            decimal inRate = inCurrency.ToLower() == "pln" ? 1 : GetRate(inCurrency);
            decimal outRate = outCurrency.ToLower() == "pln" ? 1 : GetRate(outCurrency);
            return amount * inRate / outRate;
        }
    }
}
