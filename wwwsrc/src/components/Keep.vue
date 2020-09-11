<template>
    <div class="col-4 my-2 border border-primary" @click="viewKeep(keep.id)">
        <img :src="keep.img">
        <h1>{{keep.name}}</h1>
        <h5>{{keep.description}}</h5>
        <button v-if="vk" @click="deleteVaultKeep()">Delete Vault from Keep</button>
    </div>
</template>
<script>
export default {
  name: "Keep",
  data() {
    return {};
  },
  mounted() {},

  computed: {},

  methods: {
      viewKeep(id) {this.$router.push({ name: 'KeepDetails', params: {keepId: id} })
      },

      deleteVaultKeep() {
        this.$store.state.dispatch("deleteVaultKeep", this.keep.VaultKeepId)
      }
  },
  props: ["keep", "vk"],
}
</script>
