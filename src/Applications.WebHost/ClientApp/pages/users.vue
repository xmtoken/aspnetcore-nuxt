<script>
import { ApiFields } from './users';
import { AppAlertSnackbar, AppDataTable, AppMenu } from '~/components';
import { format_date, labeled } from '~/filters';
import { mdiDeleteOutline, mdiMagnify, mdiPlusCircleOutline } from '@mdi/js';
import QueryString from 'qs';
export default {
  components: {
    AppAlertSnackbar,
    AppDataTable,
    AppMenu,
  },
  filters: {
    format_date,
    labeled,
  },
  data() {
    return {
      icons: {
        mdiDeleteOutline,
        mdiMagnify,
        mdiPlusCircleOutline,
      },
      loading: {
        paginate: false,
        remove: false,
      },
      headers: [
        { text: 'Name', value: 'name' },
        { text: 'EmailAddress', value: 'profile.emailAddress' },
        { text: 'Birthday', value: 'profile.birthday' },
        { text: 'Gender', value: 'profile.gender' },
      ],
      items: [],
      itemsPerPage: null,
      serverItemsLength: null,
      selected: [],
    };
  },
  watch: {
    '$route.query'(newQuery, oldQuery) {
      const isRoutedByNavigationMenu = Object.keys(newQuery).length === 0;
      const isRequiedReload = isRoutedByNavigationMenu && Object.values(oldQuery).filter(x => x).length !== 0;
      if (isRequiedReload) {
        this.paginate(this.$route.query);
      }
    },
  },
  created() {
    this.paginate(this.$route.query);
  },
  methods: {
    async paginate(query) {
      try {
        this.loading.paginate = true;
        const data = await this.$axios.$get('/users', {
          params: { ...query, fields: ApiFields },
          paramsSerializer: params => QueryString.stringify(params, { indices: false }),
        });
        this.items = data.currentPageItems;
        this.itemsPerPage = data.currentPageSize;
        this.serverItemsLength = data.totalItemCount;
        this.selected = [];
      } finally {
        this.loading.paginate = false;
      }
    },
    async remove() {
      try {
        this.loading.remove = true;
        this.$refs.snackbar.reset();
        await this.$axios.$delete('/users', {
          params: { ids: this.selected.map(x => x.id) },
          paramsSerializer: params => QueryString.stringify(params, { indices: false }),
        });
        this.$refs.snackbar.setSuccesses('ユーザーを削除しました。');
        await this.paginate(this.$route.query);
      } finally {
        this.loading.remove = false;
      }
    },
  },
};
</script>

<template>
  <div>
    <app-alert-snackbar ref="snackbar" top />
    <v-row dense>
      <v-col cols="auto">
        <v-btn color="primary" depressed nuxt :to="{ path: '/users/new', query: $route.query }" width="125">
          New
          <v-icon right>
            {{ icons.mdiPlusCircleOutline }}
          </v-icon>
        </v-btn>
      </v-col>
      <v-col cols="auto">
        <v-btn color="primary" depressed nuxt :to="{ path: '/users/search', query: $route.query }" width="125">
          Search
          <v-icon right>
            {{ icons.mdiMagnify }}
          </v-icon>
        </v-btn>
      </v-col>
      <v-col cols="auto">
        <app-menu offset-y>
          <template v-slot:activator="{ on }">
            <v-btn color="error" depressed :disabled="loading.remove || selected.length === 0" :loading="loading.remove" width="125" v-on="on">
              Remove
              <v-icon right>
                {{ icons.mdiDeleteOutline }}
              </v-icon>
            </v-btn>
          </template>
          <template v-slot="{ close }">
            <v-card>
              <v-card-text>
                選択しているすべてのユーザーを削除します。
              </v-card-text>
              <v-card-actions>
                <v-spacer />
                <v-btn text @click="close">
                  Cancel
                </v-btn>
                <v-btn color="error" text @click="[close(), remove()]">
                  Remove
                </v-btn>
              </v-card-actions>
            </v-card>
          </template>
        </app-menu>
      </v-col>
      <v-col />
    </v-row>
    <v-row dense>
      <v-col>
        <app-data-table v-model="selected" class="fluid-100vh" fixed-header :headers="headers" item-key="id" :items="items" :items-per-page="itemsPerPage" :loading="loading.paginate" :server-items-length="serverItemsLength" show-select @routed="paginate">
          <template v-slot:item.name="{ item }">
            <nuxt-link :to="{ path: `/users/${item.id}`, query: $route.query }">
              {{ item.name }}
            </nuxt-link>
          </template>
          <template v-slot:item.profile.birthday="{ item }">
            {{ item.profile.birthday | format_date('YYYY-MM-DD') }}
          </template>
          <template v-slot:item.profile.gender="{ item }">
            {{ item.profile.gender | labeled($enum.gender) }}
          </template>
        </app-data-table>
      </v-col>
    </v-row>
    <nuxt-child @routed="paginate" />
  </div>
</template>
