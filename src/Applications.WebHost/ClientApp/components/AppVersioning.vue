<script>
export default {
  data() {
    return {
      loading: false,
      value: false,
    };
  },
  mounted() {
    this.value = true;
  },
  methods: {
    reload() {
      this.loading = true;
      this.$router.push({ path: this.$route.path, query: this.$route.query });
    },
  },
};
</script>

<template>
  <v-slide-x-transition>
    <v-snackbar color="success" left :timeout="0" top :value="$store.state.versioning.updatable">
      アプリケーションの新しいバージョンがリリースされています。
      <v-btn :disabled="loading" :loading="loading" text @click="reload">
        更新する
      </v-btn>
    </v-snackbar>
  </v-slide-x-transition>
</template>
