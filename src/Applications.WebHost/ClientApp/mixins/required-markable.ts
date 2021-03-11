import { VueBuilder } from '~/core/vue';

export type RequiredMarkableProps = {
  disabledRequiredMarker?: boolean;
};

const Vue = VueBuilder.create() //
  .build();

export const RequiredMarkable = Vue.extend({
  props: {
    disabledRequiredMarker: {
      default: false,
      type: Boolean,
    },
  },
});
