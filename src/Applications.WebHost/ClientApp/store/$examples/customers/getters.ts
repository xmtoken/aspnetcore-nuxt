import { getterTree } from 'typed-vuex';
import { state } from './state';

export const getters = getterTree(state, {
  customers: state => state.customers,
});

export default getters;
