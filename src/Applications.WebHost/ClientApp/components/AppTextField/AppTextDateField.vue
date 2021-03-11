<script lang="ts">
import { mdiCalendar } from '@mdi/js';
import dayjs from 'dayjs';
import { PropType } from 'vue';
import { AppDatePickerProps } from '~/components/AppDatePicker/AppDatePicker.vue';
import { AppMenuProps } from '~/components/AppMenu/AppMenu.vue';
import { AppTextFieldProps } from '~/components/AppTextField/AppTextField.vue';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import * as DateFormatter from '~/extensions/formatters/date-formatter';
import { Slotable } from '~/mixins/slotable';

type ComponentProxyProps = Record<string, any> & //
  AppTextFieldProps;

export type AppTextDateFieldProps = ComponentProxyProps & {
  menuOffsetY?: boolean;
  menuProps?: AppMenuProps;
  pickerProps?: AppDatePickerProps;
};

type ComponentRefs = {
  field: Element;
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProxyProps>()
  .$refs<ComponentRefs>()
  .mixin(Slotable)
  .build();

export default Vue.extend({
  inheritAttrs: false,
  model: {
    event: 'change',
    prop: 'value',
  },
  props: {
    menuOffsetY: {
      default: true,
      type: Boolean,
    },
    menuProps: {
      default(): AppMenuProps {
        return {};
      },
      type: Object as PropType<AppMenuProps>,
    },
    pickerProps: {
      default(): AppDatePickerProps {
        return {};
      },
      type: Object as PropType<AppDatePickerProps>,
    },
  },
  data() {
    return {
      menu: false,
      textValue: null as any,
      pickerValue: null as any,
    };
  },
  computed: {
    listeners(): any {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      delete listeners.change;
      return listeners;
    },
    menuPropsInternal() {
      const defaults: AppMenuProps = {
        minWidth: 'auto',
        openOnClick: false,
      };
      const attrs: AppMenuProps = {
        ...defaults,
        ...this.menuProps,
      };
      const overrides: AppMenuProps = {
        disabled: VuePropHelper.toBoolean(this.props.disabled) || VuePropHelper.toBoolean(this.props.readonly),
        nudgeBottom: this.menuOffsetY ? (VuePropHelper.toBoolean(this.props.dense) || VuePropHelper.toBoolean(this.props.denseX) ? 27 : 45) : undefined,
      };
      return {
        ...attrs,
        ...overrides,
      };
    },
    props() {
      const defaults: ComponentProxyProps = {
        appendIcon: mdiCalendar,
        appendIconTabindex: -1,
      };
      const attrs: ComponentProxyProps = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProxyProps = {
        //
      };
      delete attrs[this.$options.model!.prop!];
      return {
        ...attrs,
        ...overrides,
      };
    },
  },
  watch: {
    'attrs.value': {
      handler(val: any, _oldVal: any) {
        // if (this.value === val) {
        //   return;
        // }
        // const value = val?.toString();
        // if (!!value && dayjs(value).isValid()) {
        //   this.value = dayjs(value).format('YYYY-MM-DD');
        // } else {
        //   this.value = null;
        // }
        // if (this.value !== val) {
        //   this.$emit('input', this.value);
        //   this.$emit('change', this.value);
        // }
      },
      immediate: true,
    },
  },
  methods: {
    onTextChange(val: any) {
      const values = String(val)
        .split(/[～,\s]/)
        .map(x => dayjs(x))
        .filter(x => x.isValid())
        .map(x => x.format('YYYY-MM-DD'));
      if (this.pickerProps.range) {
        // const textValue = values.length === 2 ? values : null;
        const pickerValue = values.length === 2 ? values : null;
        // this.textValue = textValue;
        this.pickerValue = pickerValue;
        // this.$emit('input', pickerValue);
        // this.$emit('change', pickerValue);
      } else if (this.pickerProps.multiple) {
        // const textValue = values;
        const pickerValue = values;
        // this.textValue = textValue;
        this.pickerValue = pickerValue;
        // this.$emit('input', pickerValue);
        // this.$emit('change', pickerValue);
      } else {
        // const textValue = values.length === 1 ? values[0] : null;
        const pickerValue = values.length === 1 ? values[0] : null;
        // this.textValue = textValue;
        this.pickerValue = pickerValue;
        // this.$emit('input', pickerValue);
        // this.$emit('change', pickerValue);
      }
    },
    onClickClear() {
      this.textValue = null;
      this.pickerValue = null;
      this.$emit('input', this.textValue);
      this.$emit('change', this.textValue);
    },
    onPickerInput() {
      if (this.pickerProps.multiple && !this.pickerProps.range && Array.isArray(this.pickerValue)) {
        this.textValue = this.pickerValue.sort();
        this.$emit('input', this.pickerValue);
        this.$emit('change', this.pickerValue);
      }
    },
    onPickerChange() {
      this.menu = false;
      if (this.pickerProps.range && Array.isArray(this.pickerValue)) {
        this.textValue = this.pickerValue.sort().join('～');
      } else {
        this.textValue = this.pickerValue;
      }
      this.$emit('input', this.pickerValue);
      this.$emit('change', this.pickerValue);
    },
  },
});
</script>

<template>
  <app-text-field ref="field" v-model="textValue" v-bind="props" v-on="listeners" @change="onTextChange" @click:append="menu = true" @click:clear="onClickClear">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
    <template v-slot:label>
      <app-menu v-model="menu" v-bind="menuPropsInternal" :activator="$refs.field">
        <template v-slot>
          <app-date-picker v-model="pickerValue" v-bind="pickerProps" @change="onPickerChange" @input="onPickerInput" />
        </template>
      </app-menu>
      <slot name="label">
        {{ props.label }}
      </slot>
    </template>
  </app-text-field>
</template>
