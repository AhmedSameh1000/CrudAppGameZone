using GameZone.Attributes;
using GameZone.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace GameZone.ViewModels
{
    public class CreateGameVM
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [
            AllowedExtensionsAttributes(FileSettings.AllowedExtension),
            AllowedFileMaxSizeAttributes(FileSettings.MaxFileSizeInBytes)
        ]
        public IFormFile Cover { get; set; } = default!;

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = default!;
    }
}