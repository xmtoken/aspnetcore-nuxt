<script>
import { Slotable } from '~/mixins';
import dayjs from 'dayjs';
export default {
  mixins: [
    //
    Slotable,
  ],
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
      default: false,
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
      type: [Array, String],
    },
  },
  data() {
    return {
      menu: false,
      model: this.value,
    };
  },
  computed: {
    date: {
      /** @returns {Array|String} */
      get() {
        const value = this.model?.toString().trim() || '';
        if (value && dayjs(value).isValid()) {
          return dayjs(value).format('YYYY-01-01');
        }
        return null;
      },
      /** @param {Array|String} val */
      set(val) {
        this.model = dayjs(val).format('YYYY');
      },
    },
    /** @returns {Object} */
    listeners() {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
    /** @returns {String} */
    menuClasses() {
      if (this.menuOffsetY) {
        return this.dense ? 'v-menu__content--offset-y-dense' : 'v-menu__content--offset-y';
      }
      return undefined;
    },
    /** @returns {Number} */
    menuNudgeLeft() {
      return this.menuOffsetLeft ? 290 /*menu width*/ + 5 /*space*/ : 0;
    },
  },
  watch: {
    menu(val) {
      if (val) {
        this.$nextTick(() => {
          this.$refs.picker.activePicker = 'YEAR';
        });
      }
    },
    model(val) {
      this.$emit('input', val);
    },
    value(val) {
      this.model = val;
    },
  },
  methods: {
    onComplete() {
      if (this.date) {
        this.model = dayjs(this.date).format('YYYY');
      }
    },
  },
};
</script>

<template>
  <v-menu v-model="menu" :close-on-content-click="false" :content-class="menuClasses" :disabled="readonly" min-width="inherit" :nudge-left="menuNudgeLeft" :open-on-click="openOnClick">
    <template v-slot:activator="{ on }">
      <app-text-field v-model="model" v-bind="$attrs" :class="contentClass" :dense="dense" :readonly="readonly" :style="contentStyle" v-on="{ ...listeners, ...on }" @blur="onComplete" @click:append="menu = true" @keydown.enter="onComplete">
        <slot v-for="slot in slotKeys" :slot="slot" :name="slot" />
        <template v-for="slot in scopedSlotKeys" :slot="slot" slot-scope="scope">
          <slot v-bind="scope" :name="slot" />
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
