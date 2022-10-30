namespace allSpice.Models;

public class FavRecipe : Recipe{
  public string AccountId{get;set;}
  public int RecipeId { get; set; }
  public int favoriteId { get; set; }
  // public bool isFavorited {get; set;}
}