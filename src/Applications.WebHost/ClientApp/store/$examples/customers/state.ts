import { CustomerEntity } from '~/types/api';

export const state = () => ({
  customers: [CustomerEntity.fromJS({})] as CustomerEntity[],
});

export default state;
