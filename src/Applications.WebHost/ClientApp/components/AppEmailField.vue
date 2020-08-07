<script lang="ts">
import { mdiOpenInNew } from '@mdi/js';
import mixins from '~/extensions/mixins';
import slotable from '~/mixins/slotable';

export default mixins(slotable).extend({
  inheritAttrs: false,
  props: {
    appendIconTabindex: {
      default: -1,
      type: Number,
    },
    type: {
      default: 'email',
      type: String,
    },
    value: {
      default: undefined,
      type: String,
    },
  },
  data() {
    return {
      icons: {
        mdiOpenInNew,
      },
    };
  },
});
</script>

<template>
  <app-text-field v-bind="$attrs" :type="type" v-on="$listeners">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
    <template v-slot:append>
      <v-btn :href="value ? `mailto:${value}` : null" icon small :tabindex="appendIconTabindex">
        <v-icon>
          {{ icons.mdiOpenInNew }}
        </v-icon>
      </v-btn>
    </template>
  </app-text-field>
</template>
