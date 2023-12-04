using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Projet.Models;
using Projet.Data;

namespace Projet
{
    public class ResetPasswordCleanupService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _services;
        private Timer _timer;

        public ResetPasswordCleanupService(IServiceProvider services)
        {
            _services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)); // Run every minute
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDB>(); // Replace YourDbContext with your actual DbContext class
                var currentTime = DateTime.Now;

                // Query and delete expired reset requests
                var expiredRequests = dbContext.resetpasswords
                    .Where(r => r.Expiry < currentTime)
                    .ToList();

                dbContext.resetpasswords.RemoveRange(expiredRequests);
                dbContext.SaveChanges();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
