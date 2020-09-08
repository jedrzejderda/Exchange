using System;
using Microsoft.AspNetCore.Mvc;

namespace Exchange
{
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        Logger _logger;
        ExchangeService _exchange;

        public ExchangeController(ExchangeService exchange, Logger logger)
        {
            _logger = logger;
            _exchange = exchange;
        }

        [Route("list")]
        [HttpGet]
        public IActionResult GetList()
        {
            try
            {
                string[] result = _exchange.ListCurrencies();
                _logger.Log(Request.Path, null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Log(Request.Path, ex.Message);
                return StatusCode(500);
            }
        }

        [Route("rate/{currency}")]
        [HttpGet]
        public IActionResult GetRate(string currency)
        {
            try
            {
                decimal result = _exchange.GetRate(currency);
                result = decimal.Round(result, 4);
                _logger.Log(Request.Path, null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Log(Request.Path, ex.Message);
                return StatusCode(500);
            }
        }

        [Route("calculate")]
        [HttpGet]
        public IActionResult GetCalculate(string from, string to, decimal amount)
        {
            try
            {
                decimal result = _exchange.Calculate(from, to, amount);
                result = decimal.Round(result, 2);
                _logger.Log(Request.Path, null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Log(Request.Path, ex.Message);
                return StatusCode(500);
            }
        }
    }
}
