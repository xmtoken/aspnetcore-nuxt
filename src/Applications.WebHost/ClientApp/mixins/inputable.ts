import { VueBuilder } from '~/core/vue';

export type InputableProps = {
  dense?: boolean;
  disabled?: boolean;
  hideDetails?: boolean | 'auto' | 'tooltip';
  label?: string;
  readonly?: boolean;
};

const Vue = VueBuilder.create() //
  .$attrs<InputableProps>()
  .build();

export const Inputable = Vue.extend({
  computed: {
    isEnabledTooltipMessage() {
      return this.attrs.hideDetails === 'tooltip';
    },
  },
});
