<script lang="ts">
import { ValidationProvider } from 'vee-validate';
import { PropType } from 'vue';
import mixins from '~/extensions/mixins';
import iconTabIndexable from '~/mixins/icon-tab-indexable';
import requiredMarkable from '~/mixins/required-markable';
import slotable from '~/mixins/slotable';
import validationProviderProps from '~/mixins/validation-provider-props';

export default mixins(iconTabIndexable, requiredMarkable, slotable, validationProviderProps).extend({
  components: {
    ValidationProvider,
  },
  inheritAttrs: false,
  props: {
    menuProps: {
      default(): object {
        return {
          offsetY: true,
        };
      },
      type: Object as PropType<object>,
    },
  },
});
</script>

<template>
  <validation-provider v-slot="{ errors, required }" v-bind="veeProviderProps">
    <v-select v-bind="$attrs" :class="{ required, 'required-marker': required && !disabledRequiredMarker }" :error-messages="errors" :label="label" :menu-props="menuProps" v-on="$listeners">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
    </v-select>
  </validation-provider>
</template>
