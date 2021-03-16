import { PickPrimitive } from '~/core/types';

const QUERY_KEY = '$group';

export class GroupQueryBuilder<T, TResult> {
  private readonly fields: string[];

  private constructor() {
    this.fields = [];
  }

  public static create<T>() {
    return new GroupQueryBuilder<T, unknown>();
  }

  public add<TKey extends keyof PickPrimitive<T>>(field: TKey) {
    this.fields.push(`${field}`);
    type ResultType = TResult & Pick<T, TKey>;
    return (this as any) as GroupQueryBuilder<T, { [P in keyof ResultType]: ResultType[P] }>;
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
