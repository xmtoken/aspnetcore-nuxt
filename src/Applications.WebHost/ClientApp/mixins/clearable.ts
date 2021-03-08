import { VueBuilder } from '~/core/vue';

export type ClearableProps = {
  clearable?: boolean;
};

const Vue = VueBuilder.create() //
  .$attrs<ClearableProps>()
  .build();

export const Clearable = Vue.extend({
  //
});
