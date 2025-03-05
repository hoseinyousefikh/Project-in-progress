using App.Domain.Core.Home.Contract.AppServices.Categories;
using App.Domain.Core.Home.Entities.Categories;

namespace DwellMVC.BackgroundServices
{
    public class RandomHomeServicesUpdater : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;  

        public RandomHomeServicesUpdater(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await UpdateRandomHomeServices(); 
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        public async Task<List<HomeService>> UpdateRandomHomeServices()
        {
            List<HomeService> updatedHomeServices = new List<HomeService>();

            using (var scope = _serviceProvider.CreateScope())
            {
                var homeServiceAppService = scope.ServiceProvider.GetRequiredService<IHomeServiceAppService>();
                var allHomeServices = await homeServiceAppService.GetAllHomeServicesAsync(CancellationToken.None);

                if (allHomeServices != null)
                {
                    updatedHomeServices = allHomeServices
                        .OrderBy(x => Guid.NewGuid())
                        .Take(3)
                        .ToList();
                }
            }

            return updatedHomeServices;
        }
    }
}
