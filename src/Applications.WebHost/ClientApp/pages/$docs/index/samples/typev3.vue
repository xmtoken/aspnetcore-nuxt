<script lang="ts">
import Vue from 'vue';
import TypeHelper from '~/core/type-helper';

type Source = {
  str: string;
  num: number;
  obj: Obj;
  arr: Arr[];
};

type Obj = {
  objstr: string;
  objnum: number;
};

type Arr = {
  arrstr: string;
  arrnum: number;
};

const builder = TypeHelper.build<Source>() //
  .field(x => x.prop('num'))
  .field(x => x.prop('obj').prop('objnum'))
  .field(x => x.prop('arr').prop('arrnum'));

const fields = builder.getFields();

type ResponseType = ReturnType<typeof builder.getType>;

const response: ResponseType = null as any;
// response.str = '';
// response.num = 1;
// response.obj.objstr = '';
// response.obj.objnum = 1;
// response.arr[0].arrstr = '';
// response.arr[0].arrnum = 1;

export default Vue.extend({
  data() {
    console.log('fields', fields);
    console.log(
      'fields',
      TypeHelper.nameof<Source>(x => x.prop('obj').prop('objnum'))
    );
    return {
      fields,
    };
  },
});
</script>

<template>
  <div>
    {{ fields }}
  </div>
</template>
