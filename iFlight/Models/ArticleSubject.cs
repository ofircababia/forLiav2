using System;
using System.Collections.Generic;

namespace iFlight.Models;

public partial class ArticleSubject
{
    public string SubjectName { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
