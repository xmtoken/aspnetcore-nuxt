import { actionTree } from 'typed-vuex';
import { getters } from './getters';
import { mutations } from './mutations';
import { state } from './state';
import { FieldQueryBuilder, PagingQueryBuilder, QueryBuilder, SortQueryBuilder } from '~/core/api';
import { ApiCustomerSearchConditions, CustomerEntity } from '~/types/api';

export const actions = actionTree(
  { state, getters, mutations },
  {
    async fetchCustomers({ commit }, payload: { conditions: ApiCustomerSearchConditions }) {
      const fieldQueryBuilder = FieldQueryBuilder.create<CustomerEntity>() //
        .add(x => x.prop('*'))
        .add(x => x.prop('one').prop('*'))
        .add(x => x.prop('many').prop('*'));

      const queryBuilder = QueryBuilder.create<ApiCustomerSearchConditions>() //
        .add({
          key: 'id',
          operator: 'OrElse',
          comparisons: [
            { operator: 'Equals', value: 1 }, //
            { operator: 'IsNull' },
          ],
        })
        .add({
          key: 'text',
          operator: 'AndAlso',
          comparisons: [
            { operator: 'Equals', value: 'xs' }, //
          ],
        })
        .build();

      const sortQueryBuilder = SortQueryBuilder.create<CustomerEntity>() //
        .add(x => x.prop('id'))
        .add(x => x.prop('one').prop('customerId'));

      const pagingQueryBuilder = PagingQueryBuilder.create({ offset: 1, limit: 100 });

      type fieldResponseType = ReturnType<typeof fieldQueryBuilder.getType>;

      // const customers = await this.$axios.$get<CustomerEntity[]>('/$examples/customers', {
      //   params: {
      //     ...fieldQueryBuilder.getQuery(),
      //     ...sortQueryBuilder.getQuery(),
      //     ...pagingQueryBuilder.getQuery(),
      //   },
      // });

      // commit('setCustomers', { customers });

      commit('setCustomers', { customers: [CustomerEntity.fromJS({}), CustomerEntity.fromJS({})] });
    },
  }
);

export default actions;
