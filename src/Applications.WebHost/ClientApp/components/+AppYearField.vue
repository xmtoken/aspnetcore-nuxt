<script lang="ts">
import dayjs from 'dayjs';
import Vue, { VueConstructor } from 'vue';
import { AppDatePicker } from '../components';
import mixins from '../extensions/mixins';
import slotable from '../mixins/slotable';

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
    max: {
      default: undefined,
      type: String,
    },
    menuOffsetLeft: {
      default: false,
      type: Boolean,
    },
    menuOffsetY: {
      default: false,
      type: Boolean,
    },
    openOnClick: {
      default: true,
      type: Boolean,
    },
    pickerWidth: {
      default: 150,
      type: [Number, String],
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
      model: this.value as string | undefined,
    };
  },
  computed: {
    date: {
      get(): string | undefined {
        const value = this.model?.toString().trim() || '';
        if (value && dayjs(value).isValid()) {
          return dayjs(value).format('YYYY-01-01');
        }
        return undefined;
      },
      set(val: string | undefined): void {
        this.model = dayjs(val).format('YYYY');
      },
    },
    listeners(): Record<string, Function | Function[]> {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
    menuClasses(): string | undefined {
      if (this.menuOffsetY) {
        return this.dense ? 'v-menu__content--offset-y-dense' : 'v-menu__content--offset-y';
      }
      return undefined;
    },
    menuNudgeLeft(): number {
      return this.menuOffsetLeft ? 290 /* menu width */ + 5 /* space */ : 0;
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
    model(val: string | undefined): void {
      this.$emit('input', val);
    },
    value(val: string | undefined): void {
      this.model = val;
    },
  },
  methods: {
    onComplete(): void {
      if (this.date) {
        this.model = dayjs(this.date).format('YYYY');
      }
    },
  },
});
</script>

<template>
  <v-menu v-model="menu" :close-on-content-click="false" :content-class="menuClasses" :disabled="readonly" min-width="inherit" :nudge-left="menuNudgeLeft" :open-on-click="openOnClick">
    <template v-slot:activator="{ on }">
      <app-text-field v-model="model" v-bind="$attrs" :class="contentClass" :dense="dense" :readonly="readonly" :style="contentStyle" v-on="{ ...listeners, ...on }" @blur="onComplete" @click:append="menu = true" @keydown.enter="onComplete">
        <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
        <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
          <slot v-bind="scope" :name="scopedSlotKey" />
        </template>
      </app-text-field>
    </template>
    <app-date-picker ref="picker" v-model="date" :max="`${max}-12-31`" no-title reactive :width="pickerWidth" @input="menu = false" />
  </v-menu>
</template>

<style lang="scss" scoped>
.v-menu__content--offset-y ::v-deep {
  margin-top: 45px;
}

.v-menu__content--offset-y-dense ::v-deep {
  margin-top: 29px;
}
</style>
