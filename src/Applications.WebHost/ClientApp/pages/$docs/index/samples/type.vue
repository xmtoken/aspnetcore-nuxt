<script lang="ts">
import Vue from 'vue';

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

class Api {
  public static setup<T>() {
    return new ApiPick<T, Pick<T, never>>();
  }
}

class ApiPick<T, TPick> {
  private fields: string[];

  constructor() {
    this.fields = [];
  }

  public field1<T1 extends keyof T>(
    field1: T1 //
  ): ApiPick<T, TPick & Pick<T, T1>> {
    this.fields.push(`${field1}`);
    return this as any;
  }

  public field2<T1 extends keyof T, T2 extends keyof T[T1]>(
    field1: T1, //
    field2: T2
  ): ApiPick<T, TPick & { [_ in T1]: Pick<T[T1], T2> }> {
    this.fields.push(`${field1}.${field2}`);
    return this as any;
  }

  public field3<T1 extends keyof T, T2 extends keyof T[T1], T3 extends keyof T[T1][T2]>(
    field1: T1, //
    field2: T2,
    field3: T3
  ): ApiPick<T, TPick & { [_ in T1]: { [_ in T2]: Pick<T[T1][T2], T3> } }> {
    this.fields.push(`${field1}.${field2}.${field3}`);
    return this as any;
  }

  public field2arr<T1 extends keyof T, T2 extends T[T1] extends (infer U)[] ? keyof U : keyof T[T1]>(
    field1: T1, //
    field2: T2
  ): ApiPick<T, TPick & (T[T1] extends (infer U)[] ? { [_ in T1]: Pick<U, T2>[] } : { [_ in T1]: Pick<T[T1], T2> })> {
    this.fields.push(`${field1}.${field2}`);
    return this as any;
  }

  public type(): TPick {
    return this as any;
  }

  public getFields(): string[] {
    return this.fields;
  }
}

const api = Api.setup<Source>() //
  .field2arr('arr', 'arrnum')
  .field2arr('obj', 'objnum');

const fields = api.getFields();

const typed = api.type();

type ResponseType = typeof typed;

const response: ResponseType = null as any;
response.obj.objstr = '';
response.obj.objnum = 1;
response.arr[0].arrstr = '';
response.arr[0].arrnum = 1;

export default Vue.extend({
  data() {
    console.log(fields);
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
