<script lang="ts">
import { mdiClockOutline } from '@mdi/js';
import { PropType } from 'vue';
import { VueBuilder } from '~/core/vue';
import * as TimeFormatter from '~/extensions/formatters/time-formatter';
import { Slotable } from '~/mixins/slotable';
import { Listeners } from '~/types/vue';

type PickerProps = {
  format: string;
  useSeconds: boolean;
};

const Vue = VueBuilder.create() //
  .mixin(Slotable)
  .build();

export default Vue.extend({
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
    dense: {
      default: false,
      type: Boolean,
    },
    disabled: {
      default: false,
      type: Boolean,
    },
    label: {
      default: undefined,
      type: String,
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
      menu: false,
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
    onChange(): void {
      if (this.pickerValue) {
        this.internalValue = this.pickerValue;
      }
    },
    openPickerMenu(): void {
      this.menu = true;
    },
  },
});
</script>

<template>
  <app-text-field ref="field" v-model="internalValue" v-bind="$attrs" :append-icon="appendIconInternal" :append-icon-tabindex="appendIconTabindex" :dense="dense" :disabled="disabled" :label="label" :readonly="readonly" v-on="listeners" @blur="onBlur" @change="onChange" @click:append="openPickerMenu">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
    <template v-slot:label>
      <app-menu v-model="menu" :activator="$refs.field" :close-on-content-click="false" :disabled="disabled || readonly" min-width="inherit" :nudge-bottom="menuNudgeBottom" :nudge-left="menuNudgeLeft" :open-on-click="false">
        <template v-slot="{ close, opend }">
          <v-time-picker v-if="opend" v-model="pickerValue" v-bind="pickerProps" @change="close" />
        </template>
      </app-menu>
      <slot name="label">
        {{ label }}
      </slot>
    </template>
  </app-text-field>
</template>
