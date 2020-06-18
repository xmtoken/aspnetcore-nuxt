<script>
export default {
  inheritAttrs: false,
  computed: {
    /** @returns {String[]} */
    scopedSlotKeys() {
      return Object.keys(this.$scopedSlots).filter(key => !this.slotKeys.includes(key));
    },
    /** @returns {String[]} */
    slotKeys() {
      return Object.keys(this.$slots);
    },
  },
};
</script>
