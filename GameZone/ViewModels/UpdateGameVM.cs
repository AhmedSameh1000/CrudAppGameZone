using GameZone.Attributes;
using GameZone.Settings;

namespace GameZone.ViewModels
{
    public class UpdateGameVM : GameFormViewModel
    {
        public int Id { get; set; }

        [
            AllowedExtensionsAttributes(FileSettings.AllowedExtension),
            AllowedFileMaxSizeAttributes(FileSettings.MaxFileSizeInBytes)
        ]
        public IFormFile? Cover { get; set; } = default!;

        public string? ImageUrl { get; set; }
    }
}