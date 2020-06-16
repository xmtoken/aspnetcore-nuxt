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
    <v-snackbar color="success" left :timeout="0" top :value="$store.state.versioning.updatable" :vertical="$device.isMobile">
      アプリケーションの新しいバージョンがリリースされています。
      <v-btn :class="{ 'mt-0': $device.isMobile }" :disabled="loading" :loading="loading" text @click="reload">
        更新する
      </v-btn>
    </v-snackbar>
  </v-slide-x-transition>
</template>
