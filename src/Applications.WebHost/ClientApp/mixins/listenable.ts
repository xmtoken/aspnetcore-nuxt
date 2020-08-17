import Vue from 'vue';
import { Listeners } from '~/types/vue';

export default Vue.extend({
  methods: {
    withEmit(listeners: Listeners): Listeners {
      const result: Listeners = {};
      for (const [key, value] of Object.entries(listeners)) {
        const listener = Array.isArray(value) ? [...value] : value ? [value] : [];
        listener.push((val: any) => this.$emit(key, val));
        result[key] = listener;
      }
      return result;
    },
  },
});
