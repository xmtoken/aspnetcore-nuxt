export default class TypeHelper {
  public static build<T>() {
    return new FieldBuilder<NonNullable<T>, {}>();
  }

  public static nameof<T>(selector: (selector: FieldSelector<T, unknown>) => FieldSelector<T, any>) {
    const builder = new FieldBuilder<T, {}>();
    builder.field(selector);
    return builder.getFields()[0];
  }
}

class FieldBuilder<T, TResult> {
  private readonly fields: string[];

  constructor() {
    this.fields = [];
  }

  public field<TField>(selector: (selector: FieldSelector<T, unknown>) => FieldSelector<NonNullable<T>, TField>) {
    const field = selector(new FieldSelector<NonNullable<T>, unknown>()).getField();
    this.fields.push(field);
    type ResultType = TResult & TField;
    return (this as any) as FieldBuilder<NonNullable<T>, NonNullable<{ [P in keyof ResultType]: ResultType[P] }>>;
  }

  public getFields(): string[] {
    return this.fields;
  }

  public getType(): TResult {
    return this as any;
  }
}

type Xxx<T, TKey extends keyof any, TValue> = T extends Record<string, any>
  ? {
      [P in keyof T]: NonNullable<Xxx<T[P], TKey, TValue>>;
    }
  : {
      [P in TKey]: NonNullable<TValue>;
    };

class FieldSelector<T, TResult> {
  private readonly field: string[];

  constructor() {
    this.field = [];
  }

  public getField() {
    return this.field.join('.');
  }

  public prop<TKey extends keyof T>(field: TKey) {
    type ComponentType = T[TKey] extends Array<infer U> ? U : T[TKey];
    type ValueType = T[TKey] extends any[] ? unknown[] : T[TKey] extends Record<string, any> ? unknown : T[TKey];
    this.field.push(`${field}`);
    return (this as any) as FieldSelector<ComponentType, Xxx<TResult, TKey, ValueType>>;
  }
}
