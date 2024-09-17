using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;

    // Constructor injection for IPostRepository
    public ManagePostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    public async Task CreatePostAsync()
    {
        // Create the CreatePostView and pass the repository
        CreatePostView createPostView = new CreatePostView(postRepository);
        await createPostView.DisplayCreatePostAsync();
    }

    public void ListPosts()
    {
        ListPostsView listPostsView = new ListPostsView(postRepository);
        listPostsView.DisplayPosts(); //should be async
    }

    public void ViewPost()
    {
        SinglePostView singlePostView = new SinglePostView(postRepository);
        singlePostView.DisplayPostByIdAsync(); //How do why find a post? should this be async?
    }
}