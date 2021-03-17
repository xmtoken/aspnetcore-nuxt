import { mutationTree } from 'typed-vuex';
import { state } from './state';
import { CustomerEntity } from '~/types/api';

export const mutations = mutationTree(state, {
  setCustomers(state, payload: { customers: CustomerEntity[] }) {
    state.customers = payload.customers;
  },
});

export default mutations;
