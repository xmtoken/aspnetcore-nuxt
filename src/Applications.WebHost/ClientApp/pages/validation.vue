<script lang="ts">
import { validate } from 'vee-validate';
import Vue from 'vue';

export default Vue.extend({
  data() {
    return {
      val01: null as any,
      val02: null as any,
    };
  },
  computed: {
    rules(): object {
      return {
        val01: {
          rules: {
            required: true,
          },
          name: '',
        },
        val02: {
          rules: {
            required: true,
          },
          name: '',
        },
      };
    },
  },
  methods: {
    async validate(): Promise<void> {
      const result = await validate(this.val01, this.rules);
      console.log(result);
    },
  },
});
</script>

<template>
  <div>
    <v-btn @click="validate">
      validate
    </v-btn>
    <app-scope v-slot>
      <app-text-field v-model="val01" />
    </app-scope>
    <app-scope v-slot>
      <app-text-field v-model="val02" />
    </app-scope>
  </div>
</template>
