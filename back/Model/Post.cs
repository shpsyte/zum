public class Post
{
    public string Author { get; set; } = "";
    public int AuthorId { get; set; }
    public int Id { get; set; }
    public int Likes { get; set; }
    public double Popularity { get; set; }
    public int Reads { get; set; }
    public List<string> Tags { get; set; } = new();
}

public class Root
{
    public List<Post> Posts { get; set; } = new();
}
