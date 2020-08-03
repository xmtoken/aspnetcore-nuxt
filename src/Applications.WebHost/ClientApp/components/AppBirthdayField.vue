<script lang="ts">
import { mdiCalendar } from '@mdi/js';
import dayjs from 'dayjs';
import Vue, { VueConstructor } from 'vue';
import AppDatePicker from '~/components/AppDatePicker.vue';
import * as DateHelper from '~/extensions/date';
import mixins from '~/extensions/mixins';
import slotable from '~/mixins/slotable';

const $refs = Vue as VueConstructor<
  Vue & {
    $refs: {
      picker: InstanceType<typeof AppDatePicker>;
    };
  }
>;

export default mixins($refs, slotable).extend({
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
    pickerPropsInternal(): object {
      return {
        max: dayjs().format('YYYY-MM-DD'),
        ...this.pickerProps,
      };
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
      <app-text-field v-model="model" v-bind="$attrs" :append-icon="appendIcon" :append-icon-tabindex="appendIconTabindex" :class="contentClass" :dense="dense" :readonly="readonly" :style="contentStyle" v-on="{ ...listeners, ...on }" @blur="onComplete" @click:append="menu = true" @keydown.enter="onComplete">
        <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
        <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
          <slot v-bind="scope" :name="scopedSlotKey" />
        </template>
      </app-text-field>
    </template>
    <app-date-picker ref="picker" v-model="pickerValue" v-bind="pickerPropsInternal" @change="menu = false" />
  </v-menu>
</template>
