<script>
import AppTextField from './AppTextField';
export default {
  components: {
    AppTextField,
  },
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
    <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
    <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
      <slot v-bind="scope" :name="slot" />
    </template>
  </app-text-field>
</template>
