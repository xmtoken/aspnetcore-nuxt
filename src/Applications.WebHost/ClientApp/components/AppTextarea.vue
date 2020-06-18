<script>
import { RequiredMarkable, Slotable, ValidationProviderProps } from '~/mixins';
import { ValidationProvider } from 'vee-validate';
export default {
  components: {
    ValidationProvider,
  },
  mixins: [
    //
    RequiredMarkable,
    Slotable,
    ValidationProviderProps,
  ],
  inheritAttrs: false,
};
</script>

<template>
  <!-- provider refs for RequiredMarkable -->
  <validation-provider ref="provider" v-slot="{ errors }" v-bind="veeValidationProps">
    <v-textarea v-bind="$attrs" :class="requiredClasses" :error-messages="errors" :label="label" v-on="$listeners">
      <slot v-for="slot in slotKeys" :slot="slot" :name="slot" />
      <template v-for="slot in scopedSlotKeys" :slot="slot" slot-scope="scope">
        <slot v-bind="scope" :name="slot" />
      </template>
    </v-textarea>
  </validation-provider>
</template>
