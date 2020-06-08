<script>
import AppTextField from './AppTextField';
import dayjs from 'dayjs';
export default {
  components: {
    AppTextField,
  },
  inheritAttrs: false,
  props: {
    fileldClass: {
      default: undefined,
      type: String,
    },
    fileldStyle: {
      default: undefined,
      type: String,
    },
    format: {
      default: '24hr',
      type: String,
      validator(val) {
        return ['24hr', 'ampm'].includes(val);
      },
    },
    menuOffsetLeft: {
      default: false,
      type: Boolean,
    },
    menuOffsetY: {
      default: false,
      type: Boolean,
    },
    useSeconds: {
      default: false,
      type: Boolean,
    },
    value: {
      default: undefined,
      type: undefined,
    },
  },
  data() {
    return {
      menu: false,
      time: this.applyFormat(this.value),
    };
  },
  computed: {
    model: {
      /** @returns {Any} */
      get() {
        return this.value;
      },
      /** @param {Any} val */
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
      this.time = this.applyFormat(val);
    },
  },
  methods: {
    applyFormat(val) {
      const datetime = `0001/01/01 ${val}`;
      return !!val && dayjs(datetime).isValid() ? dayjs(datetime).format(this.useSeconds ? 'HH:mm:ss' : 'HH:mm') : null;
    },
    onComplete() {
      if (this.time) {
        this.model = this.time;
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
      <app-text-field v-model="model" v-bind="$attrs" :class="fileldClass" :style="fileldStyle" v-on="{ ...listeners, ...on }" @blur="onComplete" @keydown.enter="onComplete">
        <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
        <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
          <slot v-bind="scope" :name="slot" />
        </template>
      </app-text-field>
    </template>
    <v-time-picker v-if="menu" v-model="time" :format="format" :use-seconds="useSeconds" @change="onPickerChange" />
  </v-menu>
</template>

<style lang="scss" scoped>
.v-menu__content--offset-y ::v-deep {
  margin-top: -21px;
}
</style>
