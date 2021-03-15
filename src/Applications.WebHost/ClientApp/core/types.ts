export type OmitProp<T, U> = Pick<
  T,
  {
    [TKey in keyof T]: NonNullable<T[TKey]> extends U ? never : TKey;
  }[keyof T]
>;

export type OmitArray<T> = OmitProp<T, any[]>;

export type OmitFunction<T> = OmitProp<T, Function>;

export type OmitObject<T> = OmitProp<T, object>;

export type OmitString<T> = OmitProp<T, string>;
