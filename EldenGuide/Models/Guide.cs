namespace EldenGuide.Models
{
    public class Guide
    {
        public string GuideId { get; set; }
        public string AppName { get; set;}
        public string Category { get; set;}    
        public string Content {  get; set;}
        public string[] TOC { get; set;} 
        public string AppLogo { get; set;}


    }
}
