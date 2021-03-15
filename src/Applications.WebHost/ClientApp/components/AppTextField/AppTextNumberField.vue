<script lang="ts">
import numbro from 'numbro';
import { PropType } from 'vue';
import { AppTextFieldProps } from '~/components/AppTextField/AppTextField.vue';
import { VueBuilder } from '~/core/vue';
import { Slotable } from '~/mixins/slotable';
import { UIElementState } from '~/mixins/ui-element-state';
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
  .mixin(UIElementState)
  .build();

export default Vue.extend({
  inheritAttrs: false,
  props: {
    format: {
      default(): PickProp<ComponentProps, 'format'> {
        return {
          mantissa: 0,
          thousandSeparated: true,
        };
      },
      type: (null as any) as PropType<PickProp<ComponentProps, 'format'>>,
    },
  },
  data() {
    return {
      latestValue: null as PickProp<ComponentProps, 'value'>,
      value: null as PickProp<ComponentProps, 'value'>,
    };
  },
  computed: {
    listeners() {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      delete listeners.change;
      return listeners;
    },
    props() {
      const defaults: ComponentProxyProps = {
        //
      };
      const attrs: ComponentProxyProps & Record<string, any> = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProxyProps = {
        //
      };
      delete attrs.value;
      return {
        ...attrs,
        ...overrides,
      };
    },
  },
  watch: {
    format: {
      deep: true,
      handler(_val: PickProp<ComponentProps, 'format'>) {
        const formattedValue = this.formatValue(this.value);
        const unformattedValue = this.unformatValue(formattedValue);
        this.value = formattedValue;
        this.$emit('input', unformattedValue);
        this.$emit('change', unformattedValue);
      },
    },
    'attrs.value': {
      handler(val: PickProp<ComponentProps, 'value'>, _oldVal: PickProp<ComponentProps, 'value'>) {
        if (!this.focused) {
          const formattedValue = this.formatValue(val);
          const unformattedValue = this.unformatValue(formattedValue);
          this.value = formattedValue;
          if (unformattedValue !== val) {
            this.$emit('input', unformattedValue);
            this.$emit('change', unformattedValue);
          }
        }
      },
      immediate: true,
    },
  },
  methods: {
    formatValue(val: any): string {
      const value = numbro(val);
      if (Number.isFinite(value.value())) {
        return value.format(this.format ?? undefined);
      }
      return val;
    },
    unformatValue(val: string): number | undefined {
      return numbro.unformat(val, this.format ?? undefined);
    },
    onFocus() {
      const input = this.$el.querySelector<HTMLInputElement>(':scope > .v-input__control > .v-input__slot > .v-text-field__slot > input');
      const focusedByTabKey = this.value?.toString().length === (input?.selectionEnd ?? 0) - (input?.selectionStart ?? 0);
      if (focusedByTabKey) {
        this.$nextTick(() => input?.select());
      }
      const unformatted = this.unformatValue(this.value);
      if (unformatted !== undefined) {
        this.value = unformatted.toString();
      }
    },
    onInput(val: PickProp<ComponentProps, 'value'>) {
      const unformatted = this.unformatValue(val);
      if (unformatted !== undefined) {
        const formattedValue = this.formatValue(unformatted);
        const unformattedValue = this.unformatValue(formattedValue);
        this.$emit('input', unformattedValue);
      } else {
        this.$emit('input', val);
      }
    },
    onChange(val: PickProp<ComponentProps, 'value'>) {
      const unformatted = this.unformatValue(val);
      if (unformatted !== undefined) {
        const formattedValue = this.formatValue(unformatted);
        const unformattedValue = this.unformatValue(formattedValue);
        this.value = formattedValue;
        this.$emit('change', unformattedValue);
      } else {
        this.$emit('change', val);
      }
    },
  },
});
</script>

<template>
  <app-text-field v-model="value" v-bind="props" @change="onChange" @focus="onFocus" @input="onInput">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
  </app-text-field>
</template>
