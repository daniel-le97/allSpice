import { reactive } from "vue";

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Recipe.js').Recipe[]} */
  recipes: [],
  /** @type {import('./models/Favorite.js').Favorite[]} */
  favorites: [],
  /** @type {import('./models/Recipe.js').Recipe | null} */
  activeRecipe: null,
  /** @type {import('./models/Ingredient.js').Ingredient[]} */
  activeRecipeIngredients: [],
  /** @type {import('./models/Ingredient.js').Ingredient[]} */
  newRecipeIngredients: [],
  modalForm: 0,
  offset: 0,
  favNumber: 0,
  favoriting: false
});
