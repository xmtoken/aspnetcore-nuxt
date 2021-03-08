<script lang="ts">
import { ValidationProvider } from 'vee-validate';
import { PropValidator } from 'vue/types/options';
import { deepEqual } from 'vuetify/src/util/helpers';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import { InputableProps } from '~/mixins/inputable';
import { RequiredMarkable } from '~/mixins/required-markable';
import { Slotable } from '~/mixins/slotable';
import { UIElementState } from '~/mixins/ui-element-state';
import { Validatable } from '~/mixins/validatable';

type ComponentProps = Record<string, any> &
  InputableProps & {
    falseValue?: any;
    inputValue?: any;
    trueValue?: any;
    valueComparator?: typeof deepEqual;
  };

const Vue = VueBuilder.create() //
  .$attrs<ComponentProps>()
  .mixin(RequiredMarkable)
  .mixin(Slotable)
  .mixin(UIElementState)
  .mixin(Validatable)
  .build();

export default Vue.extend({
  components: {
    ValidationProvider,
  },
  inheritAttrs: false,
  model: {
    event: 'change',
    prop: 'inputValue',
  },
  props: {
    mandatory: {
      default: true,
      type: Boolean,
    },
    valueConverter: {
      default: undefined,
      type: Function,
    } as PropValidator<(val: any) => any>,
  },
  data() {
    return {
      value: null as any,
    };
  },
  computed: {
    props() {
      const defaults: ComponentProps = {
        falseValue: false,
        trueValue: true,
        valueComparator: deepEqual,
      };
      const attrs: ComponentProps = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProps = {
        hideDetails: attrs.hideDetails === 'auto' || attrs.hideDetails === 'tooltip' ? 'auto' : VuePropHelper.toBoolean(attrs.hideDetails),
      };
      delete attrs[this.$options.model!.prop!];
      return {
        ...attrs,
        ...overrides,
      };
    },
  },
  watch: {
    'props.inputValue': {
      handler(val: any, _oldVal: any) {
        const value = this.valueConverter ? this.valueConverter(val) : val;
        if (VuePropHelper.toBoolean(this.mandatory)) {
          const isValid = this.props.valueComparator!(value, this.props.falseValue) || this.props.valueComparator!(value, this.props.trueValue);
          this.value = isValid ? value : this.props.falseValue;
        } else {
          this.value = value;
        }
        if (val !== this.value) {
          this.$emit(this.$options.model!.event!, this.value);
        }
      },
      immediate: true,
    },
  },
  methods: {
    classes(required: boolean) {
      return {
        required,
        'required-marker': required && !VuePropHelper.toBoolean(this.disabledRequiredMarker),
        'v-input--tooltip-details': this.isEnabledTooltipMessage,
      };
    },
  },
});
</script>

<template>
  <validation-provider v-slot="{ errors, required }" v-bind="validationProviderProps">
    <v-checkbox v-bind="props" :class="classes(required)" :error-messages="errors" :input-value="value" v-on="$listeners">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
    </v-checkbox>
  </validation-provider>
</template>
