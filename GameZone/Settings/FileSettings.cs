namespace GameZone.Settings
{
    public class FileSettings
    {
        public const string AllowedExtension = ".jpg,.jpeg,.png";

        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}