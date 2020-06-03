<script>
import dayjs from 'dayjs';
export default {
  inheritAttrs: false,
  props: {
    dayFormat: {
      default(val) {
        return dayjs(val).date();
      },
      type: Function,
    },
    locale: {
      default: 'ja',
      type: String,
    },
  },
  computed: {
    activePicker: {
      /** @returns {String} */
      get() {
        return this.$refs.picker.activePicker;
      },
      /** @param {String} val */
      set(val) {
        this.$refs.picker.activePicker = val;
      },
    },
  },
};
</script>

<template>
  <v-date-picker ref="picker" v-bind="$attrs" :day-format="dayFormat" :locale="locale" v-on="$listeners">
    <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
    <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
      <slot v-bind="scope" :name="slot" />
    </template>
  </v-date-picker>
</template>
