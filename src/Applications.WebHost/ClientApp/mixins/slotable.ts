import { VueBuilder } from '~/core/vue';

const Vue = VueBuilder.create() //
  .build();

export const Slotable = Vue.extend({
  computed: {
    scopedSlotKeys() {
      const slotKeys: string[] = this.slotKeys;
      return Object.keys(this.$scopedSlots).filter(key => !slotKeys.includes(key));
    },
    slotKeys() {
      return Object.keys(this.$slots);
    },
  },
});
