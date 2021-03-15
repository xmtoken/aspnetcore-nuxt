export class SortQueryBuilder<T, TResult> {
  private readonly fields: string[];

  private constructor() {
    this.fields = [];
  }

  public static create<T>() {
    return new SortQueryBuilder<T, unknown>();
  }

  public add<TKey>(predicate: (selector: FieldSelector<T, unknown>) => FieldSelector<any, TKey>) {
    const fields = predicate(new FieldSelector<NonNullable<T>, unknown>()).getFields();
    this.fields.push(fields);
    type ResultType = TResult & TKey;
    return (this as any) as SortQueryBuilder<T, { [P in keyof ResultType]: ResultType[P] }>;
  }

  public getQuery() {
    return {
      $fields: this.fields.join(','),
    };
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

  public prop<TKey extends keyof OmitFunction<T>>(key: TKey | '*') {
    type ComponentType = NonNullable<T[TKey]> extends Array<infer TArray> ? TArray : NonNullable<T[TKey]>;
    type ValueType = NonNullable<T[TKey]> extends any[] //
      ? unknown[]
      : NonNullable<T[TKey]> extends Record<string, any>
      ? unknown
      : T[TKey];
    this.fields.push(`${key}`);
    return (this as any) as FieldSelector<ComponentType, Xxx<TResult, TKey, ValueType>>;
  }

  public getFields() {
    return this.fields.join('.');
  }
}
