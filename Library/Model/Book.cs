using System;
using System.Collections.Generic;

namespace Library.Model;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int? AuthorId { get; set; }

    public int? PublicationYear { get; set; }

    public virtual Author? Author { get; set; }
}
