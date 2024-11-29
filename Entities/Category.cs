using System.Formats.Tar;

namespace Entities;

public class Category
{
    private Category() { }

    public Category(string name, string description)
    {
        Name = name;
        Description = description;
        Posts = new List<Post>();
    }
    
    public int Id { get; set; }
    public int PostId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public virtual ICollection<Post> Posts { get; set; }
}

/*public class CategoryCollection
{
    private List<Category> categories; 

    public CategoryCollection()
    {
        categories = new List<Category>();
    }
}*/
