using CodeSnippetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeSnippetApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
        {
            
        }
        public DbSet<CodeSnippet>CodeSnippets { get; set; }
    }
}
