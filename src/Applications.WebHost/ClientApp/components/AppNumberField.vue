<script>
import AppTextField from './AppTextField';
import { Slotable } from '~/mixins';
import numbro from 'numbro';
export default {
  components: {
    AppTextField,
  },
  mixins: [
    //
    Slotable,
  ],
  inheritAttrs: false,
  props: {
    format: {
      default: '0',
      type: String,
    },
    value: {
      default: undefined,
      type: undefined,
    },
  },
  data() {
    return {
      focused: false,
      model: this.toFormatted(this.value),
    };
  },
  computed: {
    /** @returns {String} */
    formattedText() {
      return this.toFormatted(this.model);
    },
    /** @returns {Object} */
    listeners() {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
  },
  watch: {
    value(val) {
      const formatted = this.toFormatted(val);
      if (this.focused && this.formattedText === formatted) {
        return;
      }
      this.model = formatted;
    },
  },
  methods: {
    onBlue() {
      this.focused = false;
      this.model = this.formattedText;
    },
    onFocus() {
      this.focused = true;
    },
    onInput() {
      this.$emit('input', this.isValid(this.model) ? numbro(this.model).value() : this.model);
    },
    toFormatted(val) {
      return this.isValid(val) ? numbro(val).format(this.format) : val;
    },
    isValid(val) {
      const value = numbro(val).value();
      return value !== undefined && value !== null && !Number.isNaN(value);
    },
  },
};
</script>

<template>
  <app-text-field v-model="model" v-bind="$attrs" v-on="listeners" @blur="onBlue" @focus="onFocus" @input="onInput">
    <slot v-for="slot in slotKeys" :slot="slot" :name="slot" />
    <template v-for="slot in scopedSlotKeys" :slot="slot" slot-scope="scope">
      <slot v-bind="scope" :name="slot" />
    </template>
  </app-text-field>
</template>

<style lang="scss" scoped>
.v-input ::v-deep {
  input {
    text-align: right;
  }
}
</style>
