// https://github.com/vuetifyjs/vuetify/blob/v2.3.7/packages/vuetify/src/util/mixins.ts

import Vue, { VueConstructor } from 'vue';

// eslint-disable-next-line import/export
export default function mixins<T extends VueConstructor[]>(...args: T): ExtractVue<T> extends infer V ? (V extends Vue ? VueConstructor<V> : never) : never;
// eslint-disable-next-line import/export
export default function mixins<T extends Vue>(...args: VueConstructor[]): VueConstructor<T>;
// eslint-disable-next-line import/export
export default function mixins(...args: VueConstructor[]): VueConstructor {
  return Vue.extend({ mixins: args });
}

export type ExtractVue<T extends VueConstructor | VueConstructor[]> = T extends (infer U)[] ? UnionToIntersection<U extends VueConstructor<infer V> ? V : never> : T extends VueConstructor<infer V> ? V : never;

type UnionToIntersection<U> = (U extends any ? (k: U) => void : never) extends (k: infer I) => void ? I : never;
