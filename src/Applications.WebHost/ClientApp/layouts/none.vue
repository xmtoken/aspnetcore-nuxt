<script>
import { AppVersioning } from '~/components';
export default {
  components: {
    AppVersioning,
  },
};
</script>

<template>
  <v-app class="noto-sans-jp">
    <nuxt />
    <app-versioning />
  </v-app>
</template>
