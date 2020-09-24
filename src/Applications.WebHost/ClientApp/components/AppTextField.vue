<script lang="ts">
import { ValidationProvider } from 'vee-validate';
import { PropType } from 'vue';
import mixins from '~/extensions/mixins';
import iconTabIndexable from '~/mixins/icon-tab-indexable';
import requiredMarkable from '~/mixins/required-markable';
import slotable from '~/mixins/slotable';
import validationProviderProps from '~/mixins/validation-provider-props';
import { Listeners } from '~/types/vue';

export default mixins(iconTabIndexable, requiredMarkable, slotable, validationProviderProps).extend({
  components: {
    ValidationProvider,
  },
  inheritAttrs: false,
  props: {
    lazy: {
      default: false,
      type: Boolean,
    },
    value: {
      default: undefined,
      type: (null as any) as PropType<any>,
    },
  },
  data() {
    return {
      internalValue: this.value,
    };
  },
  computed: {
    listeners(): Listeners {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
  },
  watch: {
    internalValue(val: any): void {
      this.$emit('input', val);
    },
    value(val: any): void {
      this.internalValue = val;
    },
  },
  methods: {
    onChange(val: any): void {
      if (this.lazy) {
        this.internalValue = val;
      }
    },
    onInput(val: any): void {
      if (!this.lazy) {
        this.internalValue = val;
      }
    },
  },
});
</script>

<template>
  <validation-provider v-slot="{ errors, required }" v-bind="veeProviderProps">
    <v-text-field v-bind="$attrs" :class="{ required, 'required-marker': required && !disabledRequiredMarker }" :error-messages="errors" :label="label" :value="internalValue" v-on="listeners" @change="onChange" @input="onInput">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
    </v-text-field>
  </validation-provider>
</template>
