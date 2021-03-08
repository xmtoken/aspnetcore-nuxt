import { VueBuilder } from '~/core/vue';

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
