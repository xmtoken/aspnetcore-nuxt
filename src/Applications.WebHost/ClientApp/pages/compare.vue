<script lang="ts">
import Vue from 'vue';
import { valueComparatorAsString } from '~/core/comparator';

export default Vue.extend({
  data() {
    return {
      checkbox_val: null as any,
      switch_val: null as any,
      radio_val: null as any,
    };
    // return {
    //   checkbox_val: 'true' as any,
    //   switch_val: 'true' as any,
    //   radio_val: '2' as any,
    // };
  },
  methods: {
    valueComparatorAsString,
    set(): void {
      this.checkbox_val = 'true';
      this.switch_val = 'true';
      this.radio_val = '2';
    },
  },
});
</script>

<template>
  <div>
    <v-btn @click="set">
      set
    </v-btn>
    <app-checkbox v-model="checkbox_val" :label="String(checkbox_val)" :value-converter="x => Boolean(x)" />
    <app-switch v-model="switch_val" :label="String(switch_val)" :value-converter="x => Boolean(x)" />
    <app-radio-group v-model="radio_val" :label="String(radio_val)" :value-converter="x => Number(x)">
      <v-radio label="radio1" :value="1" />
      <v-radio label="radio2" :value="2" />
      <v-radio label="radio3" :value="3" />
    </app-radio-group>
  </div>
</template>
