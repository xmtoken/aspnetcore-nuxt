<script lang="ts">
import dayjs from 'dayjs';
import Vue, { VueConstructor } from 'vue';
import { VDatePicker } from 'vuetify/src/components';
import mixins from '~/extensions/mixins';
import slotable from '~/mixins/slotable';

const $refs = Vue as VueConstructor<
  Vue & {
    $refs: {
      picker: InstanceType<typeof VDatePicker>;
    };
  }
>;

export default mixins($refs, slotable).extend({
  inheritAttrs: false,
  props: {
    dayFormat: {
      default(val: string): string {
        return dayjs(val).date().toString();
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
      get(): string {
        return this.$refs.picker.activePicker;
      },
      set(val: string): void {
        this.$refs.picker.activePicker = val;
      },
    },
  },
});
</script>

<template>
  <v-date-picker ref="picker" v-bind="$attrs" :day-format="dayFormat" :locale="locale" v-on="$listeners">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
  </v-date-picker>
</template>
