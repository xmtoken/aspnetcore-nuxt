import { ActionTree, MutationTree } from 'vuex';
import { RootState } from '~/store';

export type Notification = {
  key: string;
  color: string;
  type: string;
  content: string;
  isHtml: boolean;
  timeout: number;
};

export const state = () => ({
  values: [] as Notification[],
});

export type NotificationState = ReturnType<typeof state>;

export const mutations: MutationTree<NotificationState> = {
  add(state, notification: Notification): void {
    if (state.values.every(x => x.key !== notification.key)) {
      state.values.push(notification);
    }
  },
  clear(state): void {
    state.values = [];
  },
  remove(state, key: string): void {
    const index = state.values.findIndex(x => x.key === key);
    if (index >= 0) {
      state.values.splice(index, 1);
    }
  },
};

export const actions: ActionTree<NotificationState, RootState> = {
  add({ commit }, notification: Notification): void {
    const key = notification.key ?? Date.now() + Math.random();
    commit('add', { ...notification, key });
    if (notification.timeout !== -1) {
      setTimeout(() => commit('remove', key), notification.timeout ?? 5000);
    }
  },
  'add-success'({ dispatch }, notification: Notification): void {
    dispatch('add', { ...notification, type: 'success' });
  },
  'add-info'({ dispatch }, notification: Notification): void {
    dispatch('add', { ...notification, type: 'info' });
  },
  'add-warning'({ dispatch }, notification: Notification): void {
    dispatch('add', { ...notification, type: 'warning' });
  },
  'add-error'({ dispatch }, notification: Notification): void {
    dispatch('add', { ...notification, type: 'error' });
  },
};
