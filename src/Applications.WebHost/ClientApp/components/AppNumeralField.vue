<script>
import AppTextField from './AppTextField';
import Numbro from 'numbro';
export default {
  components: {
    AppTextField,
  },
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
      model: this.applyFormat(this.value),
    };
  },
  computed: {
    /** @returns {String} */
    formattedText() {
      return this.applyFormat(this.model);
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
      const formattedValue = this.applyFormat(val);
      if (this.focused && this.formattedText === formattedValue) {
        return;
      }
      this.model = formattedValue;
    },
  },
  methods: {
    applyFormat(val) {
      return Numbro.validate(val, {}) ? Numbro(val).format(this.format) : val;
    },
    onBlue() {
      this.focused = false;
      this.model = this.formattedText;
    },
    onFocus() {
      this.focused = true;
    },
    onInput() {
      this.$emit('input', Numbro.validate(this.formattedText, {}) ? Numbro(this.formattedText).value() : this.formattedText);
    },
  },
};
</script>

<template>
  <app-text-field v-model="model" v-bind="$attrs" v-on="listeners" @blur="onBlue" @focus="onFocus" @input="onInput">
    <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
    <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
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
