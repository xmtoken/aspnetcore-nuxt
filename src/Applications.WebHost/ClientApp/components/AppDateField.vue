<script lang="ts">
import { mdiCalendar } from '@mdi/js';
import * as DateHelper from '~/extensions/date';
import mixins from '~/extensions/mixins';
import slotable from '~/mixins/slotable';

export default mixins(slotable).extend({
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
      type: [Object, String],
    },
    contentStyle: {
      default: undefined,
      type: [Object, String],
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
        return {};
      },
      type: Object,
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
        return {};
      },
      type: Object,
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
    listeners(): Record<string, Function | Function[]> {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
    menuNudgeBottom(): number {
      return this.pickerOffsetY ? (this.dense ? 29 : 45) : 0;
    },
    menuNudgeLeft(): number {
      return this.pickerOffsetLeft ? 290 /* menu width */ + 5 /* space */ : 0;
    },
    menuPropsInternal(): object {
      return {
        openOnClick: false,
        ...this.menuProps,
      };
    },
    pickerPropsInternal(): { type: string } {
      return {
        type: 'date',
        ...this.pickerProps,
      };
    },
    pickerValue: {
      get(): string | null {
        const format = this.pickerPropsInternal.type === 'date' ? 'YYYY-MM-DD' : 'YYYY-MM';
        return DateHelper.isValid(this.model) ? DateHelper.format(this.model, format) : null;
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
    onComplete(): void {
      if (this.pickerValue) {
        this.model = this.pickerValue;
      }
    },
  },
});
</script>

<template>
  <v-menu v-model="menu" v-bind="menuPropsInternal" :close-on-content-click="false" :disabled="readonly" min-width="inherit" :nudge-bottom="menuNudgeBottom" :nudge-left="menuNudgeLeft">
    <template v-slot:activator="{ on }">
      <app-text-field v-model="model" v-bind="$attrs" :append-icon="appendIconInternal" :append-icon-tabindex="appendIconTabindex" :class="contentClass" :dense="dense" :disabled="disabled" :readonly="readonly" :style="contentStyle" v-on="{ ...listeners, ...on }" @blur="onComplete" @click:append="menu = true" @keydown.enter="onComplete">
        <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
        <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
          <slot v-bind="scope" :name="scopedSlotKey" />
        </template>
      </app-text-field>
    </template>
    <app-date-picker v-model="pickerValue" v-bind="pickerPropsInternal" @change="menu = false" />
  </v-menu>
</template>
