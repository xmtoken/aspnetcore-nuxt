<script lang="ts">
import { mdiTextRecognition } from '@mdi/js';
import { ValidationObserver } from 'vee-validate';
import Vue from 'vue';
import AppRadioGroup from '~/components/AppRadioGroup/AppRadioGroup.vue';
import AppScope from '~/components/AppScope/AppScope.vue';
import AppTextField from '~/components/AppTextField/AppTextField.vue';

export default Vue.extend({
  components: { AppRadioGroup, AppTextField, AppScope, ValidationObserver },
  data() {
    return {
      value: null as any,
      icon: mdiTextRecognition,
    };
  },
});
</script>

<template>
  <v-form autocomplete="off" class="component">
    <div>default</div>
    <v-row>
      <v-col>
        <app-autocomplete :append-icon="icon" :append-outer-icon="icon" label="autocomplete" :prepend-icon="icon" :prepend-inner-icon="icon" />
      </v-col>
      <v-col>
        <app-checkbox label="checkbox" />
      </v-col>
      <v-col>
        <app-combobox :append-icon="icon" :append-outer-icon="icon" label="combobox" :prepend-icon="icon" :prepend-inner-icon="icon" />
      </v-col>
      <v-col>
        <app-file-input :append-icon="icon" :append-outer-icon="icon" label="file-input" :prepend-icon="icon" :prepend-inner-icon="icon" />
      </v-col>
      <v-col>
        <app-radio-group label="radio-group" row>
          <v-radio label="Label1" />
          <v-radio label="Label2" />
        </app-radio-group>
      </v-col>
      <v-col>
        <app-select :append-icon="icon" :append-outer-icon="icon" label="select" :prepend-icon="icon" :prepend-inner-icon="icon" />
      </v-col>
      <v-col>
        <app-switch label="switch" />
      </v-col>
      <v-col>
        <app-text-field :append-icon="icon" :append-outer-icon="icon" label="text-field" :prepend-icon="icon" :prepend-inner-icon="icon" />
      </v-col>
    </v-row>
    <div>hide-details</div>
    <v-row>
      <v-col>
        <app-autocomplete hide-details label="autocomplete" />
      </v-col>
      <v-col>
        <app-checkbox hide-details label="checkbox" />
      </v-col>
      <v-col>
        <app-combobox hide-details label="combobox" />
      </v-col>
      <v-col>
        <app-file-input hide-details label="file-input" />
      </v-col>
      <v-col>
        <app-radio-group hide-details label="radio-group" row>
          <v-radio label="Label1" />
          <v-radio label="Label2" />
        </app-radio-group>
      </v-col>
      <v-col>
        <app-select hide-details label="select" />
      </v-col>
      <v-col>
        <app-switch hide-details label="switch" />
      </v-col>
      <v-col>
        <app-text-field hide-details label="text-field" />
      </v-col>
    </v-row>
    <div>dense & hide-details</div>
    <v-row>
      <v-col>
        <app-autocomplete :append-icon="icon" :append-outer-icon="icon" dense hide-details label="autocomplete" :prepend-icon="icon" :prepend-inner-icon="icon" />
      </v-col>
      <v-col>
        <app-checkbox dense hide-details label="checkbox" />
      </v-col>
      <v-col>
        <app-combobox :append-icon="icon" :append-outer-icon="icon" dense hide-details label="combobox" :prepend-icon="icon" />
      </v-col>
      <v-col>
        <app-file-input :append-icon="icon" :append-outer-icon="icon" dense hide-details label="file-input" :prepend-icon="icon" />
      </v-col>
      <v-col>
        <app-radio-group dense hide-details label="radio-group" row>
          <v-radio label="Label1" />
          <v-radio label="Label2" />
        </app-radio-group>
      </v-col>
      <v-col>
        <app-select :append-icon="icon" :append-outer-icon="icon" dense hide-details label="select" :prepend-icon="icon" />
      </v-col>
      <v-col>
        <app-switch dense hide-details label="switch" />
      </v-col>
      <v-col>
        <app-text-field :append-icon="icon" :append-outer-icon="icon" dense hide-details label="text-field" :prepend-icon="icon" :prepend-inner-icon="icon" />
      </v-col>
    </v-row>
    <div>dense-x & hide-details="tooltip" & lazy-rendering</div>
    <v-row>
      <!-- <v-col>
        <app-autocomplete dense-x hide-details="tooltip" label="autocomplete" lazy-rendering />
      </v-col>
      <v-col>
        <app-checkbox dense-x hide-details="tooltip" label="checkbox" lazy-rendering />
      </v-col>
      <v-col>
        <app-combobox dense-x hide-details="tooltip" label="combobox" lazy-rendering />
      </v-col>
      <v-col>
        <app-switch dense-x hide-details="tooltip" label="switch" lazy-rendering />
      </v-col> -->
      <v-col>
        <validation-observer ref="observer" v-slot="{ errors }" slim>
          <div>
            <app-scope v-slot>
              {{ errors }}
            </app-scope>
            <app-scope v-slot>
              <!-- <app-text-field-wrap>
                <template v-slot:input="{ attrs, on }">
                  <app-text-field v-model="value" v-bind="attrs" dense-x hide-details="tooltip" prefix="P" vee-rules="required" v-on="on" />
                </template>
                <template v-slot:label="{ attrs, on }">
                  <app-text-style-label v-bind="attrs" :value="value" v-on="on" />
                </template>
              </app-text-field-wrap> -->
              <app-text-field v-model="value" dense-x hide-details="tooltip" lazy-rendering prefix="P" vee-rules="required" />
            </app-scope>
            <v-btn @click="$refs.observer.validate()">
              validate
            </v-btn>
          </div>
        </validation-observer>
      </v-col>
      <v-col>
        <app-text-field dense-x hide-details="tooltip" lazy-rendering />
      </v-col>
      <v-col>
        <app-text-number-field dense-x hide-details="tooltip" lazy-rendering />
      </v-col>
    </v-row>
  </v-form>
</template>

<style lang="scss" scoped>
.v-input,
.component-container {
  border: 1px solid #c0c;
}

.component {
  width: 100%;
  .row {
    border: 1px solid #0cc;
    flex-wrap: inherit;
    margin: 8px 0;
    overflow-x: scroll;
    padding: 4px;

    .col {
      min-width: 300px;
      padding: 2px;
    }
  }
}
</style>
