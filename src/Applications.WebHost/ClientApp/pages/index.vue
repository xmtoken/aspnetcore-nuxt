<script lang="ts">
import Vue from 'vue';
import mixins from '~/extensions/mixins';
import responsive from '~/mixins/responsive';

export default mixins(responsive).extend({
  data() {
    return {
      postalCode: '079-0177' as string | null,
      address: null as string | null,
      numeral: 1234.5 as string | null,
    };
  },
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
    <app-number-field v-model="numeral" format="0,0.0" />
    <app-number-field v-model="numeral" format="0,0.0" />
    <app-postal-code-field v-model="postalCode" :address.sync="address" menu-offset-y />
    <app-text-field v-model="address" />
    <app-date-field picker-offset-y value="1991-10-01" />
    <app-date-field picker-offset-left :picker-props="{ type: 'month' }" />
    <app-birthday-field picker-offset-y value="1991-10-01" />
    <app-time-field picker-offset-y value="23:05" />
    <app-time-field picker-offset-y :picker-props="{ useSeconds: true }" />
  </div>
</template>
