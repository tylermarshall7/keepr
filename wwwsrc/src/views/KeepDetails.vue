<template>
  <div class="currentKeep col-4 my-2 border border-primary">
    <img :src="currentKeep.img">
        <h1>{{currentKeep.name}}</h1>
        <h5>{{currentKeep.description}}</h5>
         <button @click="deleteKeep(currentKeep.id)" class="btn btn-info">Delete Keep</button>
         <div class="dropdown">
  <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
    Dropdown button
  </button>
  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
    <a v-for="vault in vaults" :key="vault.Id" class="dropdown-item" @click="addKeepToVault(currentKeep.id, vault.id)">{{vault.name}}</a>
  </div>
</div>


  </div>
</template>


<script>
export default {
  name: 'keepDetails',
  data(){
    return {}
  },

   mounted() {
    this.$store.dispatch("getCurrentKeep", this.$route.params.keepId);
    this.$store.dispatch("getVaults");
   },

  computed:{
    currentKeep() {
      return this.$store.state.currentKeep;
    },

        vaults() {
      return this.$store.state.vaults
    }


    

    },

  methods:{
     deleteKeep(id) {
      this.$store.dispatch("deleteKeep", id);
    },

    addKeepToVault(keepId, vaultId) {
      let newVaultKeep = {
        keepId, vaultId
      }
      this.$store.dispatch("addKeepToVault", newVaultKeep)
      
    }
  },

  components:{

  }
}
</script>


<style scoped>

</style>