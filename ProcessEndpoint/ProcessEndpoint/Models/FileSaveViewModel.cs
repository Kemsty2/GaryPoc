using System.ComponentModel.DataAnnotations;

namespace ProcessEndpoint.Models;

public class FileSaveViewModel {
    [Required]
    [Display(Name = "FileName")]
    public string? FileName {get;set;}

    public string? FolderPath {get;set;}

    [Required]
    [Display(Name = "Content")]
    public string? Content {get;set;}

    public override string ToString()
    {
        return $"[Content = {Content}, FileName = {FileName}, FolderPath = {FolderPath}]";
    }
}