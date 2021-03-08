<script lang="ts">
import Vue from 'vue';
import { FieldBuilder, QueryBuilder } from '~/core/api';
import { CustomerEntity, CustomerSearchConditions } from '~/types/api';

type Hoge = {
  tex: string;
  textnull: string | null;
  one: Hoge;
  onenull: Hoge | null;
  many: Hoge[];
  manynull: Hoge[] | null;
};

const hhh = FieldBuilder.create<Hoge>() //
  .add(x => x.prop('tex'))
  .add(x => x.prop('textnull'))
  .add(x => x.prop('one').prop('tex'))
  .add(x => x.prop('one').prop('one').prop('tex'))
  .add(x => x.prop('many').prop('tex'))
  .add(x => x.prop('many').prop('many').prop('tex'));

type ResponseType = ReturnType<typeof hhh.getType>;

const fieldBuilder = FieldBuilder.create<CustomerEntity>() //
  .add(x => x.prop('one').prop('text'))
  .add(x => x.prop('many').prop('text'))
  .add(x => x.prop('id'));

const queryBuilder = QueryBuilder.create<CustomerSearchConditions>() //
  .add({ key: 'int32', comparisons: [{ operator: 'Equals', value: 1 }, { operator: 'IsNull' }] })
  .add({ key: 'string', comparisons: [{ operator: 'Equals', value: 'xs' }], operator: 'AndAlso' })
  .build();

export default Vue.extend({
  methods: {
    //
  },
});
</script>

<template>
  <div />
</template>
