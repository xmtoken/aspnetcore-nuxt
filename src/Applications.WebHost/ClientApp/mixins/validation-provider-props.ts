import Vue, { PropType } from 'vue';

type ValidationProviderProperties = {
  customMessages: object;
  disabled: boolean;
  name: string;
  rules: string | object | null;
  slim: boolean;
  vid: string;
};

export default Vue.extend({
  props: {
    label: {
      default: undefined,
      type: String,
    },
    veeCustomMessages: {
      default: undefined,
      type: Object as PropType<object>,
    },
    veeDisabled: {
      default: false,
      type: Boolean,
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
      type: [Object, String] as PropType<string | object>,
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
        disabled: this.veeDisabled,
        name: this.veeName ? this.veeName : this.label,
        rules: this.veeDisabledRules ? null : this.veeRules,
        slim: true,
        vid: this.veeVid,
      };
    },
  },
});
