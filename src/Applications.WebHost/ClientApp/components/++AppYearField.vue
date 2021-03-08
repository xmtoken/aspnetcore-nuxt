<script lang="ts">
import { mdiCalendar } from '@mdi/js';
import Vue, { PropType, VueConstructor } from 'vue';
import AppDatePicker from '~/components/AppDatePicker.vue';
import * as DateFormatter from '~/extensions/formatters/date-formatter';
import mixins from '~/extensions/mixins';
import listenable from '~/mixins/listenable';
import { Slotable } from '~/mixins/slotable';
import { Listeners } from '~/types/vue';

const $refs = Vue as VueConstructor<
  Vue & {
    $refs: {
      picker: InstanceType<typeof AppDatePicker>;
    };
  }
>;

export default mixins($refs, listenable, Slotable).extend({
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
        return {};
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
    menuNudgeBottom(): number {
      return this.pickerOffsetY ? (this.dense ? 29 : 45) : 0;
    },
    menuNudgeLeft(): number {
      return this.pickerOffsetLeft ? 290 /* menu-width */ + 5 /* space */ : 0;
    },
    pickerValue: {
      get(): string | null {
        return DateFormatter.isValid(this.model) ? DateFormatter.format(this.model, 'YYYY-MM-DD') : null;
      },
      set(val: string | null): void {
        // todo
        // this.model = val;
        this.model = val ? val.split('-')[0] : null;
      },
    },
  },
  watch: {
    menu(val: boolean): void {
      if (val) {
        this.$nextTick(() => {
          this.$refs.picker.activePicker = 'YEAR';
        });
      } else {
        this.$refs.picker.activePicker = 'YEAR';
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
        // todo
        // this.model = this.pickerValue;
        this.model = this.pickerValue ? this.pickerValue.split('-')[0] : null;
      }
    },
  },
});
</script>

<template>
  <v-menu v-model="menu" v-bind="menuProps" close-on-content-click :disabled="readonly" min-width="inherit" :nudge-bottom="menuNudgeBottom" :nudge-left="menuNudgeLeft">
    <template v-slot:activator="{ on }">
      <app-text-field v-model="model" v-bind="$attrs" :append-icon="appendIconInternal" :append-icon-tabindex="appendIconTabindex" :class="contentClass" :dense="dense" :disabled="disabled" :readonly="readonly" :style="contentStyle" v-on="{ ...listeners, ...withEmit(on) }" @blur="onBlur" @click:append="menu = true">
        <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
        <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
          <slot v-bind="scope" :name="scopedSlotKey" />
        </template>
      </app-text-field>
    </template>
    <app-date-picker ref="picker" v-model="pickerValue" v-bind="pickerProps" reactive />
  </v-menu>
</template>

<style lang="scss" scoped>
.v-picker ::v-deep {
  .v-date-picker-title__year {
    margin: 0;
  }

  .v-date-picker-title__date {
    display: none;
  }
}
</style>
