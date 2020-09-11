<template>
  <div class="home container-fluid">
    <h1>Keepr</h1>
    <div class="row">
      <keepComponent v-for="keep in publicKeeps" :key="keep.Id" :keep="keep" />
    </div>
  </div>
</template>

<script>
import keepComponent from  "../components/Keep.vue";
export default {
  name: "home",

  mounted() {
    this.$store.dispatch("getKeeps");
  },
  
  computed: {
    user() {
      return this.$store.state.user;
    },
    publicKeeps() {
      return this.$store.state.publicKeeps
    }

  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },

  components: {
    keepComponent
  }
};
</script>
