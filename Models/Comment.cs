namespace blog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
