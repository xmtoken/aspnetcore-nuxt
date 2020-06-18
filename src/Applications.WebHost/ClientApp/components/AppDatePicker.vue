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
    <slot v-for="slot in slotKeys" :slot="slot" :name="slot" />
    <template v-for="slot in scopedSlotKeys" :slot="slot" slot-scope="scope">
      <slot v-bind="scope" :name="slot" />
    </template>
  </v-date-picker>
</template>
