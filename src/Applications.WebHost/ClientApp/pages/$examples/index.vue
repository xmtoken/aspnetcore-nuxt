<script lang="ts">
import Vue from 'vue';
import AppRadioGroup from '~/components/AppRadioGroup/AppRadioGroup.vue';

export default Vue.extend({
  components: { AppRadioGroup },
  data() {
    return {
      headers: [
        { text: 'Label', value: 'label' },
        { text: 'Text', value: 'text' },
        { text: 'TextArea', value: 'textarea' },
        { text: 'Select', value: 'select' },
        { text: 'Checkbox', value: 'checkbox' },
        { text: 'Switch', value: 'switch' },
        { text: 'RadioButton', value: 'radiobutton' },
      ],
      items: [
        { label: 'Label', text: null, select: null, checkbox: 'true', switch: 'true', radio: 0, textarea: null },
        // { label: 'Label', text: null, select: null, checkbox: null, switch: false, radio: 0, textarea: null },
      ],
      selections: [
        { text: 'text0', value: 0 },
        { text: 'text1', value: 1 },
        { text: 'text2', value: 2 },
      ],
      autocomplete: null,
      combobox: null,
      textarea: null,
      textbox: null,
      textdatefield: null,
      checkbox: null,
      switch_: null,
      radio: null,
      select: null,
    };
  },
  created() {
    console.log(this.$vuetify);
  },
  methods: {
    onInput(val) {
      console.log('onInput', val);
    },
    onChange(val) {
      console.log('onChange', val);
    },
    onClickButton() {
      this.checkbox = true;
    },
  },
});
</script>

<template>
  <div style="width: 100%;">
    <v-form autocomplete="off" @submit.prevent>
      <v-row>
        <v-col>
          <app-autocomplete v-model="autocomplete" clearable dense-x hide-details="tooltip" :items="selections" label="Autocomplete" vee-rules="required" @change="onChange" @input="onInput" />
        </v-col>
        <v-col>
          <app-combobox v-model="combobox" clearable dense-x hide-details="tooltip" :items="selections" label="Combobox" vee-rules="required" @change="onChange" @input="onInput" />
        </v-col>
        <v-col>
          <app-textarea v-model="textarea" dense-x hide-details="tooltip" label="Textarea" vee-rules="required" />
        </v-col>
        <v-col>
          <app-text-field v-model="textbox" clearable dense-x hide-details="tooltip" label="TextField" vee-rules="required" />
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <app-text-date-field v-model="textdatefield" clearable dense-x hide-details="tooltip" :label="`${textdatefield}`" :picker-props="{ multiple: true, range: false, type: 'date' }" vee-rules="required" @change="onChange" @input="onInput" />
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <!-- TODO:Checkbox:required -->
          <app-checkbox v-model="checkbox" dense-x hide-details="tooltip" label="Checkbox" :vee-rules="{ required: { allowFalse: false } }" @change="onChange" @input="onInput" />
        </v-col>
        <v-col>
          <!-- TODO:Switch:required -->
          <app-switch v-model="switch_" dense-x hide-details="tooltip" label="Switch" :vee-rules="{ required: { allowFalse: false } }" @change="onChange" @input="onInput" />
        </v-col>
        <v-col>
          <app-radio-group v-model="radio" dense-x hide-details="tooltip" row vee-rules="between:-1,0" @change="onChange" @input="onInput">
            <v-radio label="label1" :value="-1" />
            <v-radio label="label2" :value="2" />
          </app-radio-group>
        </v-col>
        <v-col>
          <app-select v-model="select" clearable dense-x hide-details="tooltip" :items="selections" vee-rules="required" @change="onChange" @input="onInput" />
        </v-col>
        <v-col>
          <v-btn class="mt-3" @click="onClickButton">
            Button
          </v-btn>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-data-table disable-pagination :headers="headers" hide-default-footer :items="items">
            <template v-slot:item.label="{ value }">
              <v-row dense>
                <v-col>
                  <div style="font-size: 16px; padding-top: 2px; text-align: right;">
                    &nbsp;
                  </div>
                  <!-- <div class="v-label" style="padding-top: 2px;">
                    &nbsp;
                  </div> -->
                </v-col>
              </v-row>
              <v-row dense>
                <v-col>
                  <div style="font-size: 16px; padding-top: 2px; text-align: right;">
                    {{ value }}
                  </div>
                  <!-- <div class="v-label" style="padding-top: 2px;">
                    {{ value }}
                  </div> -->
                </v-col>
              </v-row>
            </template>
            <template v-slot:item.text="{ item }">
              <v-row dense>
                <v-col>
                  <app-text-field v-model="item.text" dense-x hide-details="tooltip" vee-rules="required" />
                </v-col>
              </v-row>
              <v-row dense>
                <v-col>
                  <app-text-field v-model="item.text" dense-x hide-details="tooltip" vee-rules="required" />
                </v-col>
              </v-row>
            </template>
            <template v-slot:item.select="{ item }">
              <v-row dense>
                <v-col>
                  <app-select v-model="item.select" clearable dense-x hide-details="tooltip" :items="selections" vee-rules="required" />
                </v-col>
              </v-row>
              <v-row dense>
                <v-col>
                  &nbsp;
                </v-col>
              </v-row>
            </template>
            <template v-slot:item.checkbox="{ item }">
              <app-checkbox v-model="item.checkbox" dense-x hide-details />
            </template>
            <template v-slot:item.switch="{ item }">
              <app-switch v-model="item.switch" dense-x hide-details />
            </template>
            <template v-slot:item.radiobutton>
              <app-radio-group class="mt-0" dense-x hide-details row>
                <v-radio label="Val1" />
                <v-radio label="Val2" />
              </app-radio-group>
            </template>
            <template v-slot:item.textarea>
              <v-row dense style="height: 100%;">
                <v-col>
                  <app-textarea dense-x hide-details="tooltip" no-resize rows="2" />
                </v-col>
              </v-row>
              <!-- <v-row dense style="height: 100%;">
                <v-col align-self="end" style="height: 100%;">
                  <v-textarea class="mt-0 textarea100" dense hide-details no-resize rows="2" />
                </v-col>
              </v-row> -->
            </template>
          </v-data-table>
        </v-col>
      </v-row>
    </v-form>
  </div>
</template>

<style lang="scss">
.v-data-table td {
  padding-bottom: 4px !important;
  padding-top: 4px !important;
}

.v-input--radio-group .v-input--radio-group__input {
  flex-wrap: nowrap;
}

.textarea100 {
  height: 100%;

  .v-input__control {
    height: 100%;

    .v-input__slot {
      height: 100%;
    }
  }
}
</style>
