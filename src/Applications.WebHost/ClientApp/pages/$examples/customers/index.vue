<script lang="ts">
import Vue from 'vue';
import { AggregateQueryBuilder, FieldQueryBuilder, GroupQueryBuilder, PagingQueryBuilder, QueryBuilder, SortQueryBuilder } from '~/core/api';
import Store from '~/store/$examples/customers';
import { ApiCustomerSearchConditions, CustomerEntity, OneEntity } from '~/types/api';

export default Vue.extend({
  computed: {
    store() {
      return this.$accessor.$examples.customers;
    },
  },
  created() {
    console.log(this.store.customers);
    // const aggregateQueryBuilder0 = AggregateQueryBuilder.create<OneEntity>() //
    //   .add({ operator: 'Avg', field: 'id' })
    //   .add({ operator: 'Max', field: 'customerId' });
    // const aggregateQueryBuilder = AggregateQueryBuilder.create<CustomerEntity>() //
    //   .add({ operator: 'Avg', field: 'id' })
    //   .add({ operator: 'Max', field: 'id' })
    //   .add({ operator: 'Min', field: 'id' })
    //   .add({ operator: 'Sum', field: 'id' });
    // console.log(aggregateQueryBuilder.getQuery());
    // type aggregateResponseType = ReturnType<typeof aggregateQueryBuilder.getType>;
    // const fieldQueryBuilder0 = FieldQueryBuilder.create<CustomerEntity>() //
    //   .add(x => x.prop('id'))
    //   .add(x => x.prop('text'));
    // const fieldQueryBuilder1 = FieldQueryBuilder.create<CustomerEntity>() //
    //   .add(x => x.prop('*'));
    // const fieldQueryBuilder2 = FieldQueryBuilder.create<CustomerEntity>() //
    //   .add(x => x.prop('one').prop('customerId'));
    // const fieldQueryBuilder3 = FieldQueryBuilder.create<CustomerEntity>() //
    //   .add(x => x.prop('many').prop('customerId'));
    // const fieldQueryBuilder = FieldQueryBuilder.create<CustomerEntity>() //
    //   .add(x => x.prop('*'))
    //   .add(x => x.prop('one').prop('*'))
    //   .add(x => x.prop('many').prop('*'));
    // console.log(fieldQueryBuilder.getQuery());
    // type fieldResponseType = ReturnType<typeof fieldQueryBuilder.getType>;
    // const groupQueryBuilder = GroupQueryBuilder.create<CustomerEntity>() //
    //   .add('id')
    //   .add('text');
    // console.log(groupQueryBuilder.getQuery());
    // type groupResponseType = ReturnType<typeof groupQueryBuilder.getType>;
    // const pagingQueryBuilder = PagingQueryBuilder.create({ offset: 1, limit: 100 });
    // console.log(pagingQueryBuilder.getQuery());
    // const sortQueryBuilder = SortQueryBuilder.create<CustomerEntity>() //
    //   .add(x => x.prop('id'))
    //   .add(x => x.prop('one').prop('customerId'));
    // this.$axios.get<fieldResponseType[]>('', {
    //   params: {
    //     ...sortQueryBuilder.getQuery(),
    //     ...pagingQueryBuilder.getQuery(),
    //     ...fieldQueryBuilder.getQuery(),
    //   },
    // });
    // this.$axios.get<(ReturnType<typeof groupQueryBuilder.getType> & ReturnType<typeof aggregateQueryBuilder.getType>)[]>('', {
    //   params: {
    //     ...groupQueryBuilder.getQuery(),
    //     ...aggregateQueryBuilder.getQuery(),
    //   },
    // });
    // const queryBuilder = QueryBuilder.create<ApiCustomerSearchConditions>() //
    //   .add({
    //     key: 'id',
    //     operator: 'OrElse',
    //     comparisons: [
    //       { operator: 'Equals', value: 1 }, //
    //       { operator: 'IsNull' },
    //     ],
    //   })
    //   .add({
    //     key: 'text',
    //     operator: 'AndAlso',
    //     comparisons: [
    //       { operator: 'Equals', value: 'xs' }, //
    //     ],
    //   })
    //   .build();
  },
});
</script>

<template>
  <div>
    <v-row>
      <v-col>
        <v-form autocomplete="off" @submit.prevent>
          <v-row dense>
            <v-col>
              aa
            </v-col>
          </v-row>
        </v-form>
      </v-col>
    </v-row>
  </div>
</template>
