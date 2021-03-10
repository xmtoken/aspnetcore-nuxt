import { VueBuilder } from '~/core/vue';

export type ClearableProxyProps = {
  clearable?: boolean;
};

const Vue = VueBuilder.create() //
  .$attrs<ClearableProxyProps>()
  .build();

export const Clearable = Vue.extend({
  //
});
