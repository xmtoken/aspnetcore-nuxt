<script lang="ts">
import { VueBuilder } from '~/core/vue';
import { Slotable } from '~/mixins/slotable';

type ComponentProxyProps = Record<string, any> & {
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
    event: 'change',
    prop: 'value',
  },
  data() {
    return {
      value: null as any,
    };
  },
  computed: {
    props() {
      const defaults: ComponentProxyProps = {
        closeOnContentClick: false,
      };
      const attrs: ComponentProxyProps = {
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
        this.value = val;
      },
      immediate: true,
    },
  },
  methods: {
    close(): void {
      this.value = false;
      this.$emit(this.$options.model!.event!, this.value);
    },
    open(): void {
      this.value = true;
      this.$emit(this.$options.model!.event!, this.value);
    },
  },
});
</script>

<template>
  <v-menu v-bind="props" :value="value" v-on="$listeners">
    <template v-slot>
      <slot v-bind="{ close, opend: !!value }" />
    </template>
    <template v-slot:activator="scope">
      <slot v-bind="{ ...scope, open, opend: !!value }" name="activator" />
    </template>
  </v-menu>
</template>
