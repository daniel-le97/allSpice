import{_ as C,d as h,e as y,f as B,A as n,i as k,P as c,B as F,R as S,C as I,c as a,a as r,g as i,F as _,r as b,h as g,j as d,o as l,n as x,k as f}from"./index.2bb5889a.js";const A={setup(){async function p(){try{let e=n.offset;await g.getRecipes(e)}catch(e){c.error(e)}}async function u(){try{await d.getMyFavorites()}catch(e){c.error(e)}}h(()=>{p(),o()}),y(()=>{u()});async function m(){let e=n.favNumber,t=n.offset;e==0&&await g.getRecipes(t),e==1&&await d.getFavoriteRecipes(t),e==2&&await d.getMyRecipes(t)}function o(){window.onscroll=e=>{document.documentElement.scrollTop+window.innerHeight===document.documentElement.offsetHeight&&m()}}return{recipes:B(()=>n.recipes),async getIngredients(e){try{n.activeRecipeIngredients=[],await k.getIngredients(e)}catch(t){c.error(t)}}}},components:{Banner:F,RecipeCard:S,CreateRecipeButton:I}},E={class:"container-fluid"},H={class:"row justify-content-center"},M={class:"col-md-10"},N={class:"row mt-4"};function P(p,u,m,o,e,t){const v=f("Banner"),w=f("RecipeCard"),R=f("CreateRecipeButton");return l(),a(_,null,[r("div",E,[r("div",H,[r("div",M,[i(v)])]),r("div",N,[(l(!0),a(_,null,b(o.recipes,s=>(l(),a("div",{class:x(["col-md-3",s?"":"skeleton-loader"]),ref_for:!0,ref:"scrollComponent",key:s.id},[i(w,{recipe:s,class:"m-3",onClick:$=>o.getIngredients(s.id)},null,8,["recipe","onClick"])],2))),128))])]),i(R)],64)}const L=C(A,[["render",P]]);export{L as default};
