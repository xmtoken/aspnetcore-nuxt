<script lang="ts">
import { mdiContentCopy, mdiDeleteOutline, mdiEyeOutline, mdiMagnify, mdiOpenInNew, mdiPlusCircleOutline } from '@mdi/js';
import { StatusCodes } from 'http-status-codes';
import QueryString from 'qs';
import Vue from 'vue';
import { DataTableItemProps } from 'vuetify/types';
import * as DateFormatter from '~/extensions/formatters/date-formatter';
import * as RouterHelper from '~/extensions/router';
import { IPaginationOfUser, User } from '~/types/api';
import { Query } from '~/types/vue-router';

export default Vue.extend({
  props: {},
  data() {
    return {
      icons: {
        mdiContentCopy,
        mdiDeleteOutline,
        mdiEyeOutline,
        mdiMagnify,
        mdiOpenInNew,
        mdiPlusCircleOutline,
      },
      loadings: {
        paginate: false,
        remove: false,
      },
      headers: [
        { text: 'Name', value: nameof.full<User>(x => x.name) },
        { text: 'EmailAddress', value: nameof.full<User>(x => x.profile!.emailAddress) },
        { text: 'Birthday', value: nameof.full<User>(x => x.profile!.birthday) },
        { text: 'Gender', value: nameof.full<User>(x => x.profile!.gender) },
      ],
      items: [] as User[],
      itemsPerPage: 0 as number,
      serverItemsLength: 0 as number,
      selected: [] as User[],
      contextmenu: {
        positionX: 0 as number,
        positionY: 0 as number,
        value: false,
      },
    };
  },
  watch: {
    '$route.query'(newQuery: Query, oldQuery: Query): void {
      if (RouterHelper.isRequiedReload(newQuery, oldQuery)) {
        this.paginate(this.$route.query);
      }
    },
  },
  created(): void {
    this.paginate(this.$route.query);
  },
  methods: {
    formatDate: DateFormatter.format,
    async paginate(query: Query): Promise<void> {
      try {
        this.loadings.paginate = true;
        const fields = [
          //
          nameof.full<User>(x => x.id),
          nameof.full<User>(x => x.name),
          nameof.full<User>(x => x.profile!.emailAddress),
          nameof.full<User>(x => x.profile!.birthday),
          nameof.full<User>(x => x.profile!.gender),
        ];
        const response = await this.$axios.get<IPaginationOfUser>('/users', {
          params: { ...query, fields },
          paramsSerializer: params => QueryString.stringify(params, { indices: false }),
        });
        if (response.status === StatusCodes.OK) {
          this.items = response.data.currentPageItems!;
          this.itemsPerPage = response.data.currentPageSize;
          this.serverItemsLength = response.data.totalItemCount;
          this.selected = [];
        }
      } finally {
        this.loadings.paginate = false;
      }
    },
    async remove(): Promise<void> {
      try {
        this.loadings.remove = true;
        await this.$axios.$delete('/users', {
          params: { ids: this.selected.map(x => x.id) },
          paramsSerializer: params => QueryString.stringify(params, { indices: false }),
        });
        this.$store.dispatch('notification/add-success', { content: 'ユーザーを削除しました。' });
        await this.paginate(this.$route.query);
      } finally {
        this.loadings.remove = false;
      }
    },
    oncontextmenu(event: MouseEvent, data: DataTableItemProps): void {
      event.preventDefault();
      this.selected = [data.item];
      this.contextmenu.positionX = event.clientX;
      this.contextmenu.positionY = event.clientY;
      this.contextmenu.value = true;
    },
  },
});
</script>

<template>
  <div class="d-flex fill-height flex flex-column">
    <v-row class="flex-grow-0" dense>
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
            <v-btn color="error" depressed :disabled="loadings.remove || selected.length === 0" :loading="loadings.remove" width="125" v-on="on">
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
    <v-row class="flex-grow-1 overflow-hidden" dense>
      <v-col class="fill-height overflow-hidden">
        <app-context-menu close-on-content-click content-class="elevation-3">
          <template v-slot:activator="{ on }">
            <app-data-table v-model="selected" class="fill-height" fixed-header :headers="headers" height="calc(100% - 37px)" item-key="id" :items="items" :items-per-page="itemsPerPage" :loading="loadings.paginate" multi-sort outlined :server-items-length="serverItemsLength" show-select v-on="on" @contextmenu:row="(_, x) => (selected = [x.item])" @route="paginate">
              <template v-slot:item.name="{ item }">
                <nuxt-link :to="{ path: `/users/${item.id}`, query: $route.query }">
                  {{ item.name }}
                </nuxt-link>
              </template>
              <!-- <template v-slot:item.profile.birthday="{ item }">
                {{ item.profile.birthday | formatDate('YYYY-MM-DD') }}
              </template> -->
              <!-- <template v-slot:item.profile.gender="{ item }">
                {{ item.profile.gender | labeled($enum.gender) }}
              </template> -->
            </app-data-table>
          </template>
          <v-list dense>
            <v-list-item>
              <v-list-item-icon>
                <v-icon>
                  {{ icons.mdiEyeOutline }}
                </v-icon>
              </v-list-item-icon>
              <v-list-item-content>
                詳細を表示
              </v-list-item-content>
            </v-list-item>
            <v-list-item>
              <v-list-item-icon>
                <v-icon>
                  {{ icons.mdiOpenInNew }}
                </v-icon>
              </v-list-item-icon>
              <v-list-item-content>
                新しいタブで開く
              </v-list-item-content>
            </v-list-item>
            <v-list-item>
              <v-list-item-icon>
                <v-icon>
                  {{ icons.mdiContentCopy }}
                </v-icon>
              </v-list-item-icon>
              <v-list-item-content>
                リンクをコピー
              </v-list-item-content>
            </v-list-item>
            <v-divider />
            <v-list-item>
              <v-list-item-icon>
                <v-icon class="red--text">
                  {{ icons.mdiDeleteOutline }}
                </v-icon>
              </v-list-item-icon>
              <v-list-item-content class="red--text">
                削除
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </app-context-menu>
        <!-- <app-data-table v-model="selected" class="responsive-100vh" fixed-header :headers="headers" item-key="id" :items="items" :items-per-page="itemsPerPage" :loading="loadings.paginate" multi-sort outlined :server-items-length="serverItemsLength" show-select @contextmenu:row="oncontextmenu" @route="paginate">
          <template v-slot:item.name="{ item }">
            <nuxt-link :to="{ path: `/users/${item.id}`, query: $route.query }">
              {{ item.name }}
            </nuxt-link>
          </template>
          <template v-slot:item.profile.birthday="{ item }">
            {{ item.profile.birthday | formatDate('YYYY-MM-DD') }}
          </template>
          <template v-slot:item.profile.gender="{ item }">
            {{ item.profile.gender | labeled($enum.gender) }}
          </template>
        </app-data-table> -->
      </v-col>
    </v-row>
    <nuxt-child @routed="paginate" />
  </div>
</template>

<style lang="scss" scoped>
.v-menu__content {
  .v-list-item {
    font-size: 0.875rem;

    .v-list-item__icon {
      margin-right: 8px;

      .v-icon {
        height: 21px;
        margin: auto 0;
        width: 21px;
      }
    }
  }

  .v-divider {
    margin: 8px 0;
  }
}
</style>
