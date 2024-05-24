using System;
using System.Collections.Generic;

namespace CookBook.Models;

public partial class Like
{
    public int LikeId { get; set; }

    public int? RecipeId { get; set; }

    public int? UserId { get; set; }

    public DateTime? LikedAt { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }
}
