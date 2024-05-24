using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models;

public partial class Medium
{
    public int MediaId { get; set; }

    public int? RecipeId { get; set; }

    public string MediaType { get; set; } = null!;

    public string MediaUrl { get; set; } = null!;

    public DateTime? UploadedAt { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
