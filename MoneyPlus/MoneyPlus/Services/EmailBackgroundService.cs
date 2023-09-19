using MoneyPlus.Data;
using MoneyPlus.Services.EmailService;

namespace MoneyPlus.Services
{
    public class EmailBackgroundService : BackgroundService
    {

        TimeSpan IntervalBetweenJobs = TimeSpan.FromHours(24);


        public IServiceProvider _serviceProvider { get; }

        public EmailBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var email = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    var userIds = ctx.Users.Select(x => x.Id).ToList();

                    email.SendEmail(userIds);
                }
                
                await Task.Delay(IntervalBetweenJobs);
            }
        }



    }
}
