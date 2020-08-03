import cloneDeep from 'lodash/cloneDeep';

export function pretty<T extends Record<string, any>>(value: T): T {
  const clone = cloneDeep(value);
  for (const key in clone) {
    if (!clone[key]?.toString()) {
      delete clone[key];
    }
  }
  return clone;
}
