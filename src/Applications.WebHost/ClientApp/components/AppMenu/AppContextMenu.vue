<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  inheritAttrs: false,
  data() {
    return {
      positionX: 0 as number,
      positionY: 0 as number,
      value: false,
    };
  },
  computed: {
    on(): object {
      return {
        contextmenu: this.onContextmenu,
        'contextmenu:row': this.onContextmenu,
      };
    },
  },
  methods: {
    onContextmenu(event: MouseEvent): void {
      event.preventDefault();
      this.positionX = event.clientX;
      this.positionY = event.clientY;
      this.value = true;
    },
  },
});
</script>

<template>
  <app-menu v-model="value" v-bind="$attrs" absolute :position-x="positionX" :position-y="positionY" v-on="$listeners">
    <template v-slot="scope">
      <slot v-bind="scope" />
    </template>
    <template v-slot:activator="scope">
      <slot v-bind="{ ...scope, on }" name="activator" />
    </template>
  </app-menu>
</template>
