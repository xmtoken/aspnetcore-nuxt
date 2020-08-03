<script lang="ts">
import { mdiAccount, mdiBattlenet, mdiLock } from '@mdi/js';
import { AxiosResponse } from 'axios';
import HttpStatus from 'http-status-codes';
import { ValidationObserver } from 'vee-validate';
import Vue, { VueConstructor } from 'vue';
import AppAlertSnackbar from '~/components/AppAlertSnackbar.vue';
import mixins from '~/extensions/mixins';
import { Credentials } from '~/types/api';

const $refs = Vue as VueConstructor<
  Vue & {
    $refs: {
      observer: InstanceType<typeof ValidationObserver>;
      snackbar: InstanceType<typeof AppAlertSnackbar>;
    };
  }
>;

export default mixins($refs).extend({
  auth: 'guest',
  layout: 'none',
  components: {
    ValidationObserver,
  },
  data() {
    return {
      icons: {
        avatar: mdiBattlenet,
        userName: mdiAccount,
        password: mdiLock,
      },
      loadings: {
        signin: false,
      },
      credentials: {} as Credentials,
    };
  },
  methods: {
    async signin(): Promise<void> {
      try {
        this.loadings.signin = true;

        this.$refs.snackbar.close();
        if (!(await this.$refs.observer.validate())) {
          return;
        }

        const response: AxiosResponse = await this.$auth.loginWith('local', { data: this.credentials });
        switch (response.status) {
          case HttpStatus.OK:
            // if (window.PasswordCredential) {
            //   const credentials = new window.PasswordCredential({ id: this.credentials.userName, password: this.credentials.password });
            //   await navigator.credentials.store(credentials);
            // }
            await this.$auth.fetchUser();
            break;

          case HttpStatus.BAD_REQUEST:
            this.$refs.observer.setErrors(response.data.errors ?? {});
            this.$refs.snackbar.setErrors(response.data.errors, '');
            break;
        }
      } finally {
        this.loadings.signin = false;
      }
    },
  },
});
</script>

<template>
  <v-container class="my-5 py-5">
    <app-alert-snackbar ref="snackbar" :timeout="-1" top transition="slide-y-transition" type="error" />
    <v-row dense justify="center">
      <v-col cols="auto">
        <v-avatar class="darken-3 grey">
          <v-icon dark large>
            {{ icons.avatar }}
          </v-icon>
        </v-avatar>
      </v-col>
    </v-row>
    <v-row dense justify="center">
      <v-col cols="auto">
        <span class="title">Project Templates</span>
      </v-col>
    </v-row>
    <v-row dense justify="center">
      <v-col cols="auto">
        <validation-observer ref="observer" slim>
          <v-form autocomplete="off" @submit.prevent="signin">
            <v-card outlined width="360">
              <v-card-text>
                <app-username-field v-model="credentials.userName" autocomplete="username" :disabled="loadings.signin" disabled-required-marker label="ユーザー名" :prepend-inner-icon="icons.userName" vee-rules="required" vee-vid="userName" />
                <app-password-field v-model="credentials.password" autocomplete="current-password" :disabled="loadings.signin" disabled-required-marker label="パスワード" :prepend-inner-icon="icons.password" vee-rules="required" vee-vid="password" />
              </v-card-text>
              <v-card-actions class="pb-4 pt-0 px-4">
                <v-btn block color="primary" depressed :disabled="loadings.signin" :loading="loadings.signin" type="submit">
                  ログイン
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-form>
        </validation-observer>
      </v-col>
    </v-row>
  </v-container>
</template>
