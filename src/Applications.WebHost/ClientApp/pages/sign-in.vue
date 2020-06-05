<script>
import { AppAlertSnackbar, AppPasswordField, AppUsernameField } from '~/components';
import { mdiAccount, mdiLock, mdiMicrosoftVisualStudio } from '@mdi/js';
import HttpStatus from 'http-status-codes';
import { ValidationObserver } from 'vee-validate';
export default {
  layout: 'none',
  auth: 'guest',
  components: {
    AppAlertSnackbar,
    AppPasswordField,
    AppUsernameField,
    ValidationObserver,
  },
  data() {
    return {
      icons: {
        mdiAccount,
        mdiLock,
        mdiMicrosoftVisualStudio,
      },
      loading: {
        signin: false,
      },
      credentials: {
        userName: null,
        password: null,
      },
    };
  },
  methods: {
    async signin() {
      try {
        this.loading.signin = true;

        this.$refs.snackbar.reset();
        if (!(await this.$refs.observer.validate())) {
          return;
        }

        const response = await this.$auth.loginWith('local', { data: this.credentials });
        if (response.status === HttpStatus.BAD_REQUEST) {
          this.$refs.observer.setErrors(response.data.errors);
          this.$refs.snackbar.setValidationErrors(response.data.errors);
          return;
        }

        if (window.PasswordCredential) {
          const credentials = new window.PasswordCredential({ id: this.credentials.userName, password: this.credentials.password });
          await navigator.credentials.store(credentials);
        }
        await this.$auth.fetchUser();
      } finally {
        this.loading.signin = false;
      }
    },
  },
};
</script>

<template>
  <v-content>
    <v-container class="align-center d-flex flex-column my-5 py-5" fluid>
      <app-alert-snackbar ref="snackbar" top />
      <v-avatar class="darken-3 grey">
        <v-icon dark large>
          {{ icons.mdiMicrosoftVisualStudio }}
        </v-icon>
      </v-avatar>
      <span class="my-3 title">Project Templates</span>
      <validation-observer ref="observer">
        <v-form autocomplete="off" @submit.prevent="signin">
          <v-card outlined width="360">
            <v-card-text>
              <app-username-field v-model="credentials.userName" :disabled="loading.signin" hide-required-marker label="ユーザー名" :prepend-inner-icon="icons.mdiAccount" vee-rules="required" vee-vid="UserName" />
              <app-password-field v-model="credentials.password" :disabled="loading.signin" hide-required-marker label="パスワード" :prepend-inner-icon="icons.mdiLock" vee-rules="required" vee-vid="Password" />
            </v-card-text>
            <v-card-actions class="pb-4 pt-0 px-4">
              <v-btn block color="primary" depressed :disabled="loading.signin" :loading="loading.signin" type="submit">
                ログイン
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-form>
      </validation-observer>
    </v-container>
  </v-content>
</template>
