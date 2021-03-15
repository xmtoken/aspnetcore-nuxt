import { OmitObject } from '~/core/types';

const QUERY_KEY = '$aggregate';

type Predicate<TKey> = {
  operator: 'Avg' | 'Max' | 'Min' | 'Sum';
  field: TKey;
};

export class AggregateQueryBuilder<T, TResult> {
  private readonly fields: string[];

  private constructor() {
    this.fields = [];
  }

  public static create<T>() {
    return new AggregateQueryBuilder<T, unknown>();
  }

  public add<TKey extends keyof OmitObject<T>>(predicate: Predicate<TKey>) {
    const field = `${predicate.operator.toLowerCase()}:${predicate.field}`;
    this.fields.push(field);
    type ResultType = TResult & Pick<T, TKey>;
    return (this as any) as AggregateQueryBuilder<T, { [P in keyof ResultType]: ResultType[P] }>;
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
