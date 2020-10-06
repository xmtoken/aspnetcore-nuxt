<script lang="ts">
import { mdiCalendar } from '@mdi/js';
import dayjs from 'dayjs';
import Vue, { VueConstructor, PropType } from 'vue';
import AppDatePicker from '~/components/AppDatePicker/AppDatePicker.vue';
import * as DateFormatter from '~/extensions/formatters/date-formatter';
import mixins from '~/extensions/mixins';
import listenable from '~/mixins/listenable';
import slotable from '~/mixins/slotable';
import { Listeners } from '~/types/vue';

const $refs = Vue as VueConstructor<
  Vue & {
    $refs: {
      picker: InstanceType<typeof AppDatePicker>;
    };
  }
>;

export default mixins($refs, listenable, slotable).extend({
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
  },
});
</script>

<template>
  <v-menu v-model="menu" v-bind="menuProps" :close-on-content-click="false" :disabled="readonly" min-width="inherit" :nudge-bottom="menuNudgeBottom" :nudge-left="menuNudgeLeft">
    <template v-slot:activator="{ on }">
      <app-text-field v-model="internalValue" v-bind="$attrs" :append-icon="appendIconInternal" :append-icon-tabindex="appendIconTabindex" :class="contentClass" :dense="dense" :disabled="disabled" :readonly="readonly" :style="contentStyle" v-on="{ ...listeners, ...withEmit(on) }" @blur="onBlur" @click:append="menu = true">
        <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
        <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
          <slot v-bind="scope" :name="scopedSlotKey" />
        </template>
      </app-text-field>
    </template>
    <app-date-picker ref="picker" v-model="pickerValue" v-bind="pickerProps" @change="menu = false" />
  </v-menu>
</template>
