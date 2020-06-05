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
    appendIconTabindex: {
      default: undefined,
      type: Number,
    },
  },
  watch: {
    appendIconTabindex() {
      this.setAppendIconTabindex();
    },
  },
  mounted() {
    this.setAppendIconTabindex();
  },
  methods: {
    setAppendIconTabindex() {
      const elements = this.$el.querySelectorAll('.v-input__icon--append button.v-icon');
      if (this.appendIconTabindex) {
        elements.forEach(x => x.setAttribute('tabindex', this.appendIconTabindex));
      } else {
        elements.forEach(x => x.removeAttribute('tabindex'));
      }
    },
  },
};
</script>

<template>
  <validation-provider ref="provider" v-slot="{ errors }" v-bind="veeValidationProps">
    <v-text-field v-bind="$attrs" :class="requiredClasses" :error-messages="errors" :label="label" v-on="$listeners">
      <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
      <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
        <slot v-bind="scope" :name="slot" />
      </template>
    </v-text-field>
  </validation-provider>
</template>
