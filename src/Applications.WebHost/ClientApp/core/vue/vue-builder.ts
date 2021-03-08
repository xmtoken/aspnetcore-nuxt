import { Auth } from '@nuxtjs/auth';
import { camelCase } from 'lodash';
import Vue, { VueConstructor } from 'vue';

export class VueBuilder<T extends Vue> {
  private readonly mixins: VueConstructor[];

  private constructor() {
    this.mixins = [];
  }

  public static create() {
    return new VueBuilder();
  }

  private assign<TAssign>() {
    return (this as any) as VueBuilder<T & TAssign>;
  }

  public $attrs<TAttrs>() {
    const keyConverter = val => {
      if (Array.isArray(val)) {
        return val.map(value => keyConverter(value));
      } else if (!!val && typeof val === 'object') {
        const value = {} as any;
        Object.keys(val).forEach(key => {
          value[camelCase(key)] = keyConverter(val[key]);
        });
        return value;
      } else {
        return val;
      }
    };

    const mixin = Vue.extend({
      computed: {
        attrs(): TAttrs {
          return keyConverter(this.$attrs);
        },
      },
    }) as VueConstructor;
    return this.mixin(mixin).assign<{ attrs: TAttrs }>();
  }

  public $auth<TUser>() {
    return this.assign<{ $auth: Auth<TUser> }>();
  }

  public $refs<TRefs>() {
    return this.assign<{ $refs: TRefs }>();
  }

  public build() {
    return Vue.extend({ mixins: this.mixins }) as VueConstructor<T>;
  }

  public mixin<Mixin extends VueConstructor>(mixin: Mixin) {
    this.mixins.push(mixin);
    return this.assign<Mixin extends VueConstructor<infer V> ? V : never>();
  }
}
