<script lang="ts">
import { ValidationObserver, ValidationProvider } from 'vee-validate';
import Vue from 'vue';

export default Vue.extend({
  components: {
    ValidationObserver,
    ValidationProvider,
  },
  data() {
    return {
      headers: [
        { text: 'text1', value: 'col1', width: 200, fixed: 'left' },
        { text: 'text2', value: 'col2', width: 200, fixed: 'left' },
        { text: 'text3', value: 'col3', width: 200 },
        { text: 'text4', value: 'col4', width: 200 },
        { text: 'text5', value: 'col5', width: 200 },
        { text: 'text6', value: 'col6', width: 200 },
        { text: 'text7', value: 'col7', width: 200 },
        { text: 'text8', value: 'col8', width: 200, fixed: 'right' },
        { text: 'text9', value: 'col9', width: 200, fixed: 'right' },
      ],
      items: [],
      bench: 0,
    };
  },
  created(): void {
    for (let i = 0; i < 20; i++) {
      this.items.push({
        val11: null,
        val21: null,
        val31: null,
        val41: null,
        val51: null,
        val61: null,
        val71: null,
        val81: null,
        val91: null,
        val12: null,
        val22: null,
        val32: null,
        val42: null,
        val52: null,
        val62: null,
        val72: null,
        val82: null,
        val92: null,
        index: i.toString(),
      });
    }
  },
  methods: {
    render(): void {
      console.log('render');
    },
    validate(): void {
      this.bench = this.items.length;
      this.$refs.observer.validate();
    },
  },
});
</script>

<template>
  <div style="width: 100%;">
    <v-btn @click="validate">
      validate
    </v-btn>
    <validation-observer ref="observer" slim>
      <app-data-table disable-pagination fixed-header fixed-select :headers="headers" height="calc(100vh - 200px)" item-key="index" :items="items" show-select>
        <template v-slot:item.data-table-select="{ isSelected, select }">
          <app-scope v-slot>
            <v-simple-checkbox :value="isSelected" @click="select" />
          </app-scope>
        </template>
        <template v-slot:item.col1="{ item }">
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val11" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val12" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
        </template>
        <template v-slot:item.col2="{ item }">
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val21" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val22" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
        </template>
        <template v-slot:item.col3="{ item }">
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val31" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val32" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
        </template>
        <template v-slot:item.col4="{ item }">
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val41" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val42" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
        </template>
        <template v-slot:item.col5="{ item }">
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val51" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val52" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
        </template>
        <template v-slot:item.col6="{ item }">
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val61" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val62" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
        </template>
        <template v-slot:item.col7="{ item }">
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val71" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val72" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
        </template>
        <template v-slot:item.col8="{ item }">
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val81" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val82" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
        </template>
        <template v-slot:item.col9="{ item }">
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val91" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
          <div>
            <app-scope v-slot>
              <app-text-field v-model="item.val92" dense hide-details vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
        </template>
      </app-data-table>
    </validation-observer>
  </div>
</template>
