using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDBContext context;

        public PlatformRepo(AppDBContext context)
        {
            this.context = context;
        }
        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
                throw new ArgumentNullException(nameof(platform));
                
            this.context.Platform.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return this.context.Platform.ToList();
        }

        public Platform? GetPlatformById(int id)
        {
            return this.context.Platform.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return this.context.SaveChanges() >= 0;
        }
    }
}