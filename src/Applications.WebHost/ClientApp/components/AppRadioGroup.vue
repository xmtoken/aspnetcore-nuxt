<script>
import AppValidationProviderProps from './AppValidationProviderProps';
import { ValidationProvider } from 'vee-validate';
export default {
  components: {
    ValidationProvider,
  },
  mixins: [AppValidationProviderProps],
  inheritAttrs: false,
  model: {
    event: 'change',
    prop: 'value',
  },
};
</script>

<template>
  <validation-provider v-slot="{ errors }" v-bind="veeValidationProps">
    <v-radio-group v-bind="$attrs" :error-messages="errors" :label="label" v-on="$listeners">
      <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
      <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
        <slot v-bind="scope" :name="slot" />
      </template>
    </v-radio-group>
  </validation-provider>
</template>
