<script lang="ts">
import '~/components/AppInput/AppInput.scss';
import { mdiAlertCircleOutline } from '@mdi/js';
import { ValidationProvider } from 'vee-validate';
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
    hideDetails: {
      default: false,
      type: [String, Boolean],
    },
  },
  data() {
    return {
      focused: false,
      hovered: false,
      icons: {
        mdiAlertCircleOutline,
      },
    };
  },
  computed: {
    classes(): object {
      return {
        'v-input--tooltip-details': this.isEnabledTooltipMessage,
      };
    },
    internalClearable(): boolean {
      return this.clearable && !this.readonly;
    },
    internalHideDetails(): string | boolean {
      return this.isEnabledTooltipMessage ? 'auto' : this.hideDetails;
    },
    isEnabledTooltipMessage(): boolean {
      return this.hideDetails === 'tooltip';
    },
  },
  mounted(): void {
    this.$el.addEventListener('mouseenter', () => {
      this.hovered = true;
    });
    this.$el.addEventListener('mouseleave', () => {
      this.hovered = false;
    });
  },
  methods: {
    onBlur(): void {
      this.focused = false;
    },
    onFocus(): void {
      this.focused = true;
    },
  },
});
</script>

<template>
  <validation-provider v-slot="{ errors, failed, required }" v-bind="veeProviderProps">
    <v-combobox ref="field" v-bind="$attrs" :class="{ ...classes, required, 'required-marker': required && !disabledRequiredMarker }" :clearable="internalClearable" :disabled="disabled" :error-messages="errors" :hide-details="internalHideDetails" :label="label" :readonly="readonly" v-on="$listeners" @blur="onBlur" @focus="onFocus">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
      <template v-if="failed && isEnabledTooltipMessage" #append-outer>
        <v-icon color="error" small>
          {{ icons.mdiAlertCircleOutline }}
        </v-icon>
        <slot name="append-outer" />
      </template>
      <template v-if="isEnabledTooltipMessage" #message="scope">
        <v-tooltip :activator="$refs.field" :open-on-hover="false" top :value="focused || hovered">
          {{ scope.message }}
        </v-tooltip>
        <slot v-bind="scope" name="message" />
      </template>
    </v-combobox>
  </validation-provider>
</template>
