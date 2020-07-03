<script>
export default {
  data() {
    return {
      loading: false,
    };
  },
  methods: {
    reload() {
      this.loading = true;
      location.reload(true);
    },
  },
};
</script>

<template>
  <v-slide-x-transition>
    <v-snackbar color="success" left :timeout="-1" top :value="$store.state.versioning.updatable">
      アプリケーションの新しいバージョンがリリースされています。
      <v-btn :disabled="loading" :loading="loading" text @click="reload">
        更新する
      </v-btn>
    </v-snackbar>
  </v-slide-x-transition>
</template>
