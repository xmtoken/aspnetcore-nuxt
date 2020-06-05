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
  props: {
    menuProps: {
      default() {
        return {
          offsetY: true,
        };
      },
      type: [Array, Object, String],
    },
  },
};
</script>

<template>
  <validation-provider v-slot="{ errors }" v-bind="veeValidationProps">
    <v-select v-bind="$attrs" :class="requiredClasses" :error-messages="errors" :label="label" :menu-props="menuProps" v-on="$listeners">
      <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
      <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
        <slot v-bind="scope" :name="slot" />
      </template>
    </v-select>
  </validation-provider>
</template>
