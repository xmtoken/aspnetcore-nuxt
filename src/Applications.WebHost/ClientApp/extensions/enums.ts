import Vue, { VueConstructor } from 'vue';

type Enumerations<T> = {
  Enum: T;
};

// eslint-disable-next-line import/export
export default function enums<T>(values: T): VueConstructor<Vue & Enumerations<T>>;
// eslint-disable-next-line import/export
export default function enums<T>(values: T): VueConstructor {
  return Vue.extend({
    computed: {
      Enum: () => values,
    },
  });
}
