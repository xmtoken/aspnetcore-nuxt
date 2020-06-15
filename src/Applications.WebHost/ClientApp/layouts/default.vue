<script>
import { AppVersioning } from '~/components';
import {
  //
  mdiAccountCircleOutline,
  mdiHomeCircleOutline,
  mdiMicrosoftVisualStudio,
} from '@mdi/js';
export default {
  components: {
    AppVersioning,
  },
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
    for (const lv1route of this.routes) {
      if (this.$route.path.startsWith(lv1route.path)) {
        return;
      }
      if (!lv1route.routes) {
        continue;
      }
      for (const lv2route of lv1route.routes) {
        if (this.$route.path.startsWith(lv2route.path)) {
          lv1route.expands = true;
          return;
        }
        if (!lv2route.routes) {
          continue;
        }
        for (const lv3route of lv2route.routes) {
          if (this.$route.path.startsWith(lv3route.path)) {
            lv1route.expands = true;
            lv2route.expands = true;
            return;
          }
        }
      }
    }
  },
  methods: {
    onResize() {
      const limit = window.innerHeight;
      const container = document.getElementById('container').clientHeight;
      const elements = Array.from(document.getElementsByClassName('fluid-100vh')).map(element => {
        if (element.classList.contains('v-data-table')) {
          return element.getElementsByClassName('v-data-table__wrapper')[0];
        }
        return element;
      });
      if (elements.length > 0) {
        const used = container - elements.map(x => x.clientHeight).reduce((x1, x2) => Math.max(x1, x2));
        for (const element of elements) {
          element.style.maxHeight = limit - used + 'px';
        }
      }
    },
  },
};
</script>

<template>
  <v-app class="noto-sans-jp">
    <v-navigation-drawer app dark fixed :mobile-break-point="0">
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
        <template v-for="lv1route in routes">
          <template v-if="lv1route.routes">
            <!-- eslint-disable-next-line vue/valid-v-for -->
            <v-list-group no-action :prepend-icon="lv1route.icon" :value="lv1route.expands">
              <template v-slot:activator>
                <v-list-item-title>{{ lv1route.text }}</v-list-item-title>
              </template>
              <template v-for="lv2route in lv1route.routes">
                <template v-if="lv2route.routes">
                  <!-- eslint-disable-next-line vue/valid-v-for -->
                  <v-list-group no-action sub-group :value="lv2route.expands">
                    <template v-slot:activator>
                      <v-list-item-title>{{ lv2route.text }}</v-list-item-title>
                    </template>
                    <template v-for="lv3route in lv2route.routes">
                      <!-- eslint-disable-next-line vue/valid-v-for -->
                      <v-list-item nuxt :to="lv3route.path">
                        <v-list-item-title>{{ lv3route.text }}</v-list-item-title>
                        <v-list-item-icon v-if="lv3route.icon">
                          <v-icon>{{ lv3route.icon }}</v-icon>
                        </v-list-item-icon>
                      </v-list-item>
                    </template>
                  </v-list-group>
                </template>
                <template v-else>
                  <!-- eslint-disable-next-line vue/valid-v-for -->
                  <v-list-item nuxt :to="lv2route.path">
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
            <!-- eslint-disable-next-line vue/valid-v-for -->
            <v-list-item nuxt :to="lv1route.path">
              <v-list-item-icon v-if="lv1route.icon">
                <v-icon>{{ lv1route.icon }}</v-icon>
              </v-list-item-icon>
              <v-list-item-title>{{ lv1route.text }}</v-list-item-title>
            </v-list-item>
          </template>
        </template>
      </v-list>
    </v-navigation-drawer>
    <v-content>
      <v-container id="container" fluid>
        <nuxt v-resize="onResize" />
      </v-container>
    </v-content>
    <app-versioning />
  </v-app>
</template>
