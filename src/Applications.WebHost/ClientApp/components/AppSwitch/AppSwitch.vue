<script lang="ts">
import { ValidationProvider } from 'vee-validate';
import { PropValidator } from 'vue/types/options';
import { deepEqual } from 'vuetify/src/util/helpers';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import { Inputable, InputableProps } from '~/mixins/inputable';
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
  .mixin(Inputable)
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
    fitContent: {
      default: true,
      type: Boolean,
    },
    mandatory: {
      default: true,
      type: Boolean,
    },
    valueConverter: {
      default: val => {
        return String(val) === String(true);
      },
      type: Function,
    } as PropValidator<(val: any) => any>,
  },
  data() {
    return {
      created: false,
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
        dense: VuePropHelper.toBoolean(attrs.dense) || this.denseX,
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
    'attrs.inputValue': {
      handler(val: any, _oldVal: any) {
        const value = this.valueConverter ? this.valueConverter(val) : val;
        if (this.mandatory) {
          const isValid = this.props.valueComparator!(value, this.props.falseValue) || this.props.valueComparator!(value, this.props.trueValue);
          this.value = isValid ? value : this.props.falseValue;
        } else {
          this.value = value;
        }
        if (!this.created && this.value !== val) {
          this.$emit(this.$options.model!.event!, this.value);
        }
      },
      immediate: true,
    },
  },
  created() {
    this.created = true;
  },
  methods: {
    classes(required: boolean) {
      return {
        'fit-content': this.fitContent,
        required,
        'required-marker': required && !this.disabledRequiredMarker,
        'v-input--dense-x': this.denseX,
        'v-input--tooltip-details': this.isEnabledTooltipMessage,
      };
    },
  },
});
</script>

<template>
  <validation-provider v-slot="{ errors, required }" v-bind="validationProviderProps">
    <v-switch v-bind="props" :class="classes(required)" :error-messages="errors" :input-value="value" v-on="$listeners">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
    </v-switch>
  </validation-provider>
</template>

<style lang="scss" scoped>
@import '~/components/AppInput/AppInput.scss';

.fit-content {
  width: fit-content;
}

.v-input ::v-deep {
  padding-right: 2px;

  .v-input--selection-controls__input {
    margin-right: 0;

    & + label {
      margin-left: 8px;
    }
  }
}

.v-input--dense ::v-deep {
  .v-input--selection-controls__ripple {
    top: -9px;
  }

  .v-input--switch__thumb {
    top: 3px;
  }

  .v-input--switch__track {
    top: 6px;
  }
}

.v-input--dense-x {
  padding-bottom: 1px;
  padding-top: 1px;
}
</style>
