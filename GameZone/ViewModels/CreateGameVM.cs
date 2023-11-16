using GameZone.Attributes;
using GameZone.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace GameZone.ViewModels
{
    public class CreateGameVM : GameFormViewModel
    {
        [
           AllowedExtensionsAttributes(FileSettings.AllowedExtension),
           AllowedFileMaxSizeAttributes(FileSettings.MaxFileSizeInBytes)
       ]
        public IFormFile Cover { get; set; } = default!;
    }
}