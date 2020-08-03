<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  methods: {
    test() {
      this.$store.dispatch('notification/add-success', { content: '期首計画の登録が完了しました。', timeout: -1 });
    },
  },
});
</script>

<template>
  <div style="width: 300px;">
    <v-btn @click="test">
      xx
    </v-btn>
    <app-date-field picker-offset-y value="1991-10-01" />
    <app-date-field picker-offset-left :picker-props="{ type: 'month' }" />
    <app-birthday-field picker-offset-y value="1991-10-01" />
    <app-time-field picker-offset-y value="23:05" />
    <app-time-field picker-offset-y :picker-props="{ useSeconds: true }" />
  </div>
</template>
