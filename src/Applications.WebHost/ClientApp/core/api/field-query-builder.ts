import { Expand, OmitFunction, OmitObject } from '~/core/types';

const QUERY_KEY = '$fields';

const WILDCARD = '*';

type Xxx2<T, TValue> = T extends Record<string, any> //
  ? {
      [P in keyof T]: Xxx2<T[P], TValue>;
    }
  : { [P in keyof TValue]: TValue[P] };

type Xxx<T, TKey extends keyof any, TValue> = T extends Record<string, any>
  ? {
      [P in keyof T]: Xxx<T[P], TKey, TValue>;
    }
  : {
      [P in TKey]: TValue;
    };

export class FieldQueryBuilder<T, TResult> {
  private readonly fields: string[];

  private constructor() {
    this.fields = [];
  }

  public static create<T>() {
    return new FieldQueryBuilder<T, unknown>();
  }

  public add<TKey>(predicate: (selector: FieldSelector<T, unknown>) => FieldSelector<any, TKey>) {
    const field = predicate(new FieldSelector<T, unknown>()).field;
    this.fields.push(field);
    type ResultType = TResult & TKey;
    return (this as any) as FieldQueryBuilder<T, { [P in keyof ResultType]: ResultType[P] }>;
  }

  public getQuery() {
    return {
      [QUERY_KEY]: this.fields.join(','),
    } as const;
  }

  public getType(): TResult {
    return null as any;
  }
}

class FieldSelector<T, TResult> {
  private readonly fields: string[];

  constructor() {
    this.fields = [];
  }

  get field() {
    return this.fields.join('.');
  }

  public prop(key: typeof WILDCARD): FieldSelector<never, Xxx2<TResult, OmitObject<T>>>;
  public prop<TKey extends keyof OmitFunction<T>>(
    key: TKey
  ): FieldSelector<
    //
    NonNullable<T[TKey]> extends Array<infer TArray> ? TArray : NonNullable<T[TKey]>,
    Xxx<TResult, TKey, NonNullable<T[TKey]> extends any[] ? unknown[] : NonNullable<T[TKey]> extends Record<string, any> ? unknown : T[TKey]>
  >;

  public prop(key: string) {
    this.fields.push(key);
    return this;
  }
}
