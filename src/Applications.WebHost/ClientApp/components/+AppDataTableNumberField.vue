<script lang="ts">
import { mdiAlertCircleOutline } from '@mdi/js';
import { ValidationProvider } from 'vee-validate';
import { format_number } from '~/filters';
import ValidationProviderProps from '~/mixins/validation-provider-props';
export default {
  components: {
    ValidationProvider,
  },
  filters: {
    format_number,
  },
  mixins: [
    //
    ValidationProviderProps,
  ],
  props: {
    format: {
      default: '0',
      type: String,
    },
    maxWidth: {
      default: 'auto',
      type: [Number, String],
    },
    value: {
      default: undefined,
      type: undefined,
    },
  },
  data() {
    return {
      icons: {
        mdiAlertCircleOutline,
      },
      model: this.value,
    };
  },
  computed: {
    /** @returns {Object} */
    listeners() {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
  },
  watch: {
    model(val) {
      this.$emit('input', val);
    },
    value(val) {
      this.model = val;
    },
  },
  methods: {
    validate() {
      this.$refs.provider.validate();
    },
  },
};
</script>

<template>
  <app-menu content-class="elevation-0 overflow-visible" :max-width="maxWidth">
    <template v-slot:activator="{ on: onMenu, opend: opendMenu }">
      <validation-provider ref="provider" v-slot="{ errors, failed }" v-bind="veeValidationProps">
        <v-tooltip color="error" :disabled="!failed" top>
          <template v-slot:activator="{ on: onTooltip }">
            <div :class="{ 'cursor-pointer': true, 'visibility-hidden': opendMenu }" v-on="{ ...onMenu, ...onTooltip }">
              <v-icon v-if="failed" color="error" small>
                {{ icons.mdiAlertCircleOutline }}
              </v-icon>
              <span v-if="`${model}`">
                {{ model | format_number(format) }}
              </span>
              <span v-else>
                &nbsp;
              </span>
            </div>
          </template>
          <span>{{ errors[0] }}</span>
        </v-tooltip>
        <input class="d-none" :value="model" />
      </validation-provider>
    </template>
    <template v-slot="{ close }">
      <app-number-field v-model="model" v-bind="$attrs" autofocus class="body-2" dense hide-details v-on="listeners" @blur="validate" @keydown.enter="close" />
    </template>
  </app-menu>
</template>

<style lang="scss" scoped>
.v-input ::v-deep {
  height: 23px;
  margin: 0;
  padding: 0;

  input {
    padding: 0 !important;
  }
}
</style>
