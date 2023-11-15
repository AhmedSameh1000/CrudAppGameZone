namespace GameZone.Attributes
{
    public class AllowedFileMaxSizeAttributes : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public AllowedFileMaxSizeAttributes(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {
            var File = value as IFormFile;

            if (File != null)
            {
                //Get File lenght=size in Bytes
                if (File.Length > _maxFileSize)
                {
                    return new ValidationResult($"Max Allowed Size is {_maxFileSize} Bytes");
                }
            }
            return ValidationResult.Success;
        }
    }
}