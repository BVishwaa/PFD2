using System.ComponentModel.DataAnnotations;

namespace EldenGuide.Models
{
    public class Guide
    {
        public string GuideId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string AppName { get; set;}

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set;}

        
        public string Content {  get; set;}

        public string[] TOC { get; set;}

        [Required]
        public string AppLogo { get; set;}


    }
}
