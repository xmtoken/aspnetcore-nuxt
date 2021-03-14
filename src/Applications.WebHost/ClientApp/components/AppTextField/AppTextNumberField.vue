<script lang="ts">
import numbro from 'numbro';
import { PropType } from 'vue';
import { AppTextFieldProps } from '~/components/AppTextField/AppTextField.vue';
import { VueBuilder } from '~/core/vue';
import { Slotable } from '~/mixins/slotable';
import { PickProp, ProxyProps } from '~/types/global';

type ComponentProxyProps = ProxyProps &
  AppTextFieldProps & {
    value?: any;
  };

type ComponentProps = ComponentProxyProps & {
  format?: string | numbro.Format | null;
};

export type AppTextNumberFieldProps = ComponentProps;

const Vue = VueBuilder.create() //
  .$attrs<ComponentProxyProps>()
  .mixin(Slotable)
  .build();

export default Vue.extend({
  inheritAttrs: false,
  props: {
    // format: {
    //   default() {
    //     return {
    //       mantissa: 0,
    //       thousandSeparated: true,
    //     };
    //   },
    //   type: (null as any) as PropType<PickProp<ComponentProps, 'format'>>,
    // },
  },
  // data() {
  //   return {
  //     // focused: false,
  //     // internalValue: NumberFormatter.format(this.value, this.format),
  //     latestValue: null as PickProp<ComponentProps, 'value'>,
  //     value: null as PickProp<ComponentProps, 'value'>,
  //   };
  // },
  computed: {
    listeners() {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      delete listeners.change;
      return listeners;
    },
    // props() {
    //   const defaults: ComponentProxyProps = {
    //     //
    //   };
    //   const attrs: ComponentProxyProps & Record<string, any> = {
    //     ...defaults,
    //     ...this.attrs,
    //   };
    //   const overrides: ComponentProxyProps = {
    //     //
    //   };
    //   delete attrs.value;
    //   return {
    //     ...attrs,
    //     ...overrides,
    //   };
    // },
  },
  watch: {
    // format(val: PickProp<ComponentProps, 'format'>) {
    //   if (!this.focused) {
    //     this.internalValue = NumberFormatter.format(this.internalValue, val);
    //   }
    // },
    // 'attrs.value': {
    //   handler(val: PickProp<ComponentProps, 'value'>, _oldVal: PickProp<ComponentProps, 'value'>) {
    //     // if (!this.focused) {
    //     //   this.internalValue = NumberFormatter.format(val, this.format);
    //     // }
    //   },
    //   immediate: true,
    // },
  },
  methods: {
    // onBlue() {
    //   this.focused = false;
    //   this.internalValue = NumberFormatter.format(this.internalValue, this.format);
    // },
    // onChange(val: any) {
    //   this.internalValue = NumberFormatter.format(val, this.format);
    //   const unformatted = numbro.unformat(val, {});
    //   if (unformatted?.toString()) {
    //     this.$emit('change', unformatted);
    //   } else {
    //     this.$emit('change', val);
    //   }
    // },
    // onFocus() {
    //   this.focused = true;
    //   const input = this.$el.querySelector<HTMLInputElement>(':scope > .v-input__control > .v-input__slot > .v-text-field__slot > input');
    //   const focusedByTabKey = this.internalValue?.toString().length === (input?.selectionEnd ?? 0) - (input?.selectionStart ?? 0);
    //   if (focusedByTabKey) {
    //     this.$nextTick(() => input?.select());
    //   }
    //   const unformatted = numbro.unformat(this.internalValue, {});
    //   if (unformatted?.toString()) {
    //     this.internalValue = unformatted?.toString();
    //   }
    // },
    // onInputValue(val: any) {
    //   const unformatted = numbro.unformat(val, {});
    //   if (unformatted?.toString()) {
    //     this.$emit('input:value', unformatted);
    //   } else {
    //     this.$emit('input:value', val);
    //   }
    // },
  },
});
</script>

<template>
  <app-text-field v-bind="props">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
  </app-text-field>
  <!-- <app-text-field v-model="value" v-bind="props" class="text-right" v-on="listeners" @blur="onBlue" @change="onChange" @focus="onFocus" @input:value="onInputValue">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
  </app-text-field> -->
</template>
