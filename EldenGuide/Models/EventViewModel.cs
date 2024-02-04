using System.ComponentModel.DataAnnotations;
namespace EldenGuide.Models
{

    public class EventViewModel
    {
        [Required(ErrorMessage = "Event ID is required.")]
        public int EventID { get; set; }

        [Required(ErrorMessage = "Event Name is required.")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Details are required.")]
        public string Details { get; set; }

        [Required(ErrorMessage = "Photo is required.")]
        public string EventPhoto { get; set; }
    }

}
