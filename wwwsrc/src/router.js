import Vue from "vue";
import Router from "vue-router";
// @ts-ignore
import Home from "./views/Home.vue";
// @ts-ignore
import Dashboard from "./views/Dashboard.vue";
import { authGuard } from "@bcwdev/auth0-vue";
//@ts-ignore
import KeepDetails from "./views/KeepDetails.vue"
//@ts-ignore
import VaultDetails from "./views/VaultDetails.vue"

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/dashboard",
      name: "dashboard",
      component: Dashboard,
      beforeEnter: authGuard
    },
    {
      path: "/keep/:keepId",
      name: "KeepDetails",
      component: KeepDetails
    },
    {path: "/vault/:vaultId",
      name: "VaultDetails",
      component: VaultDetails,
      beforeEnter: authGuard
  }
  ]
});
