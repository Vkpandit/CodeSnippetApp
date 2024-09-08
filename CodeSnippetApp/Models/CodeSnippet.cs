using System.ComponentModel.DataAnnotations;

namespace CodeSnippetApp.Models
{
    public class CodeSnippet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
    }
}

