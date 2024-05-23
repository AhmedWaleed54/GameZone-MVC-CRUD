
namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel : GameFormViewModel
    {
        [AllowedExt(FileSetting.AllowedExt),MaxSize(FileSetting.AllowedMaxSizeByte)]
        public IFormFile Cover { get; set; } = default!;

    }
}
