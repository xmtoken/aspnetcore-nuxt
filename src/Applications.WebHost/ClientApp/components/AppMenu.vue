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
  watch: {
    model(val) {
      this.$emit('change', val);
    },
    value(val) {
      this.model = val;
    },
  },
  methods: {
    close() {
      this.model = false;
    },
  },
};
</script>

<template>
  <v-menu v-model="model" v-bind="$attrs" :close-on-content-click="closeOnContentClick" v-on="$listeners">
    <template v-slot:activator="scope">
      <slot v-bind="scope" name="activator" />
    </template>
    <template v-slot>
      <slot v-bind="{ close }" />
    </template>
  </v-menu>
</template>
