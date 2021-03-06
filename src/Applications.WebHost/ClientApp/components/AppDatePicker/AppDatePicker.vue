<script lang="ts">
import dayjs from 'dayjs';
import { VDatePicker } from 'vuetify/src/components';
import { VueBuilder } from '~/core/vue';
import { Slotable } from '~/mixins/slotable';
import { ProxyProps } from '~/types/global';

type ComponentProxyProps = ProxyProps & {
  dayFormat?: ((val: string) => string) | null;
  multiple?: boolean | null;
  range?: boolean | null;
  type?: 'date' | 'month' | null;
  value?: string | string[] | null;
};

type ComponentProps = ComponentProxyProps & {
  //
};

export type AppDatePickerProps = ComponentProps;

type ComponentRefs = {
  picker: InstanceType<typeof VDatePicker>;
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProxyProps>()
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
      const defaults: ComponentProxyProps = {
        dayFormat: val => dayjs(val).date().toString(),
      };
      const attrs: ComponentProxyProps & Record<string, any> = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProxyProps = {
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
