<script>
import { Slotable, ValidationProviderProps } from '~/mixins';
import { ValidationProvider } from 'vee-validate';
export default {
  components: {
    ValidationProvider,
  },
  mixins: [
    //
    Slotable,
    ValidationProviderProps,
  ],
  inheritAttrs: false,
  model: {
    event: 'change',
    prop: 'inputValue',
  },
};
</script>

<template>
  <validation-provider v-slot="{ errors }" v-bind="veeValidationProps">
    <v-checkbox v-bind="$attrs" :error-messages="errors" :label="label" v-on="$listeners">
      <slot v-for="slot in slotKeys" :slot="slot" :name="slot" />
      <template v-for="slot in scopedSlotKeys" :slot="slot" slot-scope="scope">
        <slot v-bind="scope" :name="slot" />
      </template>
    </v-checkbox>
  </validation-provider>
</template>
