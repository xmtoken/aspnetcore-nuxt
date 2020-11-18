<script lang="ts">
import { ValidationProvider } from 'vee-validate';
import { PropType } from 'vue';
import mixins from '~/extensions/mixins';
import requiredMarkable from '~/mixins/required-markable';
import slotable from '~/mixins/slotable';
import validationProviderProps from '~/mixins/validation-provider-props';

export default mixins(requiredMarkable, slotable, validationProviderProps).extend({
  components: {
    ValidationProvider,
  },
  inheritAttrs: false,
  model: {
    event: 'change',
    prop: 'inputValue',
  },
  props: {
    falseValue: {
      default: false,
      type: (null as any) as PropType<any>,
    },
    inputValue: {
      default: undefined,
      type: (null as any) as PropType<any>,
    },
    mandatory: {
      default: true,
      type: Boolean,
    },
    trueValue: {
      default: true,
      type: (null as any) as PropType<any>,
    },
  },
  watch: {
    inputValue: {
      handler(): void {
        this.updateMandatory();
      },
      immediate: true,
    },
  },
  methods: {
    updateMandatory(): void {
      if (this.mandatory && this.inputValue !== this.falseValue && this.inputValue !== this.trueValue) {
        this.$emit('change', this.falseValue);
      }
    },
  },
});
</script>

<template>
  <validation-provider v-slot="{ errors, required }" v-bind="veeProviderProps">
    <v-checkbox v-bind="$attrs" :class="{ required, 'required-marker': required && !disabledRequiredMarker }" :disabled="disabled" :error-messages="errors" :false-value="falseValue" :input-value="inputValue" :label="label" :readonly="readonly" :true-value="trueValue" v-on="$listeners">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
    </v-checkbox>
  </validation-provider>
</template>
