﻿using ArgusMon;
using System.Runtime.Loader;

namespace ArduTempBridge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var daemon = new SerialDaemon("/dev/ttyUSB0", 57600);

            // Signal für sauberes Beenden
            AssemblyLoadContext.Default.Unloading += ctx => daemon.Stop();
            Console.CancelKeyPress += (s, e) => { e.Cancel = true; daemon.Stop(); };

            daemon.Start();

            // Warten bis beendet
            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
