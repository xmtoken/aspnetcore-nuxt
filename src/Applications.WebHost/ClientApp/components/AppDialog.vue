<script>
export default {
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
      type: undefined,
    },
  },
  data() {
    return {
      model: this.value,
    };
  },
  computed: {
    /** @returns {String} */
    classes() {
      if (this.left) {
        return 'v-dialog--position v-dialog--position-left';
      }
      if (this.right) {
        return 'v-dialog--position v-dialog--position-right';
      }
      return '';
    },
    /** @returns {String} */
    transition() {
      if (this.left) {
        return 'scroll-x-transition';
      }
      if (this.right) {
        return 'scroll-x-reverse-transition';
      }
      return '';
    },
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
  <v-dialog v-model="model" v-bind="$attrs" :content-class="classes" :transition="transition" v-on="$listeners">
    <template v-slot:activator="scope">
      <slot v-bind="scope" name="activator" />
    </template>
    <template v-slot>
      <slot v-bind="{ close }" />
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
