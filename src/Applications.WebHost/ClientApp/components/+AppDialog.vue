<script lang="ts">
import Vue from 'vue';
import { Listeners } from '~/types/vue';

export default Vue.extend({
  inheritAttrs: false,
  props: {
    left: {
      default: false,
      type: Boolean,
    },
    right: {
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
    contentClass(): string | null {
      if (this.left) {
        return 'v-dialog--position v-dialog--position-left';
      }
      if (this.right) {
        return 'v-dialog--position v-dialog--position-right';
      }
      return null;
    },
    listeners(): Listeners {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
    transition(): string {
      if (this.left) {
        return this.model ? 'scroll-x-transition' : 'scroll-x-reverse-transition';
      }
      if (this.right) {
        return this.model ? 'scroll-x-reverse-transition' : 'scroll-x-transition';
      }
      return 'dialog-transition';
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
  <v-dialog v-model="model" v-bind="$attrs" :content-class="contentClass" :transition="transition" v-on="listeners">
    <template v-slot>
      <slot v-bind="{ close }" />
    </template>
    <template v-slot:activator="scope">
      <slot v-bind="{ ...scope, open, opend: model }" name="activator" />
    </template>
  </v-dialog>
</template>

<style lang="scss" scoped>
.v-dialog__content ::v-deep {
  .v-dialog--position {
    border-radius: 0;
    margin: 0;
    max-height: 100%;
    min-height: 100%;
    position: fixed;

    &.v-dialog--position-left {
      left: 0;
    }

    &.v-dialog--position-right {
      right: 0;
    }

    .v-card {
      border-radius: 0;

      .v-card__text {
        padding: 16px;
      }
    }
  }
}
</style>
