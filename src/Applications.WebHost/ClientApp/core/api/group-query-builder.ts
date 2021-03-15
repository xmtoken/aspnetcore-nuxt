import { OmitFunction } from '~/core/types';

export class GroupQueryBuilder<T, TResult> {
  private readonly fields: string[];

  private constructor() {
    this.fields = [];
  }

  public static create<T>() {
    return new GroupQueryBuilder<T, unknown>();
  }

  public add<TKey extends keyof OmitFunction<T>>(key: TKey) {
    this.fields.push(`${key}`);
    type ResultType = TResult & TKey;
    return (this as any) as GroupQueryBuilder<T, { [P in keyof ResultType]: ResultType[P] }>;
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
