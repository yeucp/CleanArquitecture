using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services
{
    public class WriteFile : IHostedService
    {
        private readonly string _fileName = "File1.txt";
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Timer _timer;
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            Write("Recurrent process started");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Dispose();
            Write("Recurrent process stoped");
            return Task.CompletedTask;
        }

        public void DoWork(object state) {
            Write($"Recurrent process executed {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
        }

        public void Write(string message) {
            string r = Directory.GetCurrentDirectory();
            var route = $@"{r}\wwwroot\{_fileName}";

            using StreamWriter writer = new StreamWriter(route, append: true);
            writer.WriteLine(message);
        }
    }
}
