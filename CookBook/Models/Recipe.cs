using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public int? UserId { get; set; }

    [Required(ErrorMessage = "Please enter a title or recipe name")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a description or caption")]
    public string Description { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Medium> Media { get; set; } = new List<Medium>();

    public virtual User? User { get; set; }
}
