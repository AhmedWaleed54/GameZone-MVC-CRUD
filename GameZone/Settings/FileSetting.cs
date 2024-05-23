using static System.Net.Mime.MediaTypeNames;

namespace GameZone.Settings
{
    public static class FileSetting
    {
        public const string ImagePath = "/Assets/images";
        public const string AllowedExt = ".jpg,.jpeg,.png";
        public const int AllowedMaxSizeMB = 1;
        public const int AllowedMaxSizeByte = AllowedMaxSizeMB*1024*1024;
        
    }
}
