<script lang="ts">
import Vue, { PropType } from 'vue';

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
    listeners(): Record<string, Function | Function[]> {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
    transitions(): string {
      if (this.left) {
        return 'scroll-x-transition';
      }
      if (this.right) {
        return 'scroll-x-reverse-transition';
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
  <v-dialog v-model="model" v-bind="$attrs" :content-class="contentClass" :transition="transitions" v-on="listeners">
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
