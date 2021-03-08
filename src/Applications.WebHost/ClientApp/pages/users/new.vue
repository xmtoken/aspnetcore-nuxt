<script>
import { mdiCalendar } from '@mdi/js';
import { StatusCodes } from 'http-status-codes';
import { ValidationObserver } from 'vee-validate';

export default {
  components: {
    ValidationObserver,
  },
  data() {
    return {
      icons: {
        mdiCalendar,
      },
      loading: {
        create: false,
      },
      items: {
        roles: undefined,
      },
      entity: {
        userName: null,
        password: null,
        passwordConfirmation: null,
        emailAddress: null,
        birthday: null,
        gender: null,
        roles: null,
      },
    };
  },
  created() {
    this.$axios.$get('/roles').then(data => {
      this.items.roles = data;
    });
  },
  methods: {
    onDialogClose() {
      this.$router.push({ path: '/features/crud', query: this.$route.query });
    },
    async create() {
      try {
        this.loading.create = true;

        this.$refs.snackbar.reset();
        if (!(await this.$refs.observer.validate())) {
          this.$refs.snackbar.setHasInputErrorsMessage();
          return;
        }

        const response = await this.$axios.post('/users', this.entity);
        if (response.status === StatusCodes.BAD_REQUEST) {
          this.$refs.observer.setErrors(response.data.errors);
          this.$refs.snackbar.setValidationErrors(response.data.errors);
          this.$refs.snackbar.setHasInputErrorsMessage();
          return;
        }

        await this.$router.push({ path: `/features/crud/${response.data.id}`, query: this.$route.query });
        this.$emit('routed', this.$route.query);
      } finally {
        this.loading.create = false;
      }
    },
  },
};
</script>

<template>
  <app-dialog max-width="420" right scrollable :value="true" @input="onDialogClose">
    <validation-observer ref="observer" slim>
      <v-form autocomplete="off" @submit.prevent>
        <app-alert-snackbar ref="snackbar" top />
        <v-card>
          <v-card-text>
            <app-username-field v-model="entity.userName" label="ユーザー名" vee-rules="required|username" vee-vid="UserName" />
            <app-password-field v-model="entity.password" label="パスワード" vee-rules="required|password" vee-vid="Password" />
            <app-password-field v-model="entity.passwordConfirmation" label="パスワード(確認)" vee-rules="required|confirmed:Password" />
            <app-text-field v-model="entity.emailAddress" label="メールアドレス" vee-rules="required|email" vee-vid="EmailAddress" />
            <app-birthday-field v-model="entity.birthday" :append-icon="icons.mdiCalendar" label="生年月日" menu-offset-left vee-rules="required|date|birthday" vee-vid="Birthday" />
            <app-select v-model="entity.gender" label="性別" vee-rules="required" vee-vid="Gender" />
            <app-select v-model="entity.roles" :items="items.roles" label="ロール" multiple vee-rules="required" vee-vid="Roles" />
            <v-btn block class="mt-3" color="primary" depressed :disabled="loading.create" :loading="loading.create" @click="create">
              Create
            </v-btn>
          </v-card-text>
        </v-card>
      </v-form>
    </validation-observer>
  </app-dialog>
</template>
