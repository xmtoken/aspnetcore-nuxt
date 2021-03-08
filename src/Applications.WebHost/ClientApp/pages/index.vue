<script lang="ts">
import { ValidationObserver, ValidationProvider } from 'vee-validate';
import { dragscroll } from 'vue-dragscroll';
import AppAlertSnackbar from '~/components/AppAlertSnackbar.vue';
import { CONSTANTS } from '~/core/constants';
import { VueBuilder } from '~/core/vue';

const Vue = VueBuilder.create() //
  .$refs<{
    snackbar: InstanceType<typeof AppAlertSnackbar>;
  }>()
  .build();

export default Vue.extend({
  components: {
    ValidationObserver,
    ValidationProvider,
  },
  directives: {
    dragscroll,
  },
  data() {
    // Promise.all([
    //   this.$axios.$get<{ a: number }>(''), //
    //   this.$axios.$get<{ b: number }>(''), //
    //   this.$axios.$get<{ c: number }>(''), //
    //   this.$axios.$get<{ d: number }>(''), //
    //   this.$axios.$get<{ e: number }>(''), //
    //   this.$axios.$get<{ f: number }>(''), //
    //   this.$axios.$get<{ g: number }>(''), //
    //   this.$axios.$get<{ h: number }>(''), //
    //   this.$axios.$get<{ i: number }>(''), //
    //   this.$axios.$get<{ j: number }>(''), //
    // ]);
    // Promise.all([
    //   this.$axios.$get<{ a: number }>(''), //
    //   this.$axios.$get<{ b: number }>(''), //
    //   this.$axios.$get<{ c: number }>(''), //
    //   this.$axios.$get<{ d: number }>(''), //
    //   this.$axios.$get<{ e: number }>(''), //
    //   this.$axios.$get<{ f: number }>(''), //
    //   this.$axios.$get<{ g: number }>(''), //
    //   this.$axios.$get<{ h: number }>(''), //
    //   this.$axios.$get<{ i: number }>(''), //
    //   this.$axios.$get<{ j: number }>(''), //
    //   this.$axios.$get<{ k: number }>(''), //
    // ]);
    return {
      headers: [
        { text: 'index', value: 'index', fixed: 'left', width: 100 },
        { text: 'text0', value: 'col0', fixed: 'left', width: 100 },
        { text: 'text1', value: 'col1', fixed: 'left', width: 100 },
        { text: 'text2', value: 'col2', width: 350 },
        { text: 'text3', value: 'col3', width: 350 },
        { text: 'text4', value: 'col4', width: 350 },
        { text: 'text5', value: 'col5', width: 350 },
        { text: 'text6', value: 'col6', width: 350 },
        { text: 'text7', value: 'col7', width: 350 },
        { text: 'text8', value: 'col8', width: 350 },
        { text: 'text9', value: 'col9', fixed: 'right', width: 350 },
      ],
      items: [],
      selected: [],
      bench: 0,
      draggable: false,
    };
  },
  computed: {
    xxx() {
      return 1;
    },
  },
  created(): void {
    for (let i = 0; i < 3; i++) {
      this.items.push({
        val01: null,
        val11: null,
        val21: null,
        val31: null,
        val41: null,
        val51: null,
        val61: null,
        val71: null,
        val81: null,
        val91: null,
        val02: null,
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
    async validate(): Promise<void> {
      // this.headers[1].divider = !this.headers[1].divider;
      // this.draggable = !this.draggable;
      console.log('selected', this.selected);

      const promise = new Promise(() => {
        const x = 1 / 0;
        const b = x.g.b;
      });
      await promise;
    },
    alert(): void {
      console.log('xxx{0}xxx{1}'.format('param1', 'param2'));
      this.$refs.snackbar.setErrors({ '': ['aaa', 'bbb'] });
    },
  },
});
</script>

<template>
  <div style="max-width: 100%;">
    <app-alert-snackbar ref="snackbar" :timeout="-1" top transition="slide-y-transition" type="error" />
    <v-btn @click="validate">
      validate
    </v-btn>
    <v-btn @click="alert">
      Alert
    </v-btn>
    {{ render() }}
    <validation-observer ref="observer" slim>
      <app-data-table class="fixed-column-3_" disable-pagination :draggable="true" fixed-header fixed-select :headers="headers" height="calc(100vh - 250px)" item-key="index" :items="items" show-select>
        <!-- <template v-slot:item.data-table-select="{ isSelected, select }">
          <app-scope v-slot>
            <v-simple-checkbox :value="isSelected" @click="select" />
          </app-scope>
        </template> -->
        <template v-slot:item.col0="{ item }">
          <div>
            {{ render() }}
            <app-scope v-slot>
              <app-text-field v-model="item.val01" dense hide-details="tooltip" vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
          <div>
            <app-scope v-slot>
              <app-textarea v-model="item.val02" dense hide-details="tooltip" vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
        </template>
        <template v-slot:item.col1="{ item }">
          <div>
            <app-scope v-slot>
              <app-select v-model="item.val11" dense hide-details="tooltip" vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
          <div>
            <app-scope v-slot>
              <app-combobox v-model="item.val12" dense hide-details="tooltip" vee-name="Label" vee-rules="required" :vee-vid="item.index" />
            </app-scope>
          </div>
        </template>
        <template v-slot:item.col2="{ item }">
          <div>
            <app-scope v-slot>
              <app-autocomplete v-model="item.val21" dense hide-details="tooltip" vee-name="Label" vee-rules="required" :vee-vid="item.index" />
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

<style lang="scss" scoped>
// .v-data-table ::v-deep {
//   thead tr th:nth-child(1),
//   tbody tr td:nth-child(1) {
//     min-width: 64px;
//   }

//   thead tr th:nth-child(2),
//   tbody tr td:nth-child(2) {
//     left: 64px;
//     min-width: 300px;
//   }

//   thead tr th:nth-child(3),
//   tbody tr td:nth-child(3) {
//     left: 364px;
//     min-width: 300px;
//   }
// }
</style>
