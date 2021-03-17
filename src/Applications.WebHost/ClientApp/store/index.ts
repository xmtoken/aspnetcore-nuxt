import { getAccessorType } from 'typed-vuex';
import { $examples } from './$examples';

export const accessorType = getAccessorType({
  modules: {
    $examples,
  },
});
