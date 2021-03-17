export const setState = (state: any, payload: { path: string; value: any }) => {
  payload.path.split(/[.[\]]+/).reduce((prev, key, index, array) => {
    if (array.length === index + 1) {
      prev[key] = payload.value;
    }
    return prev[key];
  }, state);
};

export const mapComputed = <T>(module: any, val: T, path: string): any => {
  //
  return Object.keys(val).reduce((prev, key) => {
    const value = (val as any)[key];
    if (Array.isArray(value)) {
      const field = {
        get() {
          return Object.values(value).map((v, i) => {
            //
            return mapComputed(module, v, path ? `${path}[${i}]` : key);
          });
        },
      };
      (prev as any)[key] = field;
    } else if (typeof value === 'object') {
      const field = {
        get() {
          return mapComputed(module, value, path ? `${path}.${key}` : key);
        },
      };
      (prev as any)[key] = field;
    } else {
      const field = {
        get() {
          return value;
        },
        set(v: any) {
          module.commit('setState', { path: path ? `${path}.${key}` : key, value: v });
        },
      };
      (prev as any)[key] = field;
    }
    return prev;
  }, {});
};
