import Vue from 'vue';
import responsive from '~/extensions/responsive';

export default Vue.extend({
  mounted(): void {
    this.$nextTick(() => responsive());
  },
});
