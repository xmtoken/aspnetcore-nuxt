export type ExpandArray<T> = NonNullable<T> extends Array<infer U> ? U : never;

export type OmitProp<T, U> = Pick<
  T,
  {
    [P in keyof T]: NonNullable<T[P]> extends U ? never : P;
  }[keyof T]
>;

export type OmitArray<T> = OmitProp<T, any[]>;

export type OmitFunction<T> = OmitProp<T, Function>;

export type OmitObject<T> = OmitProp<T, object>;

export type OmitPrimitive<T> = OmitProp<T, string | number | boolean>;

export type OmitString<T> = OmitProp<T, string>;

export type PickProp<T, U> = Pick<
  T,
  {
    [P in keyof T]: NonNullable<T[P]> extends U ? P : never;
  }[keyof T]
>;

export type PickArray<T> = PickProp<T, any[]>;

export type PickPrimitive<T> = PickProp<T, string | number | boolean>;

export type PickRecord<T> = OmitArray<OmitFunction<OmitPrimitive<T>>>;
