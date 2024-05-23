namespace GameZone.Attributes
{
    public class AllowedExtAttribute : ValidationAttribute
    {
        private readonly string _allowedEXT;
        public AllowedExtAttribute( string allowedExt)
        {
            _allowedEXT = allowedExt;   
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var File = value as IFormFile;
            if (File is not null) 
            {
                var Extention = Path.GetExtension(File.FileName);
                var isAllowed = _allowedEXT.Split(",").Contains(Extention, StringComparer.OrdinalIgnoreCase);
                if (!isAllowed) 
                {
                    return new ValidationResult($"Only{_allowedEXT}  are allowed");
                }
            }

            return ValidationResult.Success;
        }
    }
}
