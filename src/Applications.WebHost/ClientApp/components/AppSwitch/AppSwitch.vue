<script lang="ts">
import { ValidationProvider } from 'vee-validate';
import { PropType } from 'vue';
import { PropValidator } from 'vue/types/options';
import { deepEqual } from 'vuetify/src/util/helpers';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import { Inputable, InputableProps } from '~/mixins/inputable';
import { RequiredMarkable, RequiredMarkableProps } from '~/mixins/required-markable';
import { Slotable } from '~/mixins/slotable';
import { UIElementState } from '~/mixins/ui-element-state';
import { Validatable, ValidatableProxyProps } from '~/mixins/validatable';
import { PickProp, ProxyProps } from '~/types/global';

type ComponentProxyProps = ProxyProps &
  InputableProps &
  RequiredMarkableProps &
  ValidatableProxyProps & {
    falseValue?: any;
    inputValue?: any;
    trueValue?: any;
    valueComparator?: typeof deepEqual | null;
  };

type ComponentProps = ComponentProxyProps & {
  fitContent?: boolean | null;
  mandatory?: boolean | null;
  valueConverter?: ((val: any) => any) | null;
};

export type AppSwitchProps = ComponentProps;

type ComponentRefs = {
  field: Element;
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProxyProps>()
  .$refs<ComponentRefs>()
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
      type: (null as any) as PropType<PickProp<ComponentProps, 'fitContent'>>,
    },
    mandatory: {
      default: true,
      type: (null as any) as PropType<PickProp<ComponentProps, 'mandatory'>>,
    },
    valueConverter: {
      default: val => {
        return String(val) === String(true);
      },
      type: Function,
    } as PropValidator<PickProp<ComponentProps, 'valueConverter'>>,
  },
  data() {
    return {
      value: null as PickProp<ComponentProps, 'inputValue'>,
    };
  },
  computed: {
    props() {
      const defaults: ComponentProxyProps = {
        falseValue: false,
        trueValue: true,
        valueComparator: deepEqual,
      };
      const attrs: ComponentProxyProps & Record<string, any> = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProxyProps = {
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
      handler(val: PickProp<ComponentProps, 'inputValue'>, _oldVal: PickProp<ComponentProps, 'inputValue'>) {
        const value = this.valueConverter ? this.valueConverter(val) : val;
        if (this.mandatory) {
          const isValid = this.props.valueComparator!(value, this.props.falseValue) || this.props.valueComparator!(value, this.props.trueValue);
          this.value = isValid ? value : this.props.falseValue;
        } else {
          this.value = value;
        }
        if (this.value !== val) {
          this.$emit(this.$options.model!.event!, this.value);
        }
      },
      immediate: true,
    },
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
  <validation-provider v-slot="{ errors, failed, required }" v-bind="validationProviderProps">
    <v-switch ref="field" v-bind="props" :class="classes(required)" :error-messages="errors" :input-value="value" v-on="$listeners">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
      <!-- <template #append>
        <v-icon v-if="failed && isEnabledTooltipMessage" color="error" small>
          {{ validationErrorIcon }}
        </v-icon>
        <slot name="append" />
      </template> -->
      <template #message="scope">
        <v-tooltip v-if="isEnabledTooltipMessage" :activator="$refs.field" color="error" :open-on-hover="false" top :value="focused || hovered">
          {{ scope.message }}
        </v-tooltip>
        <slot v-bind="scope" name="message" />
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
