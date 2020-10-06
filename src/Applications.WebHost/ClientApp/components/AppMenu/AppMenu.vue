<script lang="ts">
import Vue from 'vue';
import { Listeners } from '~/types/vue';

export default Vue.extend({
  inheritAttrs: false,
  props: {
    closeOnContentClick: {
      default: false,
      type: Boolean,
    },
    value: {
      default: false,
      type: Boolean,
    },
  },
  data() {
    return {
      opend: this.value,
    };
  },
  computed: {
    listeners(): Listeners {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
  },
  watch: {
    opend(val: boolean): void {
      this.$emit('input', val);
    },
    value(val: boolean): void {
      this.opend = val;
    },
  },
  methods: {
    close(): void {
      this.opend = false;
    },
    open(): void {
      this.opend = true;
    },
  },
});
</script>

<template>
  <v-menu v-model="opend" v-bind="$attrs" :close-on-content-click="closeOnContentClick" v-on="listeners">
    <template v-slot>
      <slot v-bind="{ close, opend }" />
    </template>
    <template v-slot:activator="scope">
      <slot v-bind="{ ...scope, open, opend }" name="activator" />
    </template>
  </v-menu>
</template>
