using System.ComponentModel.DataAnnotations;

namespace CaprezzoDigitale.WebApi.Models
{
    public enum TipiStatistica
    {
        [Display(Name = "AperturaApp", Description = "")]
        AperturaApp = 1,
        [Display(Name = "InstallazioneApp", Description = "")]
        InstallazioneApp = 2
    }
}
