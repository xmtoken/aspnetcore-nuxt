<script lang="ts">
import { mdiOpenInNew } from '@mdi/js';
import { PropType } from 'vue';
import mixins from '~/extensions/mixins';
import slotable from '~/mixins/slotable';

export default mixins(slotable).extend({
  inheritAttrs: false,
  props: {
    appendIcon: {
      default: mdiOpenInNew,
      type: String,
    },
    appendIconTabindex: {
      default: -1,
      type: Number,
    },
    type: {
      default: 'url',
      type: String,
    },
    value: {
      default: undefined,
      type: (null as any) as PropType<any>,
    },
  },
  data() {
    return {
      internalValue: this.value,
    };
  },
  methods: {
    onUpdateInternalValue(val: any): void {
      this.internalValue = val;
    },
  },
});
</script>

<template>
  <app-text-field v-bind="$attrs" :type="type" :value="value" v-on="$listeners" @update:internal-value="onUpdateInternalValue">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
    <template v-slot:append>
      <v-btn :href="internalValue" icon small :tabindex="appendIconTabindex">
        <v-icon>
          {{ appendIcon }}
        </v-icon>
      </v-btn>
    </template>
  </app-text-field>
</template>
