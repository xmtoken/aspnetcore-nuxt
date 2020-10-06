<script lang="ts">
import { mdiClockOutline } from '@mdi/js';
import { PropType } from 'vue';
import * as TimeFormatter from '~/extensions/formatters/time-formatter';
import mixins from '~/extensions/mixins';
import listenable from '~/mixins/listenable';
import slotable from '~/mixins/slotable';
import { Listeners } from '~/types/vue';

type PickerProps = {
  format: string;
  useSeconds: boolean;
};

export default mixins(listenable, slotable).extend({
  inheritAttrs: false,
  model: {
    event: 'input:value',
  },
  props: {
    appendIcon: {
      default: mdiClockOutline,
      type: String,
    },
    appendIconTabindex: {
      default: -1,
      type: Number,
    },
    contentClass: {
      default: undefined,
      type: [String, Object] as PropType<string | object>,
    },
    contentStyle: {
      default: undefined,
      type: [String, Object] as PropType<string | object>,
    },
    dense: {
      default: false,
      type: Boolean,
    },
    disabled: {
      default: false,
      type: Boolean,
    },
    menuProps: {
      default(): object {
        return {
          openOnClick: false,
        };
      },
      type: Object as PropType<object>,
    },
    pickerOffsetLeft: {
      default: false,
      type: Boolean,
    },
    pickerOffsetY: {
      default: false,
      type: Boolean,
    },
    pickerProps: {
      default(): PickerProps {
        return {
          format: '24hr',
          useSeconds: false,
        };
      },
      type: Object as PropType<PickerProps>,
    },
    readonly: {
      default: false,
      type: Boolean,
    },
    value: {
      default: undefined,
      type: (null as any) as PropType<any>,
    },
  },
  data() {
    return {
      internalValue: this.value,
    };
  },
  computed: {
    appendIconInternal(): string | null {
      return this.disabled || this.readonly ? null : this.appendIcon;
    },
    listeners(): Listeners {
      const listeners = { ...this.$listeners };
      delete listeners['input:value'];
      return listeners;
    },
    menuNudgeBottom(): number {
      return this.pickerOffsetY ? (this.dense ? 29 : 45) : 0;
    },
    menuNudgeLeft(): number {
      return this.pickerOffsetLeft ? 290 /* menu-width */ + 5 /* space */ : 0;
    },
    pickerValue: {
      get(): string | null {
        const format = this.pickerProps.useSeconds ? 'HH:mm:ss' : 'HH:mm';
        return TimeFormatter.isValid(this.internalValue) ? TimeFormatter.format(this.internalValue, format) : null;
      },
      set(val: string | null): void {
        this.internalValue = val;
      },
    },
  },
  watch: {
    internalValue(val: any): void {
      this.$emit('input:value', val);
    },
    value(val: any): void {
      this.internalValue = val;
    },
  },
  methods: {
    onBlur(): void {
      if (this.pickerValue) {
        this.internalValue = this.pickerValue;
      }
    },
  },
});
</script>

<template>
  <app-menu v-bind="menuProps" :close-on-content-click="false" :disabled="readonly" min-width="inherit" :nudge-bottom="menuNudgeBottom" :nudge-left="menuNudgeLeft">
    <template v-slot:activator="{ on, open }">
      <app-text-field v-model="internalValue" v-bind="$attrs" :append-icon="appendIconInternal" :append-icon-tabindex="appendIconTabindex" :class="contentClass" :dense="dense" :disabled="disabled" :readonly="readonly" :style="contentStyle" v-on="{ ...listeners, ...withEmit(on) }" @blur="onBlur" @click:append="open">
        <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
        <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
          <slot v-bind="scope" :name="scopedSlotKey" />
        </template>
      </app-text-field>
    </template>
    <template v-slot="{ close, opend }">
      <v-time-picker v-if="opend" v-model="pickerValue" v-bind="pickerProps" @change="close" />
    </template>
  </app-menu>
</template>
