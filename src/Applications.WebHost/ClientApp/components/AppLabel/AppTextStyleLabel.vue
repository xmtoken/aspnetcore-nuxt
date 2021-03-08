<script lang="ts">
import { validate } from 'vee-validate';
import { PropType } from 'vue';
import { VueBuilder } from '~/core/vue';
import { UIElementState } from '~/mixins/ui-element-state';
import { Validatable } from '~/mixins/validatable';

type ComponentProps = {
  prefix?: string;
  suffix?: string;
  tabindex?: number;
  value?: any;
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProps>()
  .mixin(UIElementState)
  .mixin(Validatable)
  .build();

const instance = new Vue();
type PropsType = typeof instance.attrs;

export default Vue.extend({
  inheritAttrs: false,
  inject: {
    $_veeObserver: {
      from: '$_veeObserver',
      default() {
        return null;
      },
    },
  },
  props: {
    errors: {
      default() {
        return [];
      },
      type: Array as PropType<string[]>,
    },
  },
  data() {
    return {
      activator: null as Element | null,
      validator: null as any,
    };
  },
  computed: {
    props(): PropsType {
      const defaults: PropsType = {
        tabindex: 0,
      };
      const attrs: PropsType = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: PropsType = {
        //
      };
      return {
        ...attrs,
        ...overrides,
      };
    },
  },
  watch: {
    errors: {
      handler(): void {
        this.$nextTick(() => {
          this.activator = this.$el;
        });
      },
      immediate: true,
    },
  },
  mounted() {
    console.log(this.$props);
    const errorsCont = [] as string[];
    const validateX = () => {
      //
      console.log('validate');
      return validate(this.props.value, this.validationProviderProps.rules, {}).then(x => {
        errorsCont.splice(0, errorsCont.length, ...x.errors);
        return x;
      });
    };
    const validateSilentX = () => {
      //
      console.log('validateSilent');
      return validate(this.props.value, this.validationProviderProps.rules, {}).then(x => {
        errorsCont.splice(0, errorsCont.length, ...x.errors);
        return x;
      });
    };
    this.validator = {
      id: Date.now().toString(),
      errors: errorsCont,
      flags: {},
      validate: validateX,
      validateSilent: validateSilentX,
    };
    console.log('mounted', this.$_veeObserver);
    this.$_veeObserver?.observe(this.validator);

    // this needs
    // --id: string
    // --errors: [] as string[],
    // --flags: {},
  },
  beforeDestroy() {
    console.log('beforeDestroy', this.$_veeObserver);
    this.$_veeObserver?.unobserve(this.validator.id);
  },
});
</script>

<template>
  <div v-bind="props" class="component-container" v-on="$listeners">
    <div :class="{ 'is-error': errors.length, 'text-container': true }">
      <div class="d-flex text-block">
        <div v-if="props.prefix" :class="{ 'd-inline-block': true, 'error--text': errors.length, 'v-text-field__prefix': true }">
          {{ props.prefix }}
        </div>
        <div class="d-inline-block flex-grow-1">
          {{ props.value }}
        </div>
        <div v-if="props.suffix" :class="{ 'd-inline-block': true, 'error--text': errors.length, 'v-text-field__suffix': true }">
          {{ props.suffix }}
        </div>
      </div>
    </div>
    <template v-if="errors.length">
      <v-icon class="my-1" color="error" small style="margin-left: 2px;">
        {{ validationErrorIcon }}
      </v-icon>
      <v-tooltip v-if="true" :activator="activator ? activator : $el" top>
        {{ errors[0] }}
      </v-tooltip>
      <!-- <v-tooltip v-if="hovered" :activator="activator ? activator : $el" top>
        {{ errors[0] }}
      </v-tooltip> -->
    </template>
  </div>
</template>

<style lang="scss" scoped>
.component-container ::v-deep {
  align-items: flex-end;
  box-sizing: content-box;
  cursor: text;
  display: flex;
  height: 28px;
  outline: none;

  .text-container {
    height: 100%;
    min-width: 0;
    padding: 3px 0 1px 0;
    position: relative;
    width: 100%;

    .text-block {
      margin-top: 4px;
      overflow: hidden;
    }

    &::before {
      border-color: rgba(0, 0, 0, 0.42);
      border-style: solid;
      border-width: 0 0 thin 0;
      bottom: -1px;
      content: '';
      left: 0;
      position: absolute;
      width: 100%;
    }

    &:hover::before {
      border-color: rgba(0, 0, 0, 0.87);
    }

    &.is-error::before {
      border-color: #ff5252;
    }
  }
}
</style>
