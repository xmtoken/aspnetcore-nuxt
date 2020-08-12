<script lang="ts">
import { mdiAccountCircleOutline, mdiBattlenet, mdiCheckCircleOutline, mdiHomeCircleOutline, mdiMenu, mdiMenuOpen } from '@mdi/js';
import Vue from 'vue';

type NavigationRoute = {
  icon: string | null;
  text: string | null;
  path: string | null;
  expands: boolean | null;
  routes: NavigationRoute[] | null;
};

export default Vue.extend({
  inheritAttrs: false,
  data() {
    return {
      icons: {
        avatar: mdiBattlenet,
        menuOnOpen: mdiMenuOpen,
        menuOnClose: mdiMenu,
      },
      isNavigationDrawerMiniVariant: (localStorage.getItem('isNavigationDrawerMiniVariant') ?? false) === 'true',
      routes: [
        {
          text: 'Home',
          icon: mdiHomeCircleOutline,
          path: '/',
          routes: null,
        },
        {
          text: 'Task',
          icon: mdiCheckCircleOutline,
          path: '/tasks',
          routes: null,
        },
        {
          text: 'User',
          icon: mdiAccountCircleOutline,
          path: '/users',
          routes: null,
        },
        {
          text: 'Samples',
          icon: mdiAccountCircleOutline,
          routes: [
            {
              text: 'Components',
              icon: mdiAccountCircleOutline,
              path: '/samples/components',
            },
          ],
        },
      ] as NavigationRoute[],
    };
  },
  watch: {
    isNavigationDrawerMiniVariant(val: any /* typed as boolean, $route becomes any type by vetur or typescript bug. */): void {
      localStorage.setItem('isNavigationDrawerMiniVariant', String(val));
    },
  },
  created(): void {
    const removeTrailingSlash = (path: string) => path.replace(/\/$/, '');
    const isRouteExpands = (path: string) => removeTrailingSlash(path) === removeTrailingSlash(this.$route.path);
    for (const lv1route of this.routes) {
      if (lv1route.routes) {
        for (const lv2route of lv1route.routes) {
          if (lv2route.path) {
            lv1route.expands = isRouteExpands(lv2route.path);
          }
          if (lv2route.routes) {
            for (const lv3route of lv2route.routes) {
              if (lv3route.path) {
                lv1route.expands = isRouteExpands(lv3route.path);
                lv2route.expands = isRouteExpands(lv3route.path);
              }
            }
          }
        }
      }
    }
  },
});
</script>

<template>
  <v-navigation-drawer app dark fixed :mini-variant="isNavigationDrawerMiniVariant" :mobile-breakpoint="0">
    <v-list-item :class="{ 'px-0': isNavigationDrawerMiniVariant }">
      <v-list-item-avatar v-if="!isNavigationDrawerMiniVariant">
        <v-icon>
          {{ icons.avatar }}
        </v-icon>
      </v-list-item-avatar>
      <v-list-item-content v-if="!isNavigationDrawerMiniVariant">
        <v-list-item-title class="title">
          <span>Nuxt.js</span>
        </v-list-item-title>
      </v-list-item-content>
      <v-list-item-icon class="mx-auto">
        <v-btn icon @click="isNavigationDrawerMiniVariant = !isNavigationDrawerMiniVariant">
          <v-icon>
            {{ isNavigationDrawerMiniVariant ? icons.menuOnClose : icons.menuOnOpen }}
          </v-icon>
        </v-btn>
      </v-list-item-icon>
    </v-list-item>
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
</template>
