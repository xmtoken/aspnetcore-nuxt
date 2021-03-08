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

class ApiField {
  public static build<T>() {
    return new ApiFieldBuilder<T, {}>();
  }
}

class ApiFieldBuilder<T, TResult> {
  private readonly fields: string[];

  constructor() {
    this.fields = [];
  }

  public field<TField>(selector: (_: ApiFieldSelectorkcer<T, {}>) => ApiFieldSelector<TField>) {
    const field = selector(new ApiFieldSelectorkcer()).field;
    this.fields.push(field);
    return (this as any) as ApiFieldBuilder<T, TResult & TField>;
  }

  public getType(): TResult {
    return this as any;
  }

  public getFields(): string[] {
    return this.fields;
  }
}

class ApiFieldSelector<T> {
  public readonly field: string;

  constructor(field: string) {
    this.field = field;
  }

  private _() {
    return new ApiFieldSelector<T>(this.field);
  }
}

type InferArray<T> = T extends Array<infer U> ? U : T;
type InferObj<T, Key extends keyof InferArray<T>> = T extends Array<infer U> ? { [_ in Key]: U[Key] }[] : { [_ in Key]: T[Key] };

class ApiFieldSelectorkcer<T, TResult> {
  public prop<TKey1 extends keyof T>(field: TKey1) {
    type inner = T[TKey1] extends Array<infer U> ? {}[] : T[TKey1] extends Record<string, any> ? {} : T[TKey1];
    type ResultType = { [_ in TKey1]: inner };
    return new ApiFieldSelectorkcer1<T, TKey1, ResultType>(`${field}`);
  }
}

class ApiFieldSelectorkcer1<T, TKey1 extends keyof T, TResult> extends ApiFieldSelector<TResult> {
  // public prop<TKey2 extends keyof T[TKey1]>(field: TKey2) {
  //   type ResultType = { [_ in TKey1]: { [_ in TKey2]: T[TKey1][TKey2] } };
  //   return new ApiFieldSelectorkcer2<T, TKey1, TKey2, ResultType>(`${this.field}.${field}`);
  // }

  public prop<TKey2 extends keyof InferArray<T[TKey1]>>(field: TKey2) {
    type ResultType = { [_ in TKey1]: InferObj<T[TKey1], TKey2> };
    return new ApiFieldSelectorkcer2<T, TKey1, TKey2, ResultType>(`${this.field}.${field}`);
  }
}

class ApiFieldSelectorkcer2<T, TKey1 extends keyof T, TKey2 extends keyof InferArray<T[TKey1]>, TResult> extends ApiFieldSelector<TResult> {
  //
}

const builder = ApiField.build<Source>() //
  .field(x => x.prop('obj').prop('objnum'))
  .field(x => x.prop('obj').prop('objnum'))
  .field(x => x.prop('arr').prop('arrnum'));

const typed = builder.getType();

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
