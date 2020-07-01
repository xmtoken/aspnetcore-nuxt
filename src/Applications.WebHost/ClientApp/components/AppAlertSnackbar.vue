<script>
import { mdiAlert, mdiCheckCircle } from '@mdi/js';
export default {
  inheritAttrs: false,
  data() {
    return {
      messages: [],
      snackbar: false,
      type: null,
    };
  },
  computed: {
    /** @returns {String} */
    icon() {
      if (this.$device.isMobile) {
        return undefined;
      }
      return this.type === 'success' ? mdiCheckCircle : mdiAlert;
    },
    /** @returns {Number} */
    timeout() {
      return this.type === 'success' ? 5000 : -1;
    },
  },
  watch: {
    messages(val) {
      this.snackbar = val && val.length > 0;
    },
  },
  methods: {
    close() {
      this.snackbar = false;
      this.messages = [];
    },
    setMessages(type, messages) {
      this.type = type;
      this.messages = messages ? (Array.isArray(messages) ? messages : [messages]) : [];
    },
    setSuccesses(messages) {
      this.setMessages('success', messages);
    },
    setInformations(messages) {
      this.setMessages('info', messages);
    },
    setWarnings(messages) {
      this.setMessages('warning', messages);
    },
    setErrors(messages) {
      this.setMessages('error', messages);
    },
    setValidationErrors(errors) {
      const messages = errors ? errors['']?.flatMap(message => message) : [];
      this.setErrors(messages);
    },
    setHasInputErrorsMessage() {
      this.setErrors('入力内容に誤りがあります。');
    },
  },
};
</script>

<template>
  <v-slide-y-transition>
    <v-snackbar v-model="snackbar" v-bind="$attrs" class="pa-0" color="white" :timeout="timeout">
      <v-alert class="mx-n4 my-n2" :color="type" dismissible :icon="icon" outlined text @input="close">
        <div v-for="(message, i) in messages" :key="i">
          {{ message }}
        </div>
      </v-alert>
    </v-snackbar>
  </v-slide-y-transition>
</template>

<style lang="scss" scoped>
.v-snack ::v-deep {
  .v-alert {
    box-sizing: content-box;
    width: inherit;

    .v-alert__dismissible {
      margin-left: 16px;
      padding: 2px;
    }
  }
}
</style>
