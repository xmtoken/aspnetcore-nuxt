<script lang="ts">
import Vue from 'vue';

export default Vue.extend({});
//
</script>

<template>
  <v-app class="noto-sans-jp">
    <v-app-bar id="app-bar" app elevate-on-scroll height="64">
      <v-toolbar-title>
        Nuxt.js
      </v-toolbar-title>
    </v-app-bar>
    <app-navigation-drawer />
    <v-main style="max-height: 100vh;">
      <v-container id="container" class="align-start fill-height" fluid>
        <v-form autocomplete="off" @submit.prevent>
          <nuxt />
        </v-form>
      </v-container>
    </v-main>
    <app-notification :notifications="$store.state.notification.values" />
    <app-versioning :value="$store.state.versioning.updatable" />
  </v-app>
</template>
