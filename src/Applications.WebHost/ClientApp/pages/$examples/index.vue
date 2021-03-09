<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
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
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
        { text: 'Xxx', value: 'x' },
      ],
      items: [
        { label: 'Label', text: null, select: null, checkbox: false, switch: false, radio: 0, textarea: null },
        { label: 'Label', text: null, select: null, checkbox: false, switch: false, radio: 0, textarea: null },
      ],
      selections: [
        { text: 'text0', value: 0 },
        { text: 'text1', value: 1 },
        { text: 'text2', value: 2 },
      ],
    };
  },
});
</script>

<template>
  <div style="width: 100%;">
    <v-form autocomplete="off" @submit.prevent>
      <v-row>
        <v-col>
          <app-text-field label="VTextField" vee-rules="required" />
        </v-col>
        <v-col>
          <v-btn class="mt-3">
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
            <template v-slot:item.text>
              <v-row dense>
                <v-col>
                  <v-text-field class="mt-0" dense hide-details />
                </v-col>
              </v-row>
              <v-row dense>
                <v-col>
                  <v-text-field class="mt-0" dense hide-details />
                </v-col>
              </v-row>
            </template>
            <template v-slot:item.select>
              <v-row dense>
                <v-col>
                  <v-select class="mt-0" clearable dense hide-details :items="selections" />
                </v-col>
              </v-row>
              <v-row dense>
                <v-col>
                  &nbsp;
                </v-col>
              </v-row>
            </template>
            <template v-slot:item.checkbox>
              <v-checkbox class="mt-0 fit" dense hide-details style="padding: 1px 0;" />
            </template>
            <template v-slot:item.switch>
              <v-switch class="mt-0 fit" dense hide-details style="padding: 1px 0;" />
            </template>
            <template v-slot:item.radiobutton>
              <v-radio-group class="mt-0" dense hide-details row style="padding: 1px 0;">
                <v-radio label="Val1" />
                <v-radio label="Val2" />
              </v-radio-group>
            </template>
            <template v-slot:item.textarea>
              <v-row dense style="height: 100%;">
                <v-col>
                  <v-textarea class="mt-0" dense hide-details rows="2" />
                </v-col>
              </v-row>
            </template>
          </v-data-table>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-data-table disable-pagination :headers="headers" hide-default-footer :items="items">
            <template v-slot:item.label="{ value }">
              <div style="font-size: 16px; padding-top: 2px; text-align: right;">
                {{ value }}
              </div>
            </template>
            <template v-slot:item.text>
              <v-text-field class="mt-0" dense hide-details />
            </template>
            <template v-slot:item.select>
              <v-select class="mt-0" clearable dense hide-details :items="selections" />
            </template>
            <template v-slot:item.checkbox>
              <v-checkbox class="mt-0" dense hide-details />
            </template>
            <template v-slot:item.switch>
              <v-switch class="mt-0" dense hide-details />
            </template>
            <template v-slot:item.radiobutton>
              <v-radio-group class="mt-0" dense hide-details row>
                <v-radio label="Val1" />
                <v-radio label="Val2" />
              </v-radio-group>
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
</style>

<style lang="scss" scoped>
.fit ::v-deep {
  width: fit-content;

  .v-input--selection-controls__input {
    margin-right: 0;
  }
}
</style>
