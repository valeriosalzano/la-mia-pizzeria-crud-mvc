using la_mia_pizzeria.Interfaces;
using System.Diagnostics;

namespace la_mia_pizzeria.CustomLoggers
{
    public class CustomConsoleLogger : ICustomLogger
    {
        public void WriteLog(string message)
        {
            Debug.WriteLine($"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} LOG: {message}");
        }
    }
}
