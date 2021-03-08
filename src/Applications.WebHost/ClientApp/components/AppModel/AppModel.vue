<script lang="ts">
import Vue, { PropType } from 'vue';

export default Vue.extend({
  inheritAttrs: false,
  model: {
    event: 'input',
    prop: 'value',
  },
  props: {
    value: {
      default: undefined,
      type: (null as any) as PropType<any>,
    },
  },
  methods: {
    raise(): void {
      this.$emit('input', this.value);
    },
  },
});
</script>

<template>
  <div class="d-none">
    <!---->
  </div>
</template>
