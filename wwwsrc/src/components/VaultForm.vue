<template>
  <div class="vaultForm">
    <form @submit.prevent="postVault">
      <div class="form-group">
        <label for="make">Title</label>
        <input
          v-model="newVault.name"
          type="text"
          name="title"
          class="form-control"
          placeholder="Vault title"
          required
        />
      </div>
      <div class="form-group">
        <label for="model">Description</label>
        <input
          v-model="newVault.description"
          type="text"
          name="description"
          class="form-control"
          placeholder="Vault description"
          required
        />
      </div>

      <button type="submit" class="btn btn-primary">Add Keep</button>
    </form>
  </div>
</template>

<script>
export default {
  name: "vaultForm",
  data() {
    return {
      newVault: {},
    };
  },
  methods: {
    postVault() {
      this.$store.dispatch("postVault", { ...this.newVault });
      this.newVault = {};
    },
  },
};
</script>

<style>
</style>