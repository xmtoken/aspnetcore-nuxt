<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  inheritAttrs: false,
  props: {
    closeOnContentClick: {
      default: false,
      type: Boolean,
    },
    value: {
      default: undefined,
      type: Boolean,
    },
  },
  data() {
    return {
      model: this.value,
    };
  },
  computed: {
    listeners(): Record<string, Function | Function[]> {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
  },
  watch: {
    model(val: boolean): void {
      this.$emit('input', val);
    },
    value(val: boolean): void {
      this.model = val;
    },
  },
  methods: {
    close(): void {
      this.model = false;
    },
    open(): void {
      this.model = true;
    },
  },
});
</script>

<template>
  <v-menu v-model="model" v-bind="$attrs" :close-on-content-click="closeOnContentClick" v-on="listeners">
    <template v-slot>
      <slot v-bind="{ close }" />
    </template>
    <template v-slot:activator="scope">
      <slot v-bind="{ ...scope, open, opend: model }" name="activator" />
    </template>
  </v-menu>
</template>
