<script lang="ts">
import Vue from 'vue';
import { AggregateQueryBuilder, FieldQueryBuilder, GroupQueryBuilder, PagingQueryBuilder, SortQueryBuilder } from '~/core/api';
import { OmitArray, OmitFunction, OmitObject, OmitString } from '~/core/types';
import { CustomerEntity, CustomerSearchConditions, OneEntity } from '~/types/api';

export default Vue.extend({
  created() {
    type omitArray = OmitArray<CustomerEntity>;
    type omitFunction = OmitFunction<CustomerEntity>;
    type omitObject = OmitObject<CustomerEntity>;
    type omitString = OmitString<CustomerEntity>;

    const aggregateQueryBuilder0 = AggregateQueryBuilder.create<OneEntity>() //
      .add({ operator: 'Avg', field: 'id' })
      .add({ operator: 'Max', field: 'customerId' });

    const aggregateQueryBuilder = AggregateQueryBuilder.create<CustomerEntity>() //
      .add({ operator: 'Avg', field: 'id' })
      .add({ operator: 'Max', field: 'id' })
      .add({ operator: 'Min', field: 'id' })
      .add({ operator: 'Sum', field: 'id' });
    console.log(aggregateQueryBuilder.getQuery());
    type aggregateResponseType = ReturnType<typeof aggregateQueryBuilder.getType>;

    const fieldQueryBuilder0 = FieldQueryBuilder.create<CustomerEntity>() //
      .add(x => x.prop('id'))
      .add(x => x.prop('id'));

    const fieldQueryBuilder1 = FieldQueryBuilder.create<CustomerEntity>() //
      .add(x => x.prop('*'));

    const fieldQueryBuilder2 = FieldQueryBuilder.create<CustomerEntity>() //
      .add(x => x.prop('one').prop('*'));

    const fieldQueryBuilder = FieldQueryBuilder.create<CustomerEntity>() //
      .add(x => x.prop('*'))
      .add(x => x.prop('one').prop('*'))
      .add(x => x.prop('many').prop('*'));

    // const fieldQueryBuilder = FieldQueryBuilder.create<CustomerEntity>() //
    //   .add(x => x.prop('text'))
    //   .add(x => x.prop('*'))
    //   .add(x => x.prop('id'))
    //   .add(x => x.prop('one').prop('id'))
    //   .add(x => x.prop('many').prop('text'));
    console.log(fieldQueryBuilder.getQuery());
    type fieldResponseType = ReturnType<typeof fieldQueryBuilder.getType>;

    const groupQueryBuilder = GroupQueryBuilder.create<CustomerEntity>() //
      .add('id')
      .add('text');
    console.log(groupQueryBuilder.getQuery());
    type groupResponseType = ReturnType<typeof groupQueryBuilder.getType>;

    const pagingQueryBuilder = PagingQueryBuilder.create({ offset: 1, limit: 100 });
    console.log(pagingQueryBuilder.getQuery());

    const sortQueryBuilder = SortQueryBuilder.create();

    // const queryBuilder = QueryBuilder.create<CustomerSearchConditions>() //
    //   .add({ key: 'id', comparisons: [{ operator: 'Equals', value: 1 }, { operator: 'IsNull' }] })
    //   .add({ key: 'text', comparisons: [{ operator: 'Equals', value: 'xs' }], operator: 'AndAlso' })
    //   .build();
  },
});
</script>

<template>
  <div />
</template>
