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
    veeSlim: {
      default: true,
      type: Boolean,
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
        name: this.veeName === undefined ? this.label : this.veeName,
        rules: this.veeRules,
        slim: this.veeSlim,
        vid: this.veeVid,
      };
    },
  },
};
</script>
