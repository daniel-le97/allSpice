export class Favorite {
  constructor(data) {
    this.id = data.id;
    this.recipeId = data.recipeId;
    this.accountId = data.accountId;
    this.createdAt = data.createdAt;
    this.updatedAt = data.updatedAt;
  }
}
