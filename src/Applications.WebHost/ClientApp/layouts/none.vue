<script lang="ts">
import Vue from 'vue';

export default Vue.extend({});
</script>

<template>
  <v-app class="noto-sans-jp">
    <v-main>
      <nuxt />
    </v-main>
    <app-notifications :notifications="$store.state.notification.values" />
    <app-versioning />
  </v-app>
</template>
