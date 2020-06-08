<script>
import AppDatePicker from './AppDatePicker';
import AppTextField from './AppTextField';
import dayjs from 'dayjs';
export default {
  components: {
    AppDatePicker,
    AppTextField,
  },
  inheritAttrs: false,
  props: {
    contentClass: {
      default: undefined,
      type: String,
    },
    contentStyle: {
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
    type: {
      default: 'date',
      type: String,
      validator(val) {
        return ['date', 'month'].includes(val);
      },
    },
    value: {
      default: undefined,
      type: [Array, String],
    },
  },
  data() {
    return {
      menu: false,
      date: this.applyFormat(this.value),
    };
  },
  computed: {
    model: {
      /** @returns {Array|String} */
      get() {
        return this.value;
      },
      /** @param {Array|String} val */
      set(val) {
        this.$emit('input', val);
      },
    },
    /** @returns {String} */
    classes() {
      return this.menuOffsetY ? 'v-menu__content--offset-y' : undefined;
    },
    /** @returns {Number} */
    menuNudgeLeft() {
      return this.menuOffsetLeft ? 290 /*menu width*/ + 5 /*space*/ : 0;
    },
    /** @returns {Object} */
    listeners() {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
  },
  watch: {
    model(val) {
      this.date = this.applyFormat(val);
    },
  },
  methods: {
    applyFormat(val) {
      return !!val && dayjs(val).isValid() ? dayjs(val).format(this.type === 'date' ? 'YYYY-MM-DD' : 'YYYY-MM') : null;
    },
    onComplete() {
      if (this.date) {
        this.model = this.date;
      }
    },
    onPickerChange(val) {
      this.menu = false;
      this.model = val;
    },
  },
};
</script>

<template>
  <v-menu v-model="menu" :close-on-content-click="false" :content-class="classes" min-width="inherit" :nudge-left="menuNudgeLeft" :offset-y="menuOffsetY">
    <template v-slot:activator="{ on }">
      <app-text-field v-model="model" v-bind="$attrs" :class="contentClass" :style="contentStyle" v-on="{ ...listeners, ...on }" @blur="onComplete" @keydown.enter="onComplete">
        <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
        <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
          <slot v-bind="scope" :name="slot" />
        </template>
      </app-text-field>
    </template>
    <app-date-picker v-model="date" :type="type" @change="onPickerChange" />
  </v-menu>
</template>

<style lang="scss" scoped>
.v-menu__content--offset-y ::v-deep {
  margin-top: -21px;
}
</style>
