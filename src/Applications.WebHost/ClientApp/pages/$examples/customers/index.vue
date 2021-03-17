<script lang="ts">
import Vue from 'vue';
import { mapFields } from 'vuex-map-fields';
import AppTextDateField from '~/components/AppTextField/AppTextDateField.vue';
import { AggregateQueryBuilder, FieldQueryBuilder, GroupQueryBuilder, PagingQueryBuilder, QueryBuilder, SortQueryBuilder } from '~/core/api';
import { mapComputed } from '~/core/vuex';
import Store from '~/store/$examples/customers';
import { ApiCustomerSearchConditions, CustomerEntity, OneEntity } from '~/types/api';

export default Vue.extend({
  components: { AppTextDateField },
  async fetch() {
    //
    console.log('fetch');
    await this.store.fetchCustomers({ conditions: {} });
    this.customers = this.store.customers;
    console.log(this.customers);
  },
  data() {
    return {
      customers: [] as CustomerEntity[],
    };
  },
  computed: {
    store() {
      return this.$accessor.$examples.customers;
    },
    // xxxx: mapComputed('', null, ''),
  },
  created() {
    console.log('created');
    // console.log(this.store.customers);
    // console.log(this.store.getcustomers);
    // console.log(this.store.setCustomers);
    // console.log(this.store.fetchCustomers);
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
        <v-row dense>
          <v-col>
            <app-text-field v-model="customers[0].text" hide-details label="app-text-field" />
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </div>
</template>
