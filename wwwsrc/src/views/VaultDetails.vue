<template>
  <div class="currentVault col-4 my-2 border border-primary">
          <h1>{{currentVault.name}}</h1>
      <h5>{{currentVault.description}}</h5>
      <keepComponent v-for="keep in vaultKeeps" :key="keep.Id" :keep="keep" :vk="true"/>
         <button @click="deleteVault(currentVault.id)" class="btn btn-info">Delete Vault</button>


  </div>
</template>


<script>
import keepComponent from  "../components/Keep.vue";
export default {
  name: 'vaultDetails',
  data(){
    return {}
  },

   mounted() {
    this.$store.dispatch("getCurrentVault", this.$route.params.vaultId);
    
    this.$store.dispatch("getVaultKeeps", this.$route.params.vaultId)
   },

  computed:{
    currentVault() {
      return this.$store.state.currentVault;
    },

    vaultKeeps() {
      return this.$store.state.vaultKeeps;
    }
    },

  methods:{
     deleteVault(id) {
      this.$store.dispatch("deleteVault", id);
    },
  },

  components:{
keepComponent
  }
}
</script>


<style scoped>

</style>