using System;
using System.Collections.Generic;

namespace Practice1.Models
{
    public partial class BookAuthor
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public string AuthorOrder { get; set; } = null!;
        public string RoyalityPercentage { get; set; } = null!;

        public virtual Author Author { get; set; } = null!;
        public virtual Book Book { get; set; } = null!;
    }
}
