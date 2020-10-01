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
    clearable: {
      default: false,
      type: Boolean,
    },
    menuProps: {
      default(): object {
        return {
          offsetY: true,
        };
      },
      type: [String, Object] as PropType<string | object>,
    },
  },
  computed: {
    internalClearable(): boolean {
      return this.clearable && !this.readonly;
    },
  },
});
</script>

<template>
  <validation-provider v-slot="{ errors, required }" v-bind="veeProviderProps">
    <v-autocomplete v-bind="$attrs" :class="{ required, 'required-marker': required && !disabledRequiredMarker }" :clearable="internalClearable" :disabled="disabled" :error-messages="errors" :label="label" :menu-props="menuProps" :readonly="readonly" v-on="$listeners">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
    </v-autocomplete>
  </validation-provider>
</template>
