import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "../router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    publicKeeps: [],
    privateKeeps:[],
    vaults: [],
    currentVault:{},
    currentKeep: {},
    userKeeps: [],
    vaultKeeps: []

  },

  mutations: {
    setKeeps(state, keeps) {
      state.publicKeeps = keeps;
    },
    setKeep(state, keep) {
      state.currentKeep = keep;
    },
    setVaults(state, vaults) {
      state.vaults = vaults;
    },
    setVault(state, vault) {
      state.currentVault = vault;
    },
    setUserKeeps(state, keeps) {
      state.userKeeps = keeps;
    },
    setVaultKeeps(state, vaultKeeps) {
      state.vaultKeeps = vaultKeeps;
    }
  },

  actions: {
    /// auth \\\
    setBearer({}, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },

    /// Keeps \\\

    async getUserKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps/user");
        console.log(res.data);
        commit("setUserKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },

    async getKeeps( {commit, dispatch} ) {
      try {
        let res = await api.get("keeps");
        commit("setKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },

    async getCurrentKeep( {commit, dispatch}, keepId) {
      try {
        let res = await api.get("keeps/" + keepId);
        commit("setKeep", res.data);
      } catch (error) {
        console.error(error);
      }
    },

    async postKeep( { commit, dispatch}, newKeep) {
      try {
        await api.post("keeps", newKeep);
        dispatch("getUserKeeps");
      } catch (error) {
        console.error(error);
      }
    },

    async editKeep( {commit, dispatch}, editKeep) {
      try {
        
      } catch (error) {
        
      }
    },

    async deleteKeep( {commit, dispatch}, keepId) {
      try {
        await api.delete("keeps/" + keepId);
        router.push({ name: "home" });
      } catch (error) {
        console.error(error);
      }
    },

    /// Vaults \\\

    async getVaults( {commit, dispatch} ) {
      try {
        let res = await api.get("vaults");
        commit("setVaults", res.data);
      } catch (error) {
        console.error(error);
      }
    },

    async getCurrentVault( {commit, dispatch}, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId);
        commit("setVault", res.data);
      } catch (error) {
        console.error(error);
      }
    },

    async postVault( {commit, dispatch}, newVault) {
      try {
        await api.post("vaults", newVault);
        dispatch("getVaults");
      } catch (error) {
        console.error(error);
      }
    },

    async editVault( {commit, dispatch}, editVault) {
      try {
        
      } catch (error) {
        
      }
    },

    async deleteVault ( {commit, dispatch}, vaultId) {
      try {
        await api.delete("vaults/" + vaultId)
        router.push({ name: "home" });
      } catch (error) {
        
      }
    },

    /// VaultKeep \\\

    async addKeepToVault({ commit, dispatch }, newVaultKeep) {
      let res = await api.post("vaultkeeps", newVaultKeep);
    },

    async getVaultKeeps( {commit, dispatch}, vaultId ) {
      try {
        let res = await api.get("vaults/" + vaultId + "/keeps");
        commit("setVaultKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },

    async deleteVaultKeeps( {commit, dispatch}, id) {
      try {
        let res = await api.delete("vaultkeeps/" + id);
        dispatch("getVaultKeeps", id)
      } catch (error) {
        
      }
    }
  }
});
