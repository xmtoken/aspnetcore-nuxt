<script lang="ts">
import Vue, { PropType } from 'vue';
import { Notification } from '~/store/notification';

export default Vue.extend({
  inheritAttrs: false,
  props: {
    notifications: {
      default(): Notification[] {
        return [];
      },
      type: Array as PropType<Notification[]>,
    },
  },
  methods: {
    clear(key: string): void {
      this.$store.commit('notification/remove', key);
    },
  },
});
</script>

<template>
  <v-slide-x-reverse-transition class="notifications" group>
    <v-snackbar v-for="notification in notifications" :key="notification.key" v-bind="$attrs" color="white" :timeout="-1" :value="true" v-on="$listeners">
      <v-alert class="cursor-pointer" :color="notification.color" outlined text :type="notification.type" @click="clear(notification.key)">
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
  pointer-events: none;
  position: absolute;
  right: 0;

  .v-snack {
    height: auto;
    padding: 0 !important;
    position: relative;
    width: auto;

    & + .v-snack {
      margin-top: -8px;
    }

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
}
</style>
