<script lang="ts">
import '~/components/AppInput/AppInput.scss';
import { ValidationProvider } from 'vee-validate';
import { PropType } from 'vue';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import { Clearable, ClearableProps } from '~/mixins/clearable';
import { IconTabIndexable } from '~/mixins/icon-tab-indexable';
import { InputableProps } from '~/mixins/inputable';
import { RequiredMarkable } from '~/mixins/required-markable';
import { Slotable } from '~/mixins/slotable';
import { UIElementState } from '~/mixins/ui-element-state';
import { Validatable } from '~/mixins/validatable';

type ComponentProps = ClearableProps &
  InputableProps & {
    dense?: boolean;
    prefix?: string;
    tabindex?: number;
    suffix?: string;
  };

type ComponentRefs = {
  field: HTMLElement;
  provider: InstanceType<typeof ValidationProvider>;
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProps>()
  .$refs<ComponentRefs>()
  .mixin(Clearable)
  .mixin(IconTabIndexable)
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
  props: {
    denseX: {
      default: false,
      type: Boolean,
    },
    errorMessages: {
      default() {
        return [];
      },
      type: Array as PropType<string[]>,
    },
    lazyRendering: {
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
      internalErrorMessages: [] as string[],
      internalValue: null as any,
    };
  },
  computed: {
    props() {
      const defaults: ComponentProps = {
        tabindex: 0,
      };
      const attrs: ComponentProps = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProps = {
        clearable: VuePropHelper.toBoolean(attrs.clearable) && !VuePropHelper.toBoolean(attrs.readonly),
        dense: VuePropHelper.toBoolean(attrs.dense) || VuePropHelper.toBoolean(this.denseX),
        hideDetails: attrs.hideDetails === 'auto' || attrs.hideDetails === 'tooltip' ? 'auto' : VuePropHelper.toBoolean(attrs.hideDetails),
      };
      return {
        ...attrs,
        ...overrides,
      };
    },
  },
  watch: {
    errorMessages: {
      handler(): void {
        this.internalErrorMessages = this.errorMessages;
      },
      immediate: true,
    },
    value: {
      handler(): void {
        this.internalValue = this.value;
      },
      immediate: true,
    },
  },
  methods: {
    classes(required: boolean) {
      return {
        required,
        'required-marker': required && !VuePropHelper.toBoolean(this.disabledRequiredMarker),
        'v-input--dense-x': VuePropHelper.toBoolean(this.denseX),
        'v-input--tooltip-details': this.isEnabledTooltipMessage,
      };
    },
    onFocusTextStyleLabel(): void {
      const errors = this.internalErrorMessages;
      this.focused = true;
      this.$nextTick(() => {
        this.$refs.field.focus();
        this.$refs.provider.setErrors(errors);
      });
    },
    setErrors(errors: string[]): void {
      this.internalErrorMessages = errors;
      this.$emit('update:error-messages', errors);
    },
  },
});
</script>

<template>
  <app-text-style-label v-if="lazyRendering && !focused" :errors="internalErrorMessages" :prefix="props.prefix" :suffix="props.suffix" :tabindex="props.tabindex" :value="internalValue" @focus="onFocusTextStyleLabel" />
  <validation-provider v-else ref="provider" v-slot="{ errors, failed, required }" v-bind="validationProviderProps">
    <v-text-field ref="field" v-model="internalValue" v-bind="props" :class="classes(required)" :error-messages="errors" v-on="$listeners">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
      <template v-if="failed && isEnabledTooltipMessage" #append-outer>
        <v-icon color="error" small>
          {{ validationErrorIcon }}
        </v-icon>
        <slot name="append-outer" />
      </template>
      <template v-if="failed && isEnabledTooltipMessage" #append-outer>
        <v-icon color="error" small>
          {{ validationErrorIcon }}
        </v-icon>
        <slot name="append-outer" />
      </template>
      <template v-if="isEnabledTooltipMessage" #message="scope">
        <v-tooltip :activator="$refs.field" :open-on-hover="false" top :value="focused || hovered">
          {{ scope.message }}
        </v-tooltip>
        <slot v-bind="scope" name="message" />
      </template>
      {{ setErrors(errors) }}
    </v-text-field>
  </validation-provider>
</template>
