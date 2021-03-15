<script lang="ts">
import { mdiCalendar } from '@mdi/js';
import dayjs from 'dayjs';
import deepEqual from 'deep-equal';
import { PropType } from 'vue';
import { VTextField } from 'vuetify/src/components/VTextField';
import { AppDatePickerProps } from '~/components/AppDatePicker/AppDatePicker.vue';
import { AppMenuProps } from '~/components/AppMenu/AppMenu.vue';
import { AppTextFieldProps } from '~/components/AppTextField/AppTextField.vue';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import { Slotable } from '~/mixins/slotable';
import { PickProp, ProxyProps } from '~/types/global';

type ComponentProxyProps = ProxyProps &
  AppTextFieldProps & {
    value?: PickProp<AppDatePickerProps, 'value'>;
  };

type ComponentProps = ComponentProxyProps & {
  format?: string | null;
  menuOffsetY?: boolean | null;
  menuProps?: AppMenuProps | null;
  multipleSeparator?: string | null;
  pickerProps?: AppDatePickerProps | null;
  rangeSeparator?: string | null;
};

export type AppTextDateFieldProps = ComponentProps;

type ComponentRefs = {
  field: InstanceType<typeof VTextField>;
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
    format: {
      default: undefined,
      type: (null as any) as PropType<PickProp<ComponentProps, 'format'>>,
    },
    menuOffsetY: {
      default: true,
      type: (null as any) as PropType<PickProp<ComponentProps, 'menuOffsetY'>>,
    },
    menuProps: {
      default() {
        return {};
      },
      type: (null as any) as PropType<PickProp<ComponentProps, 'menuProps'>>,
    },
    multipleSeparator: {
      default: ', ',
      type: (null as any) as PropType<PickProp<ComponentProps, 'multipleSeparator'>>,
    },
    pickerProps: {
      default() {
        return {};
      },
      type: (null as any) as PropType<PickProp<ComponentProps, 'pickerProps'>>,
    },
    rangeSeparator: {
      default: ' ~ ',
      type: (null as any) as PropType<PickProp<ComponentProps, 'rangeSeparator'>>,
    },
  },
  data() {
    return {
      menu: false,
      pickerValue: null as PickProp<ComponentProps, 'value'>,
      textLatestValue: null as PickProp<ComponentProps, 'value'>,
      textValue: null as PickProp<ComponentProps, 'value'>,
    };
  },
  computed: {
    listeners() {
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
      const attrs: AppMenuProps & Record<string, any> = {
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
      const attrs: ComponentProxyProps & Record<string, any> = {
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
    raiseValue(): any {
      return this.pickerProps?.multiple || this.pickerProps?.range //
        ? this.pickerValues
        : this.pickerValues[0] ?? null;
    },
    pickerValues(): any[] {
      return this.pickerValue //
        ? Array.isArray(this.pickerValue)
          ? [...this.pickerValue].sort()
          : [this.pickerValue]
        : [];
    },
    formattedPickerValues(): string[] {
      const format = this.format ?? (this.pickerProps?.type === 'month' ? 'YYYY-MM' : 'YYYY-MM-DD');
      return this.pickerValues.map(x => dayjs(x).format(format));
    },
    formattedPickerValuesText(): string {
      return this.pickerProps?.range //
        ? this.formattedPickerValues.join(this.rangeSeparator ?? undefined)
        : this.pickerProps?.multiple
        ? this.formattedPickerValues.join(this.multipleSeparator ?? undefined)
        : this.formattedPickerValues[0];
    },
    isValidPickerValuesCount(): boolean {
      return this.pickerProps?.range //
        ? this.pickerValues.length === 2
        : this.pickerProps?.multiple
        ? true
        : this.pickerValues.length <= 1;
    },
  },
  watch: {
    'attrs.value': {
      handler(val: PickProp<ComponentProps, 'value'>, _oldVal: PickProp<ComponentProps, 'value'>) {
        const values = Array.isArray(val) ? val : [val];
        if (deepEqual(this.pickerValues, values)) {
          return;
        }
        this.parseText(val);
        if (!deepEqual(this.raiseValue, val)) {
          this.raise({ force: true });
        }
      },
      immediate: true,
    },
    'pickerProps.multiple'(_val: PickProp<AppDatePickerProps, 'multiple'>, _oldVal: PickProp<AppDatePickerProps, 'multiple'>) {
      if (!this.validatePickerValuesCount(this.pickerValues)) {
        this.reset();
      }
    },
    'pickerProps.range'(_val: PickProp<AppDatePickerProps, 'range'>, _oldVal: PickProp<AppDatePickerProps, 'range'>) {
      if (!this.validatePickerValuesCount(this.pickerValues)) {
        this.reset();
      }
    },
    'pickerProps.type'(_val: PickProp<AppDatePickerProps, 'type'>, _oldVal: PickProp<AppDatePickerProps, 'type'>) {
      this.parseText(this.textValue);
      this.raise({ force: true });
    },
  },
  methods: {
    raise(val: { force: boolean }) {
      this.$emit('input', this.raiseValue);
      if (val.force || !deepEqual(this.textValue, this.textLatestValue)) {
        this.$emit('change', this.raiseValue);
        this.textLatestValue = this.textValue;
      }
    },
    reset() {
      this.pickerValue = this.pickerProps?.multiple || this.pickerProps?.range ? [] : null;
      this.textValue = null;
      this.raise({ force: false });
    },
    validatePickerValuesCount(values: string[]): boolean {
      return this.pickerProps?.range //
        ? values.length === 2
        : this.pickerProps?.multiple
        ? true
        : values.length <= 1;
    },
    parseText(val: any) {
      const formatters = [
        this.format, //
        'YYYY-M-DD',
        'YY-M-D',
        this.pickerProps?.type === 'month' ? 'YY-M' : 'M-D',
        this.pickerProps?.type === 'month' ? 'M' : 'D',
      ];
      const values = String(val)
        .split(new RegExp(`[${this.multipleSeparator?.trim()}${this.rangeSeparator?.trim()},\\s]`))
        .map(x => dayjs(x, formatters))
        .filter(x => x.isValid())
        .map(x => (this.pickerProps?.type === 'month' ? x.format('YYYY-MM') : x.format('YYYY-MM-DD')));
      const isValid = this.validatePickerValuesCount(values);
      if (isValid) {
        this.pickerValue =
          this.pickerProps?.multiple || this.pickerProps?.range //
            ? values
            : values[0];
        this.textValue = this.formattedPickerValuesText;
        return true;
      } else {
        this.textValue = this.textLatestValue;
        return false;
      }
    },
    onTextChange(val: any, oldVal: any) {
      if (deepEqual(val, oldVal)) {
        return;
      }
      const succeeded = this.parseText(val);
      if (succeeded) {
        this.raise({ force: false });
      }
    },
    onClickClear() {
      this.reset();
    },
    onPickerInput() {
      if (this.pickerProps?.multiple && !this.pickerProps?.range) {
        this.textValue = this.formattedPickerValuesText;
        this.raise({ force: false });
      }
    },
    onPickerChange() {
      this.menu = false;
      this.textValue = this.formattedPickerValuesText;
      this.raise({ force: false });
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
