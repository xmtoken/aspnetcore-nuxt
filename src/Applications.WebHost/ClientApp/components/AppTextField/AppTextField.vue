<script lang="ts">
import '~/components/AppInput/AppInput.scss';
import { ValidationProvider } from 'vee-validate';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import { Clearable, ClearableProps } from '~/mixins/clearable';
import { IconTabIndexable } from '~/mixins/icon-tab-indexable';
import { InputableProps } from '~/mixins/inputable';
import { LazyRenderable } from '~/mixins/lazy-renderable';
import { RequiredMarkable } from '~/mixins/required-markable';
import { Slotable } from '~/mixins/slotable';
import { UIElementState } from '~/mixins/ui-element-state';
import { Validatable, ValidatableProps } from '~/mixins/validatable';

type ComponentProps = ClearableProps & InputableProps & ValidatableProps;

type ComponentRefs = {
  field: HTMLElement;
  provider: InstanceType<typeof ValidationProvider>;
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProps>()
  .$refs<ComponentRefs>()
  .mixin(Clearable)
  .mixin(IconTabIndexable)
  .mixin(LazyRenderable)
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
  },
  data() {
    return {
      $_: null as any,
      xErrors: [] as string[],
    };
  },
  watch: {
    $attrs: {
      handler() {
        this.$emit('xxx', this.$attrs);
      },
      immediate: true,
    },
  },
  mounted() {
    //
    // this.$refs.provider.setErrors(this.$attrs.errors);
    console.log(this.$props);
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
    props(errors: string[]) {
      const defaults: ComponentProps = {
        //
      };
      const attrs: ComponentProps = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProps = {
        clearable: VuePropHelper.toBoolean(attrs.clearable) && !VuePropHelper.toBoolean(attrs.readonly),
        dense: VuePropHelper.toBoolean(attrs.dense) || VuePropHelper.toBoolean(this.denseX),
        errorMessages: this.mergeErrorMessages(errors),
        hideDetails: attrs.hideDetails === 'auto' || attrs.hideDetails === 'tooltip' ? 'auto' : VuePropHelper.toBoolean(attrs.hideDetails),
      };
      return {
        ...attrs,
        ...overrides,
      };
    },
    onUpdateError(errors: string[]) {
      this.$emit('update:error-messages', errors);
    },
    onFocusLabel() {
      this.isLazing = false;
      this.$nextTick(() => {
        this.$refs.field.focus();
        this.$refs.provider.setErrors(this.xErrors);
      });
    },
    onBlurInput() {
      this.xErrors = [...this.$refs.provider.errors];
      this.isLazing = true;
    },
  },
});
</script>

<template>
  <app-text-style-label v-if="lazyRendering && isLazing" v-bind="{ ...$attrs, ...$props }" @focus="onFocusLabel" />
  <validation-provider v-else ref="provider" v-slot="{ errors, failed, required }" v-bind="validationProviderProps">
    <v-text-field ref="field" v-bind="props(errors)" :class="classes(required)" v-on="$listeners" @blur="onBlurInput" @update:error="onUpdateError(errors)">
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
      <template v-if="isEnabledTooltipMessage" #message="scope">
        <v-tooltip v-if="focused" :activator="$refs.field" :open-on-hover="false" top>
          {{ scope.message }}
        </v-tooltip>
        <v-tooltip :activator="$refs.field" :open-on-hover="false" top :value="hovered">
          {{ scope.message }}
        </v-tooltip>
        <slot v-bind="scope" name="message" />
      </template>
    </v-text-field>
  </validation-provider>
</template>
