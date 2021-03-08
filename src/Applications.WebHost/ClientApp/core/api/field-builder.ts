type Entity = Record<string, any>;

type Xxx<T, TKey extends keyof any, TValue> = T extends Record<string, any>
  ? {
      [P in keyof T]: Xxx<T[P], TKey, TValue>;
    }
  : {
      [P in TKey]: TValue;
    };

export class FieldBuilder<T, TResult> {
  private constructor() {
    //
  }

  public static create<T>() {
    return new FieldBuilder<T, unknown>();
  }

  public add<TKey>(selector: (selector: FieldSelector<T, unknown>) => FieldSelector<any, TKey>) {
    // const field = selector(new FieldSelector<NonNullable<T>, unknown>()).getField();
    // this.fields.push(field);
    type ResultType = TResult & TKey;
    return (this as any) as FieldBuilder<T, { [P in keyof ResultType]: ResultType[P] }>;
  }

  public getType(): TResult {
    return this as any;
  }
}

class FieldSelector<T, TResult> {
  private readonly fields: string[];

  constructor() {
    this.fields = [];
  }

  public prop<TKey extends keyof T>(key: TKey) {
    type ComponentType = NonNullable<T[TKey]> extends Array<infer TArray> ? TArray : NonNullable<T[TKey]>;
    type ValueType = NonNullable<T[TKey]> extends any[] //
      ? unknown[]
      : NonNullable<T[TKey]> extends Record<string, any>
      ? unknown
      : T[TKey];
    this.fields.push(`${key}`);
    return (this as any) as FieldSelector<ComponentType, Xxx<TResult, TKey, ValueType>>;
  }
}
