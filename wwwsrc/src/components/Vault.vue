<template>
    <div class="col-4 my-2 border border-primary" @click="viewVault(vault.id)">
      <h5>{{vault.name}}</h5>
      <h5>{{vault.description}}</h5>
    </div>
</template>


<script>
export default {
  name: "vault",
  props: ["vault"],
  data() {
    return {};
  },
  computed: {
  },
  methods: {
    viewVault(id) {
      this.$router.push({ name: "VaultDetails", params: { vaultId: id } });
      console.log(id);
    },
  },
  components: {},
};
</script>


<style scoped>
</style>