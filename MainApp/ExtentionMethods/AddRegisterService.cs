using Infrastructure.Services.PersonService;

namespace MainApp.ExtentionMethods;

public static class AddRegisterService
{
    public static void Register(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IPersonService, PersonService>();
        
        serviceCollection.AddControllers();
    }
}