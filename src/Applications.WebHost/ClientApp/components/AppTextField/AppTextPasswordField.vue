<script lang="ts">
import { mdiEye, mdiEyeOff } from '@mdi/js';
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
    appendIconTabindex: {
      default: -1,
      type: Number,
    },
  },
  data() {
    return {
      visible: false,
    };
  },
  computed: {
    appendIcon(): string {
      return this.visible ? mdiEye : mdiEyeOff;
    },
    type(): string {
      return this.visible ? 'text' : 'password';
    },
  },
});
</script>

<template>
  <app-text-field v-bind="$attrs" :append-icon="appendIcon" :append-icon-tabindex="appendIconTabindex" :type="type" v-on="$listeners" @click:append="visible = !visible">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
  </app-text-field>
</template>
