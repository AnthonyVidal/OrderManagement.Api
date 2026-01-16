using System;
using System.Threading;

namespace OrderManagement.Worker
{
    internal class Program
    {
        private static Timer _timer;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Servicio de Procesamiento de Órdenes INICIADO ===");
            Console.WriteLine("Presiona CTRL + C para detener el servicio.");

            _timer = new Timer(
                EjecutarTarea,
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(30) //Ejecutamos cada 30s la tarea de procesamiento
            );

            Console.CancelKeyPress += OnExit;

            Thread.Sleep(Timeout.Infinite);
        }

        private static void EjecutarTarea(object state)
        {
            Console.WriteLine(
                $"[{DateTime.Now:HH:mm:ss}] Ejecutando tarea de fondo..."
            );

            try
            {
                ProcesarOrdenes();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        private static void ProcesarOrdenes()
        {
            Console.WriteLine("Buscando órdenes pendientes...");

            Thread.Sleep(2000);

            Console.WriteLine(
                "Órdenes marcadas como PROCESADAS correctamente."
            );
        }

        private static void OnExit(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Deteniendo servicio...");
            _timer.Dispose();
            Console.WriteLine("Servicio detenido.");
        }
    }
}
