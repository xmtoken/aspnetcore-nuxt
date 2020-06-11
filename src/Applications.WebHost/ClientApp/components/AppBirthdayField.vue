<script>
import AppDatePicker from './AppDatePicker';
import AppTextField from './AppTextField';
import { mdiCalendar } from '@mdi/js';
import dayjs from 'dayjs';
export default {
  components: {
    AppDatePicker,
    AppTextField,
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
    autocomplete: {
      default: 'bday',
      type: String,
    },
    closeOnContentClick: {
      default: false,
      type: Boolean,
    },
    contentClass: {
      default: undefined,
      type: String,
    },
    contentStyle: {
      default: undefined,
      type: String,
    },
    max: {
      default() {
        return dayjs().format('YYYY-MM-DD');
      },
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
      model: this.value,
    };
  },
  computed: {
    date: {
      /** @returns {Array|String} */
      get() {
        if (this.model && dayjs(this.model).isValid()) {
          return dayjs(this.model).format(this.type === 'date' ? 'YYYY-MM-DD' : 'YYYY-MM');
        }
        return null;
      },
      /** @param {Array|String} val */
      set(val) {
        this.model = val;
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
      return this.menuOffsetY ? 'v-menu__content--offset-y' : undefined;
    },
    /** @returns {Number} */
    menuNudgeLeft() {
      return this.menuOffsetLeft ? 290 /*menu width*/ + 5 /*space*/ : 0;
    },
  },
  watch: {
    menu(val) {
      if (val) {
        this.$nextTick(() => (this.$refs.picker.activePicker = 'YEAR'));
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
        this.model = this.date;
      }
    },
  },
};
</script>

<template>
  <v-menu ref="menu" v-model="menu" :close-on-content-click="closeOnContentClick" :content-class="menuClasses" min-width="inherit" :nudge-left="menuNudgeLeft" :offset-y="menuOffsetY" :open-on-click="openOnClick">
    <template v-slot:activator="{ on }">
      <app-text-field v-model="model" v-bind="$attrs" :append-icon="appendIcon" :append-icon-tabindex="appendIconTabindex" :class="contentClass" :style="contentStyle" v-on="{ ...listeners, ...on }" @blur="onComplete" @click:append="menu = true" @keydown.enter="onComplete">
        <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
        <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
          <slot v-bind="scope" :name="slot" />
        </template>
      </app-text-field>
    </template>
    <app-date-picker ref="picker" v-model="date" :max="max" :type="type" @change="menu = false" />
  </v-menu>
</template>

<style lang="scss" scoped>
.v-menu__content--offset-y ::v-deep {
  margin-top: -21px;
}
</style>
