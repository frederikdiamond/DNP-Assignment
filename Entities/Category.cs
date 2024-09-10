namespace Entities;

public class Category
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}


public class CategoryCollection
{
    private List<Category> users;

    public CategoryCollection()
    {
        users = new List<Category>();
    }
}