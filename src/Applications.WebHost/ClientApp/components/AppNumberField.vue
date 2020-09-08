<script lang="ts">
import numbro from 'numbro';
import mixins from '~/extensions/mixins';
import * as NumberHelper from '~/extensions/number';
import slotable from '~/mixins/slotable';
import { Listeners } from '~/types/vue';

export default mixins(slotable).extend({
  inheritAttrs: false,
  props: {
    format: {
      default: '0',
      type: String,
    },
    value: {
      default: undefined,
      type: [Number, String],
    },
  },
  data() {
    return {
      focused: false,
      model: NumberHelper.format(this.value, this.format) as string | number | null,
    };
  },
  computed: {
    listeners(): Listeners {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
  },
  watch: {
    model(val: string | number | null): void {
      const formatted = NumberHelper.format(val, this.format);
      const value = NumberHelper.isValid(formatted) ? numbro(formatted).value() : val;
      this.$emit('input', value);
    },
    value(val: string | number | null): void {
      const format = this.focused ? '0' : this.format;
      this.model = NumberHelper.format(val, format);
    },
  },
  methods: {
    onBlue(): void {
      this.focused = false;
      this.model = NumberHelper.format(this.model, this.format);
    },
    onFocus(): void {
      this.focused = true;
    },
  },
});
</script>

<template>
  <app-text-field v-model="model" v-bind="$attrs" class="text-right" v-on="listeners" @blur="onBlue" @focus="onFocus">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
  </app-text-field>
</template>
