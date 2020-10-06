<script lang="ts">
import mixins from '~/extensions/mixins';
import slotable from '~/mixins/slotable';

export default mixins(slotable).extend({
  inheritAttrs: false,
  model: {
    event: 'input:value',
  },
  props: {
    type: {
      default: 'email',
      type: String,
    },
  },
});
</script>

<template>
  <app-text-field v-bind="$attrs" :type="type" v-on="$listeners">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
  </app-text-field>
</template>
