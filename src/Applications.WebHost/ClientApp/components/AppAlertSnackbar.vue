<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  inheritAttrs: false,
  props: {
    color: {
      default: undefined,
      type: String,
    },
    icon: {
      default: undefined,
      type: [Boolean, String],
    },
    type: {
      default: undefined,
      type: String,
    },
  },
  data() {
    return {
      message: null as string | null,
      snackbar: false,
    };
  },
  methods: {
    close(): void {
      this.snackbar = false;
    },
    setErrors(errors: Record<string, string[]>, messageIfErrorsEmpty: string = '入力内容のエラーを確認してください。'): void {
      const message = errors ? errors['']?.join() ?? messageIfErrorsEmpty : null;
      this.setMessage(message);
    },
    setMessage(message: string | null): void {
      this.message = message;
      this.snackbar = !!this.message;
    },
  },
});
</script>

<template>
  <v-snackbar v-model="snackbar" v-bind="$attrs" color="white" v-on="$listeners">
    <v-alert v-model="snackbar" :color="color" dismissible :icon="icon" outlined text :type="type">
      {{ message }}
    </v-alert>
  </v-snackbar>
</template>

<style lang="scss" scoped>
.v-snack ::v-deep {
  .v-snack__wrapper {
    min-width: auto;

    .v-snack__content {
      padding: 0;

      .v-alert {
        margin: 0;
      }
    }

    .v-snack__action {
      display: none;
    }
  }
}
</style>
