<script>
export default {
  inheritAttrs: false,
  props: {
    notifications: {
      default: undefined,
      type: Array,
    },
  },
};
</script>

<template>
  <v-slide-x-reverse-transition class="notifications" group>
    <v-snackbar v-for="notification in notifications" :key="notification.key" v-bind="$attrs" color="white" :timeout="0" :value="true" v-on="$listeners">
      <v-alert :color="notification.color" outlined text :type="notification.type">
        <template v-if="notification.isHtml">
          <!-- eslint-disable-next-line vue/no-v-html -->
          <span v-html="notification.content" />
        </template>
        <template v-else>
          {{ notification.content }}
        </template>
      </v-alert>
    </v-snackbar>
  </v-slide-x-reverse-transition>
</template>

<style lang="scss" scoped>
.notifications ::v-deep {
  align-items: flex-end;
  display: flex;
  flex-direction: column;
  height: 100vh;
  justify-content: flex-end;
  padding: 4px 0 4px 12px;
  position: absolute;
  right: 0;

  .v-snack {
    margin-right: 16px;
    position: relative;

    & + .v-snack {
      margin-top: 8px;
    }

    .v-snack__wrapper {
      min-width: auto;

      .v-alert {
        box-sizing: content-box;
        margin: -8px -16px;
        width: inherit;
      }
    }
  }
}
</style>
