<script>
export default {
  props: {
    label: {
      default: undefined,
      type: String,
    },
    veeCustomMessages: {
      default: undefined,
      type: Object,
    },
    veeDisabled: {
      default: false,
      type: Boolean,
    },
    veeName: {
      default: undefined,
      type: String,
    },
    veeRules: {
      default: undefined,
      type: [Object, String],
    },
    veeVid: {
      default: undefined,
      type: String,
    },
  },
  computed: {
    /** @returns {Object} */
    veeValidationProps() {
      return {
        customMessages: this.veeCustomMessages,
        disabled: this.veeDisabled,
        name: this.veeName ? this.veeName : this.label,
        rules: this.veeRules,
        slim: true,
        vid: this.veeVid,
      };
    },
  },
};
</script>
