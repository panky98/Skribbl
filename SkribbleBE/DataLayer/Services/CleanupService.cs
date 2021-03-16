using DataLayer.DTOs;
using DataLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class CleanupService : IHostedService
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CleanupService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(RemoveUnsavedGames, null,0, 15 * 60 * 1000);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void RemoveUnsavedGames(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                TokIgreService tokIgreService = scope.ServiceProvider.GetRequiredService<TokIgreService>();
                tokIgreService.DeleteAllUnusedTokIgre();
            }
        }
    }
}
