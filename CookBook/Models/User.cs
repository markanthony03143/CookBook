using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models;

public partial class User
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Please enter a username")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an email")]
    [UniqueEmail(ErrorMessage = "Email must be unique.")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a password")]
    public string Password { get; set; } = null!;

    public string? ProfilePicUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
