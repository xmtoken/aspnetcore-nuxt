<script>
import { Resizer } from '~/extensions';
import { mdiAccountCircleOutline, mdiHomeCircleOutline, mdiMicrosoftVisualStudio, mdiLayersOutline } from '@mdi/js';
export default {
  data() {
    return {
      icons: {
        mdiMicrosoftVisualStudio,
      },
      routes: [
        {
          text: 'Home',
          icon: mdiHomeCircleOutline,
          path: '/',
        },
        {
          text: 'User',
          icon: mdiAccountCircleOutline,
          path: '/users',
        },
      ],
    };
  },
  created() {
    const isRouteExpand = path => path.replace(/\/$/, '') === this.$route.path.replace(/\/$/, '');
    for (const lv1route of this.routes.filter(x => x.routes)) {
      for (const lv2route of lv1route.routes.filter(x => x.path)) {
        lv1route.expands = isRouteExpand(lv2route.path);
      }
      for (const lv2route of lv1route.routes.filter(x => x.routes)) {
        for (const lv3route of lv2route.routes.filter(x => x.path)) {
          lv1route.expands = isRouteExpand(lv3route.path);
          lv2route.expands = isRouteExpand(lv3route.path);
        }
      }
    }
  },
  methods: {
    onResize() {
      Resizer.resize();
    },
  },
};
</script>

<template>
  <v-app v-resize="onResize" class="noto-sans-jp">
    <v-navigation-drawer app dark fixed :mobile-breakpoint="0">
      <v-list-item>
        <v-list-item-avatar>
          <v-icon>
            {{ icons.mdiMicrosoftVisualStudio }}
          </v-icon>
        </v-list-item-avatar>
        <v-list-item-content>
          <v-list-item-title class="title">
            Nuxt.js
          </v-list-item-title>
          <v-list-item-subtitle>
            {{ $auth.user.userName }}
          </v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
      <v-divider />
      <v-list>
        <template v-for="(lv1route, lv1key) in routes">
          <template v-if="lv1route.routes">
            <v-list-group :key="lv1key" no-action :prepend-icon="lv1route.icon" :value="lv1route.expands">
              <template v-slot:activator>
                <v-list-item-title>{{ lv1route.text }}</v-list-item-title>
              </template>
              <template v-for="(lv2route, lv2key) in lv1route.routes">
                <template v-if="lv2route.routes">
                  <v-list-group :key="lv2key" no-action sub-group :value="lv2route.expands">
                    <template v-slot:activator>
                      <v-list-item-title>{{ lv2route.text }}</v-list-item-title>
                    </template>
                    <template v-for="(lv3route, lv3key) in lv2route.routes">
                      <v-list-item :key="lv3key" nuxt :to="lv3route.path">
                        <v-list-item-title>{{ lv3route.text }}</v-list-item-title>
                        <v-list-item-icon v-if="lv3route.icon">
                          <v-icon>{{ lv3route.icon }}</v-icon>
                        </v-list-item-icon>
                      </v-list-item>
                    </template>
                  </v-list-group>
                </template>
                <template v-else>
                  <v-list-item :key="lv2key" nuxt :to="lv2route.path">
                    <v-list-item-title>{{ lv2route.text }}</v-list-item-title>
                    <v-list-item-icon v-if="lv2route.icon">
                      <v-icon>{{ lv2route.icon }}</v-icon>
                    </v-list-item-icon>
                  </v-list-item>
                </template>
              </template>
            </v-list-group>
          </template>
          <template v-else>
            <v-list-item :key="lv1key" nuxt :to="lv1route.path">
              <v-list-item-icon v-if="lv1route.icon">
                <v-icon>{{ lv1route.icon }}</v-icon>
              </v-list-item-icon>
              <v-list-item-title>{{ lv1route.text }}</v-list-item-title>
            </v-list-item>
          </template>
        </template>
      </v-list>
    </v-navigation-drawer>
    <v-main>
      <v-container id="container" fluid>
        <nuxt />
      </v-container>
    </v-main>
    <app-notifications :notifications="$store.state.notifications.queue" />
    <app-versioning />
  </v-app>
</template>
