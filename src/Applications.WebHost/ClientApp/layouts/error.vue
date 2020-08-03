<script lang="ts">
import Vue, { PropType } from 'vue';

export default Vue.extend({
  props: {
    error: {
      default: undefined,
      type: Object as PropType<{ statusCode: number; message: string }>,
    },
  },
});
</script>

<template>
  <div>
    <h1 class="display-4 font-weight-medium my-5 primary--text">
      {{ error.statusCode }}
    </h1>
    <h5 class="title">
      {{ error.message }}
    </h5>
  </div>
</template>

<style lang="scss" scoped>
div ::v-deep {
  height: 100%;
  left: 0;
  position: absolute;
  text-align: center;
  top: 0;
  width: 100%;
}
</style>
