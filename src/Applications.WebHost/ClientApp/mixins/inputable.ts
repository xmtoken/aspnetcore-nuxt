import { VueBuilder } from '~/core/vue';

export type InputableProxyProps = {
  dense?: boolean;
  disabled?: boolean;
  hideDetails?: boolean | 'auto' | 'tooltip';
  label?: string;
  readonly?: boolean;
};

const Vue = VueBuilder.create() //
  .$attrs<InputableProxyProps>()
  .build();

export const Inputable = Vue.extend({
  props: {
    denseX: {
      default: false,
      type: Boolean,
    },
  },
  computed: {
    isEnabledTooltipMessage(): boolean {
      return this.attrs.hideDetails === 'tooltip';
    },
  },
});
