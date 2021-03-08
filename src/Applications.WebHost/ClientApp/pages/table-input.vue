<script lang="ts">
import cloneDeep from 'lodash/cloneDeep';
import { validate } from 'vee-validate';
import Vue from 'vue';
import valdationMixin, { autoValidate } from './-table-input';
import mixins from '~/extensions/mixins';

type Validations = Record<string, any>;

interface Ent {
  value1: string;
  nest: {
    nestvalue1: string;
  };
  array: {
    arrayvalue1: string;
  }[];
}

export default mixins(valdationMixin).extend({
  data() {
    return {
      value1: '' as string,
      value2: '' as string,
      file: null as any,
      xentity: {
        value1: '' as string,
        nest: {
          nestvalue1: '' as string,
        },
        array: [
          {
            arrayvalue1: '' as string,
          },
          {
            arrayvalue1: '' as string,
          },
        ],
      },
      xerrors: {
        value1: [] as string[],
        nest: {
          nestvalue1: [] as string[],
        },
        array: [
          {
            arrayvalue1: [] as string[],
          },
          {
            arrayvalue1: [] as string[],
          },
        ],
      },
      xvalidation: {
        value1: {
          rules: 'required|max:10',
          options: { name: 'value1' },
        },
        nest: {
          nestvalue1: {
            rules: 'required|max:10',
            options: { name: 'nestvalue1' },
          },
        },
        array: (i: number) => {
          return {
            arrayvalue1: {
              rules: 'required|max:10',
              options: { name: `arrayvalue1[${i}]` },
            },
          };
        },
      },
    };
  },
  created(): void {
    // this.errors.value1 = ['xxx'];
  },
  methods: {
    async validate(): Promise<void> {
      const errors = cloneDeep(this.xerrors);
      const result = await autoValidate<Ent>(this.xentity, this.xvalidation, errors);
      console.log('result', result);
      console.log('entity', this.xentity);
      console.log('errors', errors);
      console.log('validation', this.xvalidation);
      this.xerrors = errors;
    },
  },
});
</script>

<template>
  <v-form @submit.prevent>
    <v-btn @click="validate">
      validate
    </v-btn>
    <div class="my-1" style="width: 350px;">
      <!-- prettier-ignore -->
      <app-text-field v-model="xentity.value1"
                      dense-x
                      :error-messages.sync="xerrors.value1"
                      :format="{ mantissa: 2, thousandSeparated: true, }"
                      hide-details="tooltip"
                      lazy-rendering
                      prefix="$"
                      suffix="%"
                      :vee-name="xvalidation.value1.options.name"
                      :vee-rules="xvalidation.value1.rules"
      />
    </div>
    <div class="my-1" style="width: 350px;">
      <!-- prettier-ignore -->
      <app-text-number-field v-model="xentity.value1"
                             dense-x
                             :error-messages.sync="xerrors.value1"
                             :format="{ mantissa: 2, thousandSeparated: true, }"
                             hide-details="tooltip"
                             lazy-rendering
                             prefix="$"
                             suffix="%"
                             :vee-name="xvalidation.value1.options.name"
                             :vee-rules="xvalidation.value1.rules"
      />
    </div>
    <div class="my-1" style="width: 350px;">
      <!-- prettier-ignore -->
      <app-text-number-field dense-x
                             :format="{ mantissa: 2, thousandSeparated: true, }"
                             hide-details="tooltip"
                             suffix="%"
                             vee-rules="required|length:10"
      />
    </div>
  </v-form>
</template>
