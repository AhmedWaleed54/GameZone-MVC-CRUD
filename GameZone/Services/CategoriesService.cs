
namespace GameZone.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDBContext _dbContext;

        public CategoriesService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<SelectListItem> GetSelectLists()
        {
            
            return _dbContext.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
