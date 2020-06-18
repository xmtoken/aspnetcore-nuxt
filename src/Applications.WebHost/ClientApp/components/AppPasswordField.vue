<script>
import AppTextField from './AppTextField';
import { Slotable } from '~/mixins';
import { mdiEye, mdiEyeOff } from '@mdi/js';
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
    appendIconTabindex: {
      default: -1,
      type: Number,
    },
    autocomplete: {
      default: 'current-password',
      type: String,
    },
  },
  data() {
    return {
      visible: false,
    };
  },
  computed: {
    /** @returns {String} */
    icon() {
      return this.visible ? mdiEye : mdiEyeOff;
    },
    /** @returns {String} */
    type() {
      return this.visible ? 'text' : 'password';
    },
  },
};
</script>

<template>
  <app-text-field v-bind="$attrs" :append-icon="icon" :append-icon-tabindex="appendIconTabindex" :autocomplete="autocomplete" :type="type" v-on="$listeners" @click:append="visible = !visible">
    <slot v-for="slot in slotKeys" :slot="slot" :name="slot" />
    <template v-for="slot in scopedSlotKeys" :slot="slot" slot-scope="scope">
      <slot v-bind="scope" :name="slot" />
    </template>
  </app-text-field>
</template>
