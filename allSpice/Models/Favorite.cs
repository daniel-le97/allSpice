namespace allSpice.Models;

public class Favorite :IRepoItem<int>
{
  public int Id { get; set; }
  public int recipeId { get; set; }
  public string AccountId { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public Profile Creator {get; set;}
}