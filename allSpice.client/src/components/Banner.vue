<template>
  <div class="container-fluid">
    <div class="row">
      <div
        class="col-md-12 p-0 mt-md-3 mt-sm-0 position-relative img rounded elevation-1"
      >
        <Login />
        <div class="row">
          <div
            class="col-8 bg-white position-absolute overlay rounded d-flex justify-content-between elevation-5"
          >
            <button
              class="btn no-select selectable"
              :class="number == 0 ? 'bannerBtn' : ''"
              auto
              @click="getAllRecipes"
              id="homeBtn"
            >
              home
            </button>
            <button
              class="btn no-select selectable"
              :class="number == 2 ? 'bannerBtn' : ''"
              @click="getMyRecipes"
            >
              recipes
            </button>
            <button
              class="btn no-select selectable"
              :class="number == 1 ? 'bannerBtn' : ''"
              @click="getFavorites"
            >
              favorites
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";

import { accountService } from "../services/AccountService.js";
import { AuthService } from "../services/AuthService";
import { recipeService } from "../services/RecipesService.js";
import Pop from "../utils/Pop.js";
import Login from "./Login.vue";

export default {
  setup() {
    // onMounted(() => {
    //   focus();
    // });
    // function focus() {
    //   document.getElementById("homeBtn").focus();
    // }
    return {
      offset: computed(() => AppState.offset),
      number: computed(() => AppState.favNumber),
      async getFavorites() {
        try {
           if (!AppState.account?.id) {
            AuthService.loginWithPopup();
            return
          }
          AppState.favNumber = 1;
          AppState.offset = 0;
          AppState.recipes = [];
          await accountService.getFavoriteRecipes(AppState.offset);
        } catch (error) {
          Pop.error(error);
        }
      },
      async getAllRecipes() {
        try {
          AppState.favNumber = 0;
          AppState.offset = 0;
          AppState.recipes = [];
          await recipeService.getRecipes(AppState.offset);
        } catch (error) {
          Pop.error(error);
        }
      },
      async getMyRecipes() {
        try {
          if (!AppState.account?.id) {
            AuthService.loginWithPopup();
            return
          }
          AppState.favNumber = 2;
          AppState.offset = 0;
          AppState.recipes = [];
          await accountService.getMyRecipes(AppState.offset);
        } catch (error) {
          Pop.error(error);
        }
      },
    };
  },
  components: { Login },
};
</script>

<style lang="scss" scoped>
.img {
  width: 100vw;
  height: 25vh;
  object-fit: cover;
  background-image: url("https://plus.unsplash.com/premium_photo-1663126351065-741a1d338b5d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2832&q=80");
}

.overlay {
  bottom: -20px;
  right: 190px;
}

.bannerBtn {
  background: linear-gradient(to bottom right, #ffbb00, #ff5e00);
  border-radius: 0px;
  color: #ffffff;
  cursor: pointer;
  display: inline-block;
  font-family: "Baloo 2", cursive;
  font-size: 16px;
  font-weight: 600;
  letter-spacing: 0.07em;
  line-height: 2.5;
  outline: transparent;
  text-align: center;
  text-decoration: none;
  transition: all 0.25s ease-in-out;
  user-select: none;
  -webkit-user-select: none;
  touch-action: manipulation;
  white-space: nowrap;
  box-shadow: rgba(0, 0, 0, 0.07) 0px 1px 1px, rgba(0, 0, 0, 0.07) 0px 2px 2px,
    rgba(0, 0, 0, 0.07) 0px 4px 4px, rgba(0, 0, 0, 0.07) 0px 8px 8px,
    rgba(0, 0, 0, 0.07) 0px 16px 16px;
}

@media screen AND (max-width: 700px) {
  .overlay {
    bottom: -20px;
    right: 50px;
  }
}
</style>
