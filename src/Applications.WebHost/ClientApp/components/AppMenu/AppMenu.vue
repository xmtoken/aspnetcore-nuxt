<script lang="ts">
import { VueBuilder } from '~/core/vue';
import { Slotable } from '~/mixins/slotable';
import { ProxyProps } from '~/types/global';

type ComponentProxyProps = ProxyProps & {
  closeOnContentClick?: boolean;
  disabled?: boolean;
  minWidth?: string | number;
  nudgeBottom?: string | number;
  openOnClick?: boolean;
  value?: any;
};

export type AppMenuProps = ComponentProxyProps & {
  //
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProxyProps>()
  .mixin(Slotable)
  .build();

export default Vue.extend({
  inheritAttrs: false,
  model: {
    event: 'input',
    prop: 'value',
  },
  data() {
    return {
      opend: null as any,
    };
  },
  computed: {
    props() {
      const defaults: ComponentProxyProps = {
        closeOnContentClick: false,
      };
      const attrs: ComponentProxyProps & Record<string, any> = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProxyProps = {
        //
      };
      delete attrs[this.$options.model!.prop!];
      return {
        ...attrs,
        ...overrides,
      };
    },
  },
  watch: {
    'attrs.value': {
      handler(val: any, _oldVal: any) {
        this.opend = val;
      },
      immediate: true,
    },
  },
  methods: {
    close() {
      this.opend = false;
      this.$emit(this.$options.model!.event!, this.opend);
    },
    open() {
      this.opend = true;
      this.$emit(this.$options.model!.event!, this.opend);
    },
  },
});
</script>

<template>
  <v-menu v-bind="props" :value="opend" v-on="$listeners">
    <template v-slot>
      <slot v-bind="{ close, opend }" />
    </template>
    <template v-slot:activator="scope">
      <slot v-bind="{ ...scope, open, opend }" name="activator" />
    </template>
  </v-menu>
</template>
