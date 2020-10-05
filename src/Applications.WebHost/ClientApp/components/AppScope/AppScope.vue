<script lang="ts">
import Vue, { VNode } from 'vue';

export default Vue.extend({
  inheritAttrs: false,
  render(): VNode {
    return this.$slots.default![0];
  },
});
</script>
