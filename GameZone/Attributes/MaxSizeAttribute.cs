namespace GameZone.Attributes
{
    public class MaxSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;
        public MaxSizeAttribute(int maxSize)
        {
           maxSize = _maxSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var File = value as IFormFile;
            if (File is not null)
            {
                
                if (File.Length > FileSetting.AllowedMaxSizeByte)
                {
                    return new ValidationResult($"Maximum allowed file size  is 1");
                }
            }

            return ValidationResult.Success;
        }
    }
}
