<script lang="ts">
import dayjs from 'dayjs';
import { VDatePicker } from 'vuetify/src/components';
import { VueBuilder } from '~/core/vue';
import { Slotable } from '~/mixins/slotable';

type ComponentProps = Record<string, any> & {
  dayFormat?: (val: string) => string;
  locale?: string;
};

type ComponentRefs = {
  picker: InstanceType<typeof VDatePicker>;
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProps>()
  .$refs<ComponentRefs>()
  .mixin(Slotable)
  .build();

export default Vue.extend({
  inheritAttrs: false,
  computed: {
    activePicker: {
      get() {
        return this.$refs.picker.activePicker;
      },
      set(val: string) {
        this.$refs.picker.activePicker = val;
      },
    },
    props() {
      const defaults: ComponentProps = {
        dayFormat: val => dayjs(val).date().toString(),
        locale: 'ja',
      };
      const attrs: ComponentProps = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProps = {
        //
      };
      return {
        ...attrs,
        ...overrides,
      };
    },
  },
});
</script>

<template>
  <v-date-picker ref="picker" v-bind="props" v-on="$listeners">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
  </v-date-picker>
</template>
