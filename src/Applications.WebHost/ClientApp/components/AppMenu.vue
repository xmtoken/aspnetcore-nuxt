<script>
export default {
  inheritAttrs: false,
  props: {
    closeOnContentClick: {
      default: false,
      type: Boolean,
    },
    value: {
      default: undefined,
      type: undefined,
    },
  },
  data() {
    return {
      model: this.value,
    };
  },
  computed: {
    /** @returns {Object} */
    listeners() {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
  },
  watch: {
    model(val) {
      this.$emit('input', val);
    },
    value(val) {
      this.model = val;
    },
  },
  methods: {
    close() {
      this.model = false;
    },
    open() {
      this.model = true;
    },
  },
};
</script>

<template>
  <v-menu v-model="model" v-bind="$attrs" :close-on-content-click="closeOnContentClick" v-on="listeners">
    <template v-slot:activator="scope">
      <slot v-bind="{ ...scope, open }" name="activator" />
    </template>
    <template v-slot>
      <slot v-bind="{ close }" />
    </template>
  </v-menu>
</template>
