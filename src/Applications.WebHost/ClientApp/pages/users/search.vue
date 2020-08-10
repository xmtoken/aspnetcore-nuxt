<script>
export default {
  data() {
    return {
      conditions: {
        name: this.$route.query.name,
        email: this.$route.query.email,
        birthday: this.$route.query.birthday,
        gender: Array.create(this.$route.query.gender).ofNumbers(),
        role: Array.create(this.$route.query.role).ofNumbers(),
      },
      items: {
        roles: [],
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
      this.$router.push({ path: '/users', query: this.$route.query });
    },
    search() {
      const query = {
        name: this.conditions.name || undefined,
        email: this.conditions.email || undefined,
        birthday: this.conditions.birthday || undefined,
        gender: this.conditions.gender || undefined,
        role: this.conditions.role || undefined,
      };
      this.$router.push({ path: '/users', query });
      this.$emit('routed', query);
    },
  },
};
</script>

<template>
  <app-dialog max-width="420" right scrollable :value="true" @input="onDialogClose">
    <v-form ref="form" autocomplete="off" @submit.prevent="search">
      <v-card>
        <v-card-actions>
          <v-btn color="primary" depressed type="submit" width="100">
            Search
          </v-btn>
          <v-btn color="primary" depressed outlined width="100" @click="$refs.form.reset()">
            Clear
          </v-btn>
        </v-card-actions>
        <v-divider />
        <v-card-text>
          <app-text-field v-model.trim="conditions.name" label="ユーザー名" />
          <app-text-field v-model.trim="conditions.email" label="メールアドレス" />
          <app-date-field v-model.trim="conditions.birthday" label="生年月日" menu-offset-left />
          <app-select v-model="conditions.gender" clearable :items="$enum.gender" label="性別" multiple />
          <app-select v-model="conditions.role" clearable :items="items.roles" label="ロール" multiple />
        </v-card-text>
      </v-card>
    </v-form>
  </app-dialog>
</template>
