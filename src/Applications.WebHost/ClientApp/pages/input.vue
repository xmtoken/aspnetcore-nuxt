<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  data() {
    return {
      check: null as boolean | null,
      radio: null as number | null,
      text: null as string | null,
    };
  },
  watch: {
    //
  },
  methods: {
    post(): void {
      console.log('check', this.check);
      console.log('radio', this.radio);
    },
    valueComparator(): boolean {
      console.log('valueComparator');
      return true;
    },
    changeRadioValue(): void {
      this.radio = this.radio && this.radio === 1 ? 0 : 1;
    },
  },
});
</script>

<template>
  <div>
    <!-- <v-btn @click="post">
      log
    </v-btn>
    <v-btn @click="[$refs.form.reset()]">
      reset
    </v-btn> -->
    <v-form ref="form">
      <app-scope v-slot>
        <app-text-number-field v-model="text" class="x" format="#,0" label="label" picker-offset-y />
      </app-scope>
      <app-scope v-slot>
        <app-text-number-field v-model="radio" class="x" format="#,0" label="label" picker-offset-y />
      </app-scope>
      <!-- <app-scope v-slot>
        <app-text-date-field v-model="text" class="x" label="label" picker-offset-y />
      </app-scope> -->
      <!-- <app-scope v-slot>
        <app-text-time-field v-model="text" label="label" />
      </app-scope> -->
      <!-- <app-scope v-slot>
        <app-text-birthday-field v-model="check" />
      </app-scope> -->
      <v-btn @click="changeRadioValue">
        changeRadioValue
      </v-btn>
      <app-radio-group v-model="radio" :value-comparator="valueComparator">
        <v-radio label="val0" :value="0" :value-comparator="valueComparator" />
        <v-radio label="val1" :value="1" :value-comparator="valueComparator" />
      </app-radio-group>
    </v-form>
  </div>
</template>
