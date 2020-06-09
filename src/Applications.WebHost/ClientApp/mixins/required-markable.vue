<script>
export default {
  inheritAttrs: false,
  props: {
    hideRequiredMarker: {
      default: false,
      type: Boolean,
    },
  },
  data() {
    return {
      isRequired: false,
    };
  },
  computed: {
    /** @returns {Object} */
    requiredClasses() {
      return {
        required: this.isRequired && !this.hideRequiredMarker,
      };
    },
  },
  mounted() {
    this.isRequired = this.$refs.provider.isRequired;
    this.$watch(
      () => this.$refs.provider.isRequired,
      val => (this.isRequired = val)
    );
  },
};
</script>

<style lang="scss" scoped>
.required ::v-deep {
  .v-label::after {
    color: #f00;
    content: '*';
  }
}
</style>
