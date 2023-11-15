namespace GameZone.Services.NewFolder
{
    public class CategoriesService : ICategoriesService
    {
        private readonly AppDbContext _appDbContext;

        public CategoriesService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<SelectListItem> GetSelectListCategories()
        {
            var Categories = _appDbContext.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            })
            .OrderBy(c => c.Text)
            .AsNoTracking()
            .ToList();

            return Categories;
        }
    }
}