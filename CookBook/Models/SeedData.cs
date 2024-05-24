using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CookBook.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace CookBook.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app) 
        {
            CookBookDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<CookBookDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Username = "Mark Anthony Miñoza",
                        Email = "mark@gmail.com",
                        Password = "mark123",
                        ProfilePicUrl = "https://ui-avatars.com/api/?name=M",

                    },
                    new User
                    {
                        Username = "Bjelland Scott Arreglo",
                        Email = "scottarreglo@gmail.com",
                        Password = "scott123",
                        ProfilePicUrl = "https://ui-avatars.com/api/?name=S",

                    },
                    new User
                    {
                        Username = "JASPER ELDRICH BALLESTEROS",
                        Email = "jasper@gmail.com",
                        Password = "jasper123",
                        ProfilePicUrl = "https://ui-avatars.com/api/?name=J",

                    },
                    new User
                    {
                        Username = "KHARL MILLER DAGUMAN",
                        Email = "kharl@gmail.com",
                        Password = "kharl123",
                        ProfilePicUrl = "https://ui-avatars.com/api/?name=K",

                    },
                    new User
                    {
                        Username = "RONALD DAVE LAZARTE",
                        Email = "ronalddave@gmail.com",
                        Password = "ronalddave123",
                        ProfilePicUrl = "https://ui-avatars.com/api/?name=R",

                    }
                );
                context.SaveChanges();
            }

            if (!context.Recipes.Any())
            {
                context.Recipes.AddRange(
                    new Recipe
                    {
                        UserId = 1,
                        Title = "Omellete for Life",
                        Description = "To make an omelette, beat 2-3 eggs with a little milk or water, salt, and pepper. Heat a non-stick skillet with some butter or oil over medium heat, then pour in the eggs. Let them cook until the edges set, add your desired fillings (cheese, veggies, etc.), and fold the omelette in half. Cook for another minute or two until done, then serve."

                    },
                    new Recipe
                    {
                        UserId = 2,
                        Title = "Scrambled Eggs",
                        Description = "Beat 2-3 eggs with optional milk or cream, salt, and pepper. Heat a skillet with butter or oil over medium-low heat, pour in the eggs, let them sit briefly, then gently stir until they are mostly set but still a bit runny. Remove from heat and let the residual warmth finish cooking them before serving."

                    },
                    new Recipe
                    {
                        UserId = 3,
                        Title = "Pork Adobo",
                        Description = "Tips:\nMarinate the pork in soy sauce, vinegar, and crushed garlic for at least 30 minutes for deeper flavor.\nSear the pork in a hot pan to brown the edges before simmering.\nSimmer the pork with the marinade, bay leaves, peppercorns, and onions until tender. Adjust the seasoning with sugar for balance."

                    },
                    new Recipe
                    {
                        UserId = 4,
                        Title = "Pork Schnitzel",
                        Description = "Ingredients: Pork cutlets, flour, eggs, breadcrumbs, salt, pepper, oil for frying.\n\nTips:\n\nPound the pork cutlets thin for even cooking.\nSeason the flour, eggs, and breadcrumbs with salt and pepper.\nDredge the pork in flour, then dip in beaten eggs, and coat with breadcrumbs.\nFry in hot oil until golden brown and crispy on both sides."

                    },
                    new Recipe
                    {
                        UserId = 5,
                        Title = "Pork Stir-Fry",
                        Description = "Ingredients: Pork tenderloin, assorted vegetables (bell peppers, onions, broccoli), soy sauce, garlic, ginger, and cornstarch.\n\nTips:\n\nSlice the pork thinly against the grain for tenderness.\nMarinate the pork in soy sauce, garlic, ginger, and a little cornstarch to keep it juicy.\nStir-fry on high heat to quickly cook the pork and vegetables without overcooking."

                    }
                );
                context.SaveChanges();
            }

            if (!context.Media.Any())
            {
                context.Media.AddRange(
                    new Medium
                    {
                        RecipeId = 1,
                        MediaType = "image",
                        MediaUrl = "~/img/Omelette.jpeg"
                    },
                    new Medium
                    {
                        RecipeId = 1,
                        MediaType = "video",
                        MediaUrl = "~/img/OmeletteVid.mp4"
                    },
                    new Medium
                    {
                        RecipeId = 1,
                        MediaType = "image",
                        MediaUrl = "~/img/Omelette2.jpg"
                    },
                    new Medium
                    {
                        RecipeId = 2,
                        MediaType = "image",
                        MediaUrl = "~/img/ScrambledEggs.jpeg"
                    },
                    new Medium
                    {
                        RecipeId = 2,
                        MediaType = "image",
                        MediaUrl = "~/img/ScrambledEggs2.jpg"
                    },
                    new Medium
                    {
                        RecipeId = 2,
                        MediaType = "video",
                        MediaUrl = "~/img/ScrambledEggsVid.mp4"
                    },
                    new Medium
                    {
                        RecipeId = 3,
                        MediaType = "video",
                        MediaUrl = "~/img/PorkAdoboVid.mp4"
                    },
                    new Medium
                    {
                        RecipeId = 4,
                        MediaType = "image",
                        MediaUrl = "~/img/PorkSchnitzel.jpg"
                    },
                    new Medium
                    {
                        RecipeId = 4,
                        MediaType = "video",
                        MediaUrl = "~/img/PorkSchnitzelVid.mp4"
                    },
                    new Medium
                    {
                        RecipeId = 5,
                        MediaType = "image",
                        MediaUrl = "~/img/PorkStirFry.jpg"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Likes.Any())
            {
                context.Likes.AddRange(
                    new Like
                    {
                        RecipeId = 5,
                        UserId = 1
                    },
                    new Like
                    {
                        RecipeId = 4,
                        UserId = 1
                    },
                    new Like
                    {
                        RecipeId = 1,
                        UserId = 2
                    },
                    new Like
                    {
                        RecipeId = 3,
                        UserId = 2
                    },
                    new Like
                    {
                        RecipeId = 1,
                        UserId = 3
                    },
                    new Like
                    {
                        RecipeId = 5,
                        UserId = 3
                    },
                    new Like
                    {
                        RecipeId = 3,
                        UserId = 4
                    },
                    new Like
                    {
                        RecipeId = 2,
                        UserId = 4
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
