<script>
import AppValidationProviderProps from './AppValidationProviderProps';
import { RequiredMarkable } from '~/mixins';
import { ValidationProvider } from 'vee-validate';
export default {
  components: {
    ValidationProvider,
  },
  mixins: [
    //
    AppValidationProviderProps,
    RequiredMarkable,
  ],
  inheritAttrs: false,
};
</script>

<template>
  <validation-provider v-slot="{ errors }" v-bind="veeValidationProps">
    <v-combobox v-bind="$attrs" :class="requiredClasses" :error-messages="errors" :label="label" v-on="$listeners">
      <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
      <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
        <slot v-bind="scope" :name="slot" />
      </template>
    </v-combobox>
  </validation-provider>
</template>
