using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AppDBContext>();

                if(context == null) throw new NullReferenceException("Context not found in services");

                SeedData(context);
            }
        }

        public static void SeedData(AppDBContext context)
        {
            if (context.Platform.Any())
            {
                Console.WriteLine("--> we have some data");
                return;
            }

            Console.WriteLine("--> Seeding data");
            context.AddRange(
                new Platform(){ Name="Dot Net", Cost="Free", Published="Microsoft"},
                new Platform(){ Name="SQL Server", Cost="Free", Published="Microsoft"},
                new Platform(){ Name="Kurbentes", Cost="Free", Published="Cloud Native Computing Foundation"}
            );
            context.SaveChanges();           
        }

    }
}