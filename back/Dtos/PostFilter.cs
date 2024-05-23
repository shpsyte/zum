

using System.ComponentModel.DataAnnotations;

public class PostFilter
{
    [Required]
    public string tags { get; set; } = "tech";

    /// <summary>
    /// Sort column. Default is id.
    /// </summary>
    [CheckRangeString(new string[] { "id", "reads", "likes", "popularity" })]
    public string? sortBy { get; set; } = "id";

    /// <summary>
    /// Sort direction. Default is ascending.
    /// </summary>
    [CheckRangeString(new string[] { "asc", "desc" })]
    public string? direction { get; set; } = "asc";
}


