<script lang="ts">
import Vue from 'vue';
import AppAutocomplete from '~/components/AppAutocomplete/AppAutocomplete.vue';
import AppCombobox from '~/components/AppCombobox/AppCombobox.vue';
import AppSelect from '~/components/AppSelect/AppSelect.vue';

export default Vue.extend({
  components: { AppAutocomplete, AppCombobox, AppSelect },
  data() {
    return {
      text: '',
      val: 111 as number | null,
      items: [
        { text: 'text1', value: 1 },
        { text: 'text2', value: 2 },
      ],
      errors: [],
    };
  },
  watch: {
    //
  },
  methods: {
    onchange(x, y): void {
      console.log(x);
      console.log(y);
    },
    click() {
      this.val = 2;
    },
  },
});
</script>

<template>
  <div>
    <v-form ref="form">
      <v-btn @click="click">
        set
      </v-btn>
      <app-scope v-slot>
        <app-textarea v-model="text" :error-messages="['aaaa', 'bbb']" vee-rules="required" />
      </app-scope>
      {{ errors.length }}
      <app-scope v-slot>
        <app-autocomplete v-model="val" clearable :items="items" @change="onchange" />
      </app-scope>
      {{ `val:${val}` }}
    </v-form>
  </div>
</template>
