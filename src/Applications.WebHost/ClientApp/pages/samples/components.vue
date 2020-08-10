<script lang="ts">
import Vue from 'vue';

export default Vue.extend({});
</script>

<template>
  <v-form autocomplete="off" @submit.prevent>
    <v-row dense>
      <v-col>
        <v-card outlined>
          <v-card-title>
            app-text-field
          </v-card-title>
          <v-card-text>
            <app-text-field label="Normal" />
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-row dense>
      <v-col>
        <v-card outlined>
          <v-card-title>
            app-time-field
          </v-card-title>
          <v-card-text>
            <app-time-field label="Normal" />
            <app-time-field :append-icon-tabindex="0" label="append-icon-tabindex=0" />
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-form>
</template>
