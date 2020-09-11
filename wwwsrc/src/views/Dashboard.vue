<template>
<div class="dashboard container-fluid">
    <div class="row">
      <div class="col-6 m2 border border-primary">
        <keepForm />
      </div>
      <div class="col-6 m2 border border-primary">
        <vaultForm />
    </div>

<div class="row">
  <vaultComponent v-for="vault in vaults" :key="vault.Id" :vault="vault" />
</div>
<div class="row">
  <keepComponent v-for="keep in userKeeps" :key="keep.Id" :keep="keep" />
</div>


    </div>
    </div>
</template>

<script>
import vaultComponent from "../components/Vault.vue"
import keepComponent from  "../components/Keep.vue"
import keepForm from "../components/KeepForm.vue";
import vaultForm from "../components/VaultForm.vue"
export default {
  name: "dashboard",

  mounted() {
this.$store.dispatch("getUserKeeps");
this.$store.dispatch("getVaults");
  },

  computed: {
     user() {
      return this.$store.state.user;
    },
    userKeeps() {
      return this.$store.state.userKeeps
    },
    vaults() {
      return this.$store.state.vaults
    }
  },

  methods: {},

  components: {
    keepForm,
    vaultForm,
    keepComponent,
    vaultComponent
  }
};
</script>

<style>

</style>
