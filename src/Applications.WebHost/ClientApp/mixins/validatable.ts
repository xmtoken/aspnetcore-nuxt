import { mdiAlertCircleOutline } from '@mdi/js';
import { PropType } from 'vue';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import { Inputable } from '~/mixins/inputable';

export type ValidatableProxyProps = {
  errorMessages?: string | string[];
};

const Vue = VueBuilder.create() //
  .$attrs<ValidatableProxyProps>()
  .mixin(Inputable)
  .build();

export const Validatable = Vue.extend({
  props: {
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
    veeMode: {
      default: undefined,
      type: [String, Function],
    },
    veeName: {
      default: undefined,
      type: String,
    },
    veeRules: {
      default: undefined,
      type: [String, Object] as PropType<string | object>,
    },
    veeVid: {
      default: undefined,
      type: String,
    },
  },
  computed: {
    validationErrorIcon() {
      return mdiAlertCircleOutline;
    },
    validationProviderProps() {
      const props: Record<string, any> = {
        customMessages: this.veeCustomMessages,
        disabled: this.veeDisabled,
        mode: this.veeMode,
        name: this.veeName ?? this.attrs.label,
        rules:
          VuePropHelper.toBoolean(this.attrs.disabled) || //
          VuePropHelper.toBoolean(this.attrs.readonly) ||
          this.veeDisabledRules
            ? undefined
            : this.veeRules,
        slim: true,
        vid: this.veeVid,
      };
      return props;
    },
  },
  methods: {
    mergeErrorMessages(errors: string[]) {
      const errorMessages = [...errors];
      if (this.attrs.errorMessages) {
        if (Array.isArray(this.attrs.errorMessages)) {
          errorMessages.push(...this.attrs.errorMessages);
        } else {
          errorMessages.push(this.attrs.errorMessages);
        }
      }
      return errorMessages;
    },
  },
});
