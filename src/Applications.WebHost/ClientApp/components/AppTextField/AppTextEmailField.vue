<script lang="ts">
import { mdiOpenInNew } from '@mdi/js';
import { PropType } from 'vue';
import { VueBuilder } from '~/core/vue';
import { Slotable } from '~/mixins/slotable';

const Vue = VueBuilder.create() //
  .mixin(Slotable)
  .build();

export default Vue.extend({
  inheritAttrs: false,
  model: {
    event: 'input:value',
  },
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
      default: 'email',
      type: String,
    },
    value: {
      default: undefined,
      type: (null as any) as PropType<any>,
    },
  },
  data() {
    return {
      internalValue: this.value?.toString(),
    };
  },
  watch: {
    value(val: any): void {
      this.internalValue = val?.toString();
    },
  },
  methods: {
    onInputValue(val: any): void {
      this.internalValue = val?.toString();
    },
  },
});
</script>

<template>
  <app-text-field v-bind="$attrs" :type="type" :value="value" v-on="$listeners" @input:value="onInputValue">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
    <template v-slot:append>
      <v-btn :href="internalValue ? `mailto:${internalValue}` : null" icon small :tabindex="appendIconTabindex">
        <v-icon>
          {{ appendIcon }}
        </v-icon>
      </v-btn>
    </template>
  </app-text-field>
</template>
