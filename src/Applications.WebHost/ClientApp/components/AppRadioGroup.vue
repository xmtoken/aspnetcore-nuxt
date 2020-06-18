<script>
import AppValidationProviderProps from './AppValidationProviderProps';
import { Slotable } from '~/mixins';
import { ValidationProvider } from 'vee-validate';
export default {
  components: {
    ValidationProvider,
  },
  mixins: [
    //
    AppValidationProviderProps,
    Slotable,
  ],
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
      <slot v-for="slot in slotKeys" :slot="slot" :name="slot" />
      <template v-for="slot in scopedSlotKeys" :slot="slot" slot-scope="scope">
        <slot v-bind="scope" :name="slot" />
      </template>
    </v-radio-group>
  </validation-provider>
</template>
