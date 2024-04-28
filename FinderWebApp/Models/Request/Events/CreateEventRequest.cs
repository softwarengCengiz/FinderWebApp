namespace FinderWebApp.Models.Request.Events
{
	public class CreateEventRequest
	{
        public string EventHeader { get; set; }
        public string EventDetail { get; set; }
        public IFormFile EventImage { get; set; }
        public bool IsActive { get; set; }
    }
}
