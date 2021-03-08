<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  data() {
    return {
      breadcrumbs: [],
    };
  },
  created() {
    console.log(this.$router.options.routes);
    let path = this.$route.path.endsWith('/') ? this.$route.path.replace(/\/[^/]*$/, '') : this.$route.path;
    const paths = this.$route.path.split('/').filter(x => x);
    while (path) {
      this.breadcrumbs.unshift({
        text: paths.pop(),
        href: path,
      });
      path = path.replace(/\/[^/]*$/, '');
    }
  },
});
</script>

<template>
  <div>
    xxxxxxx
    <v-breadcrumbs :items="breadcrumbs" />
    <nuxt-child />
  </div>
</template>
