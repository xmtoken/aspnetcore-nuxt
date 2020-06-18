<script>
import AppTextField from './AppTextField';
import { Slotable } from '~/mixins';
export default {
  components: {
    AppTextField,
  },
  mixins: [
    //
    Slotable,
  ],
  inheritAttrs: false,
  props: {
    autocomplete: {
      default: 'username',
      type: String,
    },
    type: {
      default: 'email',
      type: String,
    },
  },
};
</script>

<template>
  <app-text-field v-bind="$attrs" :autocomplete="autocomplete" :type="type" v-on="$listeners">
    <slot v-for="slot in slotKeys" :slot="slot" :name="slot" />
    <template v-for="slot in scopedSlotKeys" :slot="slot" slot-scope="scope">
      <slot v-bind="scope" :name="slot" />
    </template>
  </app-text-field>
</template>
