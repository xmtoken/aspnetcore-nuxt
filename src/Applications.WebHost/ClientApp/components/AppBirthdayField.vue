<script lang="ts">
import { mdiCalendar } from '@mdi/js';
import dayjs from 'dayjs';
import { VueMaskDirective } from 'v-mask';
import Vue, { VueConstructor, PropType } from 'vue';
import AppDatePicker from '~/components/AppDatePicker.vue';
import * as DateHelper from '~/extensions/date';
import mixins from '~/extensions/mixins';
import slotable from '~/mixins/slotable';
import { Listeners } from '~/types/vue';

const $refs = Vue as VueConstructor<
  Vue & {
    $refs: {
      picker: InstanceType<typeof AppDatePicker>;
    };
  }
>;

export default mixins($refs, slotable).extend({
  directives: {
    mask: VueMaskDirective,
  },
  inheritAttrs: false,
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
      type: [Object, String] as PropType<string | object>,
    },
    contentStyle: {
      default: undefined,
      type: [Object, String] as PropType<string | object>,
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
          max: dayjs().format('YYYY-MM-DD'),
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
      type: String,
    },
  },
  data() {
    return {
      menu: false,
      model: this.value as string | null,
    };
  },
  computed: {
    appendIconInternal(): string | null {
      return this.disabled || this.readonly ? null : this.appendIcon;
    },
    listeners(): Listeners {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
    mask(): string {
      return '####-#?#-#?#';
    },
    menuNudgeBottom(): number {
      return this.pickerOffsetY ? (this.dense ? 29 : 45) : 0;
    },
    menuNudgeLeft(): number {
      return this.pickerOffsetLeft ? 290 /* menu-width */ + 5 /* space */ : 0;
    },
    pickerValue: {
      get(): string | null {
        return DateHelper.isValid(this.model) ? DateHelper.format(this.model, 'YYYY-MM-DD') : null;
      },
      set(val: string | null): void {
        this.model = val;
      },
    },
  },
  watch: {
    menu(val: boolean): void {
      if (val) {
        this.$nextTick(() => {
          this.$refs.picker.activePicker = 'YEAR';
        });
      }
    },
    model(val: string | null): void {
      this.$emit('input', val);
    },
    value(val: string | null): void {
      this.model = val;
    },
  },
  methods: {
    onBlur(): void {
      if (this.pickerValue) {
        this.model = this.pickerValue;
      }
    },
    onInput(val: string | null): void {
      if (val) {
        if (/^[0-9]{4}-[0-9]--$/.test(val)) {
          // formatted `0000-0-` to `0000-0--` by v-mask, re-format `0000-0--` to `0000-00-`.
          this.model = val.substr(0, 5) + '0' + val.substr(5, 2);
        }
      }
    },
  },
});
</script>

<template>
  <v-menu v-model="menu" v-bind="menuProps" :close-on-content-click="false" :disabled="readonly" min-width="inherit" :nudge-bottom="menuNudgeBottom" :nudge-left="menuNudgeLeft">
    <template v-slot:activator="{ on }">
      <app-text-field v-model="model" v-mask="mask" v-bind="$attrs" :append-icon="appendIconInternal" :append-icon-tabindex="appendIconTabindex" :class="contentClass" :dense="dense" :disabled="disabled" :readonly="readonly" :style="contentStyle" v-on="{ ...listeners, ...on }" @blur="onBlur" @click:append="menu = true" @input="onInput">
        <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
        <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
          <slot v-bind="scope" :name="scopedSlotKey" />
        </template>
      </app-text-field>
    </template>
    <app-date-picker ref="picker" v-model="pickerValue" v-bind="pickerPropsInternal" @change="menu = false" />
  </v-menu>
</template>
