using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class AppContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Reaction> Reactions => Set<Reaction>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source = /Users/unknownuser/dev_projects/school/DNP-Assignment/EfcRepositories/app.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Comment>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Comment>()
            .HasOne<Post>()
            .WithMany()
            .HasForeignKey(c => c.PostId);
        
        modelBuilder.Entity<Category>()
            .HasOne<Post>()
            .WithMany()
            .HasForeignKey(c => c.PostId);
        
        modelBuilder.Entity<Reaction>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(r => r.UserId);
        
        modelBuilder.Entity<Reaction>()
            .HasOne<Post>()
            .WithMany()
            .HasForeignKey(r => r.PostId);

        modelBuilder.Entity<Reaction>()
            .HasOne<Comment>()
            .WithMany()
            .HasForeignKey(r => r.CommentId);
    }
}
