import{_ as C,d as h,e as B,A as n,M as f,i as y,P as m,B as k,R as I,C as M,c,a as s,f as r,F as u,r as x,g as _,h as g,o as a,n as S,j as i}from"./index.4bfbdb07.js";const O={setup(){async function l(){try{let e=n.offset;await _.getRecipes(e)}catch(e){m.error(e)}}h(()=>{l(),p()});async function d(){let e=n.favNumber,t=n.offset;e==0&&await _.getRecipes(t),e==1&&await g.getFavoriteRecipes(t),e==2&&await g.getMyRecipes(t)}function p(){window.onscroll=e=>{document.documentElement.scrollTop+window.innerHeight===document.documentElement.offsetHeight&&d()}}return{recipes:B(()=>n.recipes),async getIngredients(e){try{if(n.activeRecipe==e){f.getOrCreateInstance("#exampleModal").show();return}n.activeRecipe=e,f.getOrCreateInstance("#exampleModal").show(),n.activeRecipeIngredients=[],await y.getIngredients(e.id)}catch(t){m.error(t)}}}},components:{Banner:k,RecipeCard:I,CreateRecipeButton:M}},b={class:"container-fluid"},E={class:"row justify-content-center"},F={class:"col-md-10"},H={class:"row mt-4"};function N(l,d,p,e,t,P){const R=i("Banner"),v=i("RecipeCard"),w=i("CreateRecipeButton");return a(),c(u,null,[s("div",b,[s("div",E,[s("div",F,[r(R)])]),s("div",H,[(a(!0),c(u,null,x(e.recipes,o=>(a(),c("div",{class:S(["col-md-3",o?"":"skeleton-loader"]),ref_for:!0,ref:"scrollComponent",key:o.id},[r(v,{recipe:o,class:"m-3",onClick:$=>e.getIngredients(o)},null,8,["recipe","onClick"])],2))),128))])]),r(w)],64)}const A=C(O,[["render",N]]);export{A as default};
