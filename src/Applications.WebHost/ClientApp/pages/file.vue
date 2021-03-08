<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  data() {
    return {
      value: '' as string,
      file: null as any,
    };
  },
  watch: {
    //
  },
  methods: {
    post(): void {
      const form = new FormData();
      // form.append(
      //   'model',
      //   JSON.stringify({
      //     value: this.value,
      //     nestNest: {
      //       value: 'nestval',
      //     },
      //   })
      // );
      form.append(
        'model',
        JSON.stringify([
          {
            value: '',
            nestNest: {
              value: '',
            },
          },
          {
            value: '',
            nestNest: {
              value: '',
            },
          },
        ])
      );
      form.append('file', this.file);
      this.$axios.post('/test', form);
    },
  },
});
</script>

<template>
  <div>
    <v-btn @click="post">
      post
    </v-btn>
    <app-scope v-slot>
      <app-text-field v-model="value" />
    </app-scope>
    <app-file-input v-model="file" />
  </div>
</template>
