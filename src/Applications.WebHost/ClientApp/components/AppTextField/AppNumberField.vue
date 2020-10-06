<script lang="ts">
import numbro from 'numbro';
import { PropType } from 'vue';
import * as NumberFormatter from '~/extensions/formatters/number-formatter';
import mixins from '~/extensions/mixins';
import slotable from '~/mixins/slotable';
import { Listeners } from '~/types/vue';

export default mixins(slotable).extend({
  inheritAttrs: false,
  model: {
    event: 'input:value',
  },
  props: {
    format: {
      default(): object {
        return {};
      },
      type: [String, Object] as PropType<string | numbro.Format>,
    },
    value: {
      default: undefined,
      type: (null as any) as PropType<any>,
    },
  },
  data() {
    return {
      focused: false,
      internalValue: NumberFormatter.format(this.value, this.format),
    };
  },
  computed: {
    listeners(): Listeners {
      const listeners = { ...this.$listeners };
      delete listeners['input:value'];
      return listeners;
    },
  },
  watch: {
    format(val: string | numbro.Format): void {
      if (!this.focused) {
        this.internalValue = NumberFormatter.format(this.internalValue, val);
      }
    },
    value(val: any): void {
      if (!this.focused) {
        this.internalValue = NumberFormatter.format(val, this.format);
      }
    },
  },
  methods: {
    onBlue(): void {
      this.focused = false;
      this.internalValue = NumberFormatter.format(this.internalValue, this.format);
    },
    onFocus(): void {
      this.focused = true;
      const input = this.$el.querySelector<HTMLInputElement>(':scope > .v-input__control > .v-input__slot > .v-text-field__slot > input');
      const focusedByTabKey = this.internalValue?.toString().length === (input?.selectionEnd ?? 0) - (input?.selectionStart ?? 0);
      if (focusedByTabKey) {
        this.$nextTick(() => input?.select());
      }
      const unformatted = numbro.unformat(this.internalValue?.toString() || '', {});
      if (unformatted?.toString()) {
        this.internalValue = unformatted?.toString();
      }
    },
    onInputValue(val: any): void {
      const unformatted = numbro.unformat(val, {});
      if (unformatted?.toString()) {
        this.$emit('input:value', unformatted);
      } else {
        this.$emit('input:value', val);
      }
    },
  },
});
</script>

<template>
  <app-text-field v-model="internalValue" v-bind="$attrs" class="text-right" v-on="listeners" @blur="onBlue" @focus="onFocus" @input:value="onInputValue">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
  </app-text-field>
</template>
