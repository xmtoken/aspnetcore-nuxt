import { VueBuilder } from '~/core/vue';

const Vue = VueBuilder.create() //
  .build();

export const LazyRenderable = Vue.extend({
  props: {
    lazyRendering: {
      default: false,
      type: Boolean,
    },
  },
  data() {
    return {
      isLazing: true,
    };
  },
});
