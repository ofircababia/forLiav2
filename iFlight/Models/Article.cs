using System;
using System.Collections.Generic;

namespace iFlight.Models;

public partial class Article
{
    public short ArticleNumber { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public short? WriterLicenseNumber { get; set; }

    public string? SubjectName { get; set; }

    public virtual ArticleSubject? SubjectNameNavigation { get; set; }

    public virtual Pilot? WriterLicenseNumberNavigation { get; set; }
}
