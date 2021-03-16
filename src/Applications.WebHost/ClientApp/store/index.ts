import { getAccessorType } from 'typed-vuex';
import * as customers from '~/store/$examples/customers';

export const state = () => ({});

export type RootState = ReturnType<typeof state>;

export const accessorType = getAccessorType({
  state,
  // getters,
  // mutations,
  // actions,
  modules: {
    $examples: {
      modules: {
        customers,
      },
    },
  },
});
