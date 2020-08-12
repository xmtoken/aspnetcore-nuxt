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
  <v-snackbar color="success" content-class="align-center d-flex" left :timeout="-1" top transition="slide-x-transition" :value="value">
    <span>アプリケーションの新しいバージョンがリリースされています。</span>
    <v-btn :disabled="loading" :loading="loading" text @click="reload">
      更新する
    </v-btn>
  </v-snackbar>
</template>
