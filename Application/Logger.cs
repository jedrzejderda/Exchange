using System;

namespace Exchange
{
    public class Logger
    {
        ExchangeContext _context;

        public Logger(ExchangeContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public void Log(string request, string errorMsg)
        {
            _context.Logs.Add(new Log()
            {
                Request = request,
                Error = errorMsg,
                Date = DateTime.Now
            });

            _context.SaveChanges();
        }
    }
}
