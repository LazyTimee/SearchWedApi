namespace SearchWedApi.Domain
{
    public class DbInicializer
    {
        public static void Initialize(SearchDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
