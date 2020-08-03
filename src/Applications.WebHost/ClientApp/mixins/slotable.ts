import Vue from 'vue';

export default Vue.extend({
  computed: {
    scopedSlotKeys(): string[] {
      return Object.keys(this.$scopedSlots).filter(key => !this.slotKeys.includes(key));
    },
    slotKeys(): string[] {
      return Object.keys(this.$slots);
    },
  },
});
