<script>
export default {
  inheritAttrs: false,
  props: {
    closeOnContentClick: {
      default: false,
      type: Boolean,
    },
  },
  data() {
    return {
      menu: false,
    };
  },
  methods: {
    close() {
      this.menu = false;
    },
  },
};
</script>

<template>
  <v-menu v-model="menu" v-bind="$attrs" :close-on-content-click="closeOnContentClick" v-on="$listeners">
    <template v-slot:activator="scope">
      <slot v-bind="scope" name="activator" />
    </template>
    <template v-slot>
      <slot v-bind="{ close }" />
    </template>
  </v-menu>
</template>
