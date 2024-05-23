namespace GameZone.ViewModels
{
    public class EditGameFormViewModel : GameFormViewModel
    {
        public int id { get; set; }

        public string? CurrentCover { get; set; }

        [AllowedExt(FileSetting.AllowedExt), MaxSize(FileSetting.AllowedMaxSizeByte)]
        public IFormFile? Cover { get; set; } = default!;

    }
}
