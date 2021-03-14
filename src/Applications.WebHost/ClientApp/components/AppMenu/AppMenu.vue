<script lang="ts">
import { VueBuilder } from '~/core/vue';
import { Slotable } from '~/mixins/slotable';
import { PickProp, ProxyProps } from '~/types/global';

type ComponentProxyProps = ProxyProps & {
  closeOnContentClick?: boolean | null;
  disabled?: boolean | null;
  minWidth?: string | number | null;
  nudgeBottom?: string | number | null;
  openOnClick?: boolean | null;
  value?: any;
};

type ComponentProps = ComponentProxyProps & {
  //
};

export type AppMenuProps = ComponentProps;

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
      value: null as PickProp<ComponentProps, 'value'>,
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
      handler(val: PickProp<ComponentProps, 'value'>, _oldVal: PickProp<ComponentProps, 'value'>) {
        this.value = val;
      },
      immediate: true,
    },
  },
  methods: {
    close() {
      this.value = false;
      this.$emit(this.$options.model!.event!, this.value);
    },
    open() {
      this.value = true;
      this.$emit(this.$options.model!.event!, this.value);
    },
  },
});
</script>

<template>
  <v-menu v-bind="props" :value="value" v-on="$listeners">
    <template v-slot>
      <slot v-bind="{ close, opend: value }" />
    </template>
    <template v-slot:activator="scope">
      <slot v-bind="{ ...scope, open, opend: value }" name="activator" />
    </template>
  </v-menu>
</template>
