<script lang="ts">
import { VueBuilder } from '~/core/vue';

type Parent = {
  a: string;
  b: string;
  c: string;
  child1: Child;
  child2: Child;
};

type Child = {
  d: string;
  e: string;
  f: string;
};

type FilteredKeys<T, U> = {
  [P in keyof T]: T[P] extends U ? P : never;
}[keyof T];

class Api<T> {
  static response<T>(): Api<T> {
    return ({} as any) as Api<T>;
  }

  fields<TKey extends keyof T>(fields: readonly TKey[]): readonly TKey[] {
    return fields;
  }

  nest<TKey extends keyof { [Q in FilteredKeys<T, Record<string, any>>]: T[Q] }>(fields: TKey): ApiNest<T[TKey]> {
    return new ApiNest<TKey>(fields as string);
  }
}

class ApiNest<T> {
  private readonly prefix: string;

  constructor(prefix: string) {
    this.prefix = prefix;
  }

  fields<TKey extends keyof T>(fields: readonly TKey[]): readonly TKey[] {
    return fields;
  }
}

const Vue = VueBuilder.create() //
  .build();

export default Vue.extend({
  data() {
    return {
      items: [
        { text: 'Text1', value: 1 },
        { text: 'Text2', value: 2 },
        { text: 'Text3', value: 3 },
      ],
      autocomplete_value: null as any,
      checkbox_value: null as any,
      combobox_value: null as any,
      fileinput_value: null as any,
      radiogroup_value: null as any,
      select_value: null as any,
      select_values: null as any,
      switch_value: null as any,
      textfield_value: null as any,
    };
  },
  async created() {
    const aaa = await this.typetestb();
    aaa.a = '';
  },
  methods: {
    async typetestb() {
      const fields = new Api<Parent>().fields(['a', 'c', 'child1', 'child2']);
      type ResponseType = Pick<Parent, typeof fields[number]>;
      const response = await this.$axios.$get<ResponseType>('/xxx', {
        params: { fields },
      });

      const xxx = new Api<Parent>().nest('child1').fields(['d', 'f']);

      type obj = {
        [Q in FilteredKeys<Parent, Record<string, any>>]: Parent[Q];
      };

      return response;
    },
    saveToQuery(): void {
      //
      const query = {
        autocomplete_value: this.autocomplete_value || undefined,
        checkbox_value: this.checkbox_value || undefined,
        combobox_value: this.combobox_value || undefined,
        fileinput_value: this.fileinput_value || undefined,
        radiogroup_value: this.radiogroup_value || undefined,
        select_value: this.select_value || undefined,
        select_values: this.select_values || undefined,
        switch_value: this.switch_value || undefined,
        textfield_value: this.textfield_value || undefined,
      };
      this.$router.push({ path: this.$route.path, query });
    },
    readFromQuery(): void {
      //
      this.autocomplete_value = this.$route.query.autocomplete_value;
      this.checkbox_value = this.$route.query.checkbox_value;
      this.combobox_value = this.$route.query.combobox_value;
      this.fileinput_value = this.$route.query.fileinput_value;
      this.radiogroup_value = this.$route.query.radiogroup_value;
      this.select_value = this.$route.query.select_value;
      this.select_values = this.$route.query.select_values;
      this.switch_value = this.$route.query.switch_value;
      this.textfield_value = this.$route.query.textfield_value;
      console.log(this.$data);
    },
  },
});
</script>

<template>
  <v-card>
    <v-card-text>
      <v-form autocomplete="off">
        <v-row dense>
          <v-col cols="auto">
            <v-btn @click="saveToQuery">
              SaveToQuery
            </v-btn>
          </v-col>
          <v-col cols="auto">
            <v-btn @click="readFromQuery">
              ReadFromQuery
            </v-btn>
          </v-col>
        </v-row>
        <v-row dense>
          <v-col cols="auto">
            <app-autocomplete v-model="autocomplete_value" hide-details="auto" :hint="autocomplete_value" :items="items" label="Autocomplete" persistent-hint :value-comparator="(a, b) => Number(a) === Number(b)" />
          </v-col>
          <v-col cols="auto">
            <app-checkbox v-model="checkbox_value" hide-details="auto" :hint="checkbox_value" label="Checkbox" persistent-hint :value-converter="x => Boolean(x)" />
          </v-col>
          <v-col cols="auto">
            <app-combobox v-model="combobox_value" hide-details="auto" :hint="combobox_value" :items="items" label="Combobox" persistent-hint />
          </v-col>
          <v-col>
            <app-file-input v-model="fileinput_value" hide-details="auto" :hint="fileinput_value" label="FileInput" persistent-hint />
          </v-col>
        </v-row>
        <v-row dense>
          <v-col cols="auto">
            <app-radio-group v-model="radiogroup_value" hide-details="auto" :hint="radiogroup_value" label="RadioGroup" persistent-hint row :value-converter="x => Number(x)">
              <v-radio label="Radio1" :value="1" />
              <v-radio label="Radio2" :value="2" />
            </app-radio-group>
          </v-col>
          <v-col cols="auto">
            <app-select v-model="select_value" hide-details="auto" :hint="select_value" :items="items" label="Select" persistent-hint :value-comparator="(a, b) => Number(a) === Number(b)" />
          </v-col>
          <v-col cols="auto">
            <app-switch v-model="switch_value" hide-details="auto" :hint="switch_value" label="Switch" persistent-hint :value-converter="x => Boolean(x)" />
          </v-col>
          <v-col cols="auto">
            <app-text-field v-model="textfield_value" hide-details="auto" :hint="textfield_value" label="TextFiled" persistent-hint />
          </v-col>
        </v-row>
        <v-row dense>
          <v-col cols="auto">
            <app-select v-model="select_values" hide-details="auto" :hint="select_values" :items="items" label="Select-Multi" multiple persistent-hint :value-comparator="(a, b) => Number(a) === Number(b)" />
          </v-col>
        </v-row>
      </v-form>
    </v-card-text>
  </v-card>
</template>

<style lang="scss">
.v-card .v-text-field {
  margin-top: 0;
}
</style>
