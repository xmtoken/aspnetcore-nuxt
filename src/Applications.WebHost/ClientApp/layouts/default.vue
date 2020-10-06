<script lang="ts">
import Vue from 'vue';
import * as Responsive from '~/extensions/responsive';

export default Vue.extend({
  methods: {
    onResize(): void {
      const elements = Responsive.getResponsiveElements();
      if (elements.length) {
        Responsive.setResponsiveMaxHeight(elements);
      }
    },
  },
});
</script>

<template>
  <v-app v-resize="onResize" class="noto-sans-jp">
    <v-app-bar id="app-bar" app :elevation="0" height="64">
      <v-toolbar-title>
        Nuxt.js
      </v-toolbar-title>
    </v-app-bar>
    <app-navigation-drawer />
    <v-main>
      <v-container id="container" fluid>
        <nuxt />
      </v-container>
    </v-main>
    <app-notification :notifications="$store.state.notification.values" />
    <app-versioning :value="$store.state.versioning.updatable" />
  </v-app>
</template>
