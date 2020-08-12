<script lang="ts">
import { mdiClockOutline } from '@mdi/js';
import { VueMaskDirective } from 'v-mask';
import { PropType } from 'vue';
import mixins from '~/extensions/mixins';
import * as TimeHelper from '~/extensions/time';
import slotable from '~/mixins/slotable';
import { Listeners } from '~/types/vue';

type PickerProps = {
  format: string;
  useSeconds: boolean;
};

export default mixins(slotable).extend({
  directives: {
    mask: VueMaskDirective,
  },
  inheritAttrs: false,
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
      return this.pickerProps.useSeconds ? '#?#:#?#:#?#' : '#?#:#?#';
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
        return TimeHelper.isValid(this.model) ? TimeHelper.format(this.model, format) : null;
      },
      set(val: string | null): void {
        this.model = val;
      },
    },
  },
  watch: {
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
        if (/^[0-9]::$/.test(val)) {
          // formatted `0:` to `0::` by v-mask, re-format `0::` to `00:`.
          this.model = '0' + val.substr(0, 2);
        }
        if (this.pickerProps.useSeconds) {
          if (/^[0-9]{2}:[0-9]::$/.test(val)) {
            // formatted `00:0:` to `00:0::` by v-mask, re-format `00:0::` to `00:00:`.
            this.model = val.substr(0, 3) + '0' + val.substr(3, 2);
          }
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
    <v-time-picker v-if="menu" v-model="pickerValue" v-bind="pickerProps" @change="menu = false" />
  </v-menu>
</template>
