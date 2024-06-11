using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Models;

namespace OpenAI_UIR.Db
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ConversationUser> ConversationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //
            modelBuilder.Entity<Conversation>()
                .HasDiscriminator<string>("conversation_type")
                .HasValue<ConversationUser>("user")
                .HasValue<ConversationAnonymous>("anonymous");
            // Question and Answer
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Answer)
                .WithOne(q => q.Question)
                .HasForeignKey<Answer>(q => q.QuestionId);
            // Question and Conversation
            modelBuilder.Entity<Conversation>()
            .HasMany(c => c.Questions)
            .WithOne(q => q.Conversation)
            .HasForeignKey(q => q.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);
            // Conversation and User
            modelBuilder.Entity<ConversationUser>()
                .HasOne(c=>c.User)
                .WithMany(u=>u.Conversations)
                .HasForeignKey(c=>c.UserId);

            modelBuilder.Entity<Admin>().HasData(
                    new Admin()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Jobintech",
                        UserName = "jobintech@jobintech-uir.ma",
                        Password = "@Jobintech2024@",
                    }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
