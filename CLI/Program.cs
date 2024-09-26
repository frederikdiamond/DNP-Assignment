
using CLI.UI;
using FileRepositories;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app..");
IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();
IPostRepository postRepository = new PostFileRepository();

CliApp cliApp = new CliApp(userRepository, postRepository);
await cliApp.RunAsync();