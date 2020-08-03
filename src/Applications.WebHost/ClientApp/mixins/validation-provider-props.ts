import Vue from 'vue';

interface ValidationProviderProperties {
  customMessages: object;
  name: string;
  rules: string | object;
  slim: boolean;
  vid: string;
}

export default Vue.extend({
  props: {
    label: {
      default: undefined,
      type: String,
    },
    veeCustomMessages: {
      default: undefined,
      type: Object,
    },
    veeDisabledRules: {
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
    veeProviderProps(): ValidationProviderProperties {
      return {
        customMessages: this.veeCustomMessages,
        name: this.veeName ? this.veeName : this.label,
        rules: this.veeDisabledRules ? undefined : this.veeRules,
        slim: true,
        vid: this.veeVid,
      };
    },
  },
});
