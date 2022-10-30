namespace allSpice.Models;

public class Recipe : ICreated, IRepoItem<int>
{
  public int Id { get ; set ; }
  public DateTime CreatedAt { get ; set ; }
  public DateTime UpdatedAt { get ; set ; }
  public string CreatorId { get ; set ; }
  public Profile Creator { get ; set ; }

  public string Title { get; set; }
  public string Instructions { get; set; }
  public string Img { get; set; }
  public string Category { get; set; }
  public  int FavoriteCount {get; set;}
  public int IngredientCount{get; set;}
  public bool favorited {get; set;}
}