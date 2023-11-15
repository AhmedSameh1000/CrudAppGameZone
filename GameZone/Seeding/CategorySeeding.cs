namespace GameZone.Seeding
{
    public static class CategorySeeding
    {
        public static IEnumerable<Category> LoadCategories()
        {
            return new List<Category>()
            {
                new Category{Id=1, Name="Sprts"},
                new Category{Id=2, Name="Action"},
                new Category{Id=3, Name="Adventure"},
                new Category{Id=4, Name="Racing"},
                new Category{Id=5, Name="Fighting"},
                new Category{Id=6, Name="Film"},
            };
        }
    }

    public static class DeviceSeeding
    {
        public static IEnumerable<Device> LoadDevices()
        {
            return new List<Device>()
            {
                new Device { Id = 1, Name = "PlayStation", Icon = "bi bi-playstation" },
                new Device { Id = 2, Name = "xbox", Icon = "bi bi-xbox" },
                new Device { Id = 3, Name = "Nintendo Switch", Icon = "bi bi-nintendo-switch" },
                new Device { Id = 4, Name = "PC", Icon = "bi bi-pc-display" }
            };
        }
    }
}