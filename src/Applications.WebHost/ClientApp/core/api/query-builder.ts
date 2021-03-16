import { OmitFunction } from '~/core/types';

type ComparisonOperator =
  | 'Equals' //
  | 'NotEquals'
  | 'GreaterThan'
  | 'GreaterThanOrEquals'
  | 'LessThan'
  | 'LessThanOrEquals'
  | 'Contains'
  | 'NotContains'
  | 'StartsWith'
  | 'NotStartsWith'
  | 'EndsWith'
  | 'NotEndsWith';

type IsNullComparisonOperator = 'IsNull';

type LogicalOperator =
  | 'AndAlso' //
  | 'OrElse';

type Comparison<T> = {
  operator: ComparisonOperator;
  value: T | null | undefined;
};

type IsNullComparison = {
  operator: IsNullComparisonOperator;
};

type QueryValue<T, TKey extends keyof T> = {
  key: TKey;
  operator?: LogicalOperator;
  comparisons: (Comparison<T[TKey]> | IsNullComparison)[];
};

export class QueryBuilder<T extends Record<string, any>> {
  private readonly values: QueryValue<T, any>[];

  private constructor() {
    this.values = [];
  }

  public static create<T>() {
    return new QueryBuilder<T>();
  }

  public add<TKey extends keyof OmitFunction<T>>(value: QueryValue<T, TKey>): QueryBuilder<Omit<T, TKey>> {
    this.values.push(value);
    return this;
  }

  public build() {
    const result: Record<string, any> = {};
    for (const value of this.values) {
      const comparisons = value.comparisons.filter(x => x.operator === 'IsNull' || (!!x.value && !!String(x.value).trim()));
      if (comparisons.length === 0) {
        continue;
      }

      const predicates = comparisons
        .map(comparison => {
          if (comparison.operator === 'IsNull') {
            return '$null';
          }
          const escaped = this.escape(String(comparison.value).trim());
          switch (comparison.operator) {
            case 'Equals':
              return `==${escaped}`;
            case 'NotEquals':
              return `!=${escaped}`;
            case 'GreaterThan':
              return `>${escaped}`;
            case 'GreaterThanOrEquals':
              return `>=${escaped}`;
            case 'LessThan':
              return `<${escaped}`;
            case 'LessThanOrEquals':
              return `<=${escaped}`;
            case 'Contains':
              return `==*${escaped}*`;
            case 'NotContains':
              return `!=*${escaped}*`;
            case 'StartsWith':
              return `==*${escaped}`;
            case 'NotStartsWith':
              return `!=*${escaped}`;
            case 'EndsWith':
              return `==${escaped}*`;
            case 'NotEndsWith':
              return `!=${escaped}*`;
          }
        })
        .map(x => `(${x})`);

      const separator = value.operator === 'AndAlso' ? '&' : '|';
      result[value.key] = predicates.join(separator);
    }
    return result;
  }

  private escape(val: string) {
    return val.replace(/(!|\$|&|\(|\)|\*|<|=|>|\|)/g, '\\$1');
  }
}
