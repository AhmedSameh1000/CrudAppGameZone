namespace GameZone.Services.NewFolder
{
    public interface ICategoriesService
    {
        IEnumerable<SelectListItem> GetSelectListCategories();
    }
}