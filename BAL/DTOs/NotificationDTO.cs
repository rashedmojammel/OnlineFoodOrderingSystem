namespace BAL.DTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool? IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}