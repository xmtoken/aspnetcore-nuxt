<script>
export default {
  props: {
    error: {
      default: null,
      type: Object,
    },
  },
};
</script>

<template>
  <v-content>
    <v-container fluid>
      <v-row>
        <v-col class="text-center">
          <h1 class="display-4 font-weight-medium my-5 primary--text">
            {{ error.statusCode }}
          </h1>
          <h4 class="title">
            {{ error.message }}
          </h4>
        </v-col>
      </v-row>
    </v-container>
  </v-content>
</template>

<style lang="scss" scoped>
.v-content ::v-deep {
  height: 100%;
  left: 0;
  position: absolute;
  top: 0;
  width: 100%;
}
</style>
