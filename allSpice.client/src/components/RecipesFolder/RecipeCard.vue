<template>
  <div class="card text-shadow skelton-loader">
    <img :src="recipe.img" class="card-img img-fluid imgSize" alt="..." />
    <div>hello</div>
    <div class="card-img-overlay p-1 px-2 event">
      <div class="d-flex justify-content-between">
        <i
          class="mdi mdi-heart text-danger"
          @click="deleteFavorite(favorited, index)"
          v-if="favorited"
        ></i>
        <i class="mdi mdi-heart" @click="favoriteRecipe()" v-else></i>
      </div>
      <div
        @click="makeActive(recipe)"
        data-bs-toggle="modal"
        data-bs-target="#exampleModal"
      >
        <h5 class="card-title">{{ recipe.title }}</h5>
        <p class="card-text">{{ recipe.instructions }}</p>
        <p class="card-text">
          <small>{{ recipe.category }}</small>
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../../AppState.js";
import { Recipe } from "../../models/Recipe.js";
import { favoritesService } from "../../services/FavoritesService.js";
import { recipeService } from "../../services/RecipesService.js";
import Pop from "../../utils/Pop.js";
export default {
  props: {
    recipe: { type: Recipe, required: true },
  },
  setup(props) {
    return {
      favorited: computed(() => {
        let favorited = AppState.favorites.find(
          (f, index) => f.recipeId == props.recipe.id
        );
        if (favorited) {
          let recipe = AppState.recipes.find((r) => r.id == favorited.recipeId);
          recipe.favorited = true;
          recipe.favoriteId = favorited.id;
        }
        return favorited;
      }),
      makeActive(recipeData) {
        if (AppState.activeRecipe != recipeData) {
          AppState.activeRecipe = recipeData;
        }
      },
      async favoriteRecipe() {
        try {
          await favoritesService.favoriteRecipe(props.recipe);
        } catch (error) {
          Pop.error(error);
        }
      },
      async deleteFavorite(favorited, index) {
        try {
          const yes = await Pop.confirm();
          if (!yes) {
            return;
          }
          console.log(favorited, index);
          let favoriteId = this.favorited.id;
          await favoritesService.deleteFavorite(favoriteId);
          Pop.success(`${props.recipe.title} removed from favorites`);
        } catch (error) {
          Pop.error(error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.imgSize {
  height: 20rem;
}
.event {
  background: rgba(204, 243, 253, 0.2);
  border: 1px solid rgba(86, 199, 251, 0.2);
  backdrop-filter: blur(2px);
  /* Note: backdrop-filter has minimal browser support */

  border-radius: 3px;
}
.event:hover {
  background: rgba(204, 243, 253, 0.2);
  border: 1px solid rgba(86, 199, 251, 0.2);
  backdrop-filter: blur(0px);

  border-radius: 3px;
}
.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px salmon;
  font-weight: bold;
  letter-spacing: 0.08rem;

  /* Second Color  in text-shadow is the blur */
}
</style>
