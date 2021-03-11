import { VueBuilder } from '~/core/vue';

type ClearableProxyProps = {
  clearable?: boolean;
};

export type ClearableProps = ClearableProxyProps & {
  //
};

const Vue = VueBuilder.create() //
  .$attrs<ClearableProxyProps>()
  .build();

export const Clearable = Vue.extend({
  //
});
