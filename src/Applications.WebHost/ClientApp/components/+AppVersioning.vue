<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  inheritAttrs: false,
  props: {
    value: {
      default: false,
      type: Boolean,
    },
  },
  data() {
    return {
      loading: false,
    };
  },
  methods: {
    reload(): void {
      this.loading = true;
      location.reload(true);
    },
  },
});
</script>

<template>
  <v-snackbar class="pt-0" color="info" content-class="align-center d-flex" right :timeout="-1" top transition="slide-x-reverse-transition" :value="value">
    <span>アプリケーションの新しいバージョンがリリースされています。</span>
    <v-btn :disabled="loading" :loading="loading" text @click="reload">
      更新する
    </v-btn>
  </v-snackbar>
</template>
