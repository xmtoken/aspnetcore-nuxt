<script lang="ts">
import { mdiCalendar } from '@mdi/js';
import dayjs from 'dayjs';
import AppDatePicker from '~/components/AppDatePicker/AppDatePicker.vue';
import { VueBuilder } from '~/core/vue';
import * as DateFormatter from '~/extensions/formatters/date-formatter';
import { Slotable } from '~/mixins/slotable';
import { Listeners } from '~/types/vue';

const Vue = VueBuilder.create() //
  .$refs<{
    picker: InstanceType<typeof AppDatePicker>;
  }>()
  .mixin(Slotable)
  .build();

export default Vue.extend({
  inheritAttrs: false,
  model: {
    event: 'input:value',
  },
  props: {
    appendIcon: {
      default: mdiCalendar,
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
      default(): object {
        return {
          max: dayjs().subtract(1, 'day').format('YYYY-MM-DD'),
        };
      },
      type: Object as PropType<object>,
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
        return DateFormatter.isValid(this.internalValue) ? DateFormatter.format(this.internalValue, 'YYYY-MM-DD') : null;
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
    menu(val: boolean): void {
      if (val) {
        this.$nextTick(() => {
          this.$refs.picker.activePicker = 'YEAR';
        });
      } else {
        this.$refs.picker.activePicker = 'YEAR';
      }
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
        <template v-slot="{ close }">
          <app-date-picker ref="picker" v-model="pickerValue" v-bind="pickerProps" @change="close" />
        </template>
      </app-menu>
      <slot name="label">
        {{ label }}
      </slot>
    </template>
  </app-text-field>
</template>
