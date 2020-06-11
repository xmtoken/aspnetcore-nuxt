<script>
import AppTextField from './AppTextField';
import { mdiClockOutline } from '@mdi/js';
import dayjs from 'dayjs';
export default {
  components: {
    AppTextField,
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
    openOnClick: {
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
      model: this.value,
    };
  },
  computed: {
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
    time: {
      /** @returns {Any} */
      get() {
        const datetime = `0001/01/01 ${this.model}`;
        if (this.model && dayjs(datetime).isValid()) {
          return dayjs(datetime).format(this.useSeconds ? 'HH:mm:ss' : 'HH:mm');
        }
        return null;
      },
      /** @param {Any} val */
      set(val) {
        this.model = val;
      },
    },
  },
  watch: {
    model(val) {
      this.$emit('input', val);
    },
    value(val) {
      this.model = val;
    },
  },
  methods: {
    onComplete() {
      if (this.time) {
        this.model = this.time;
      }
    },
  },
};
</script>

<template>
  <v-menu v-model="menu" :close-on-content-click="closeOnContentClick" :content-class="menuClasses" min-width="inherit" :nudge-left="menuNudgeLeft" :offset-y="menuOffsetY" :open-on-click="openOnClick">
    <template v-slot:activator="{ on }">
      <app-text-field v-model="model" v-bind="$attrs" :append-icon="appendIcon" :append-icon-tabindex="appendIconTabindex" :class="contentClass" :style="contentStyle" v-on="{ ...listeners, ...on }" @blur="onComplete" @click:append="menu = true" @keydown.enter="onComplete">
        <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
        <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
          <slot v-bind="scope" :name="slot" />
        </template>
      </app-text-field>
    </template>
    <v-time-picker v-if="menu" v-model="time" :format="format" :use-seconds="useSeconds" @change="menu = false" />
  </v-menu>
</template>

<style lang="scss" scoped>
.v-menu__content--offset-y ::v-deep {
  margin-top: -21px;
}
</style>
