export const state = () => ({
  queue: [],
});

export const mutations = {
  add(state, { key, color, type, content, isHtml }) {
    state.queue.push({ key, color, type, content, isHtml });
  },
  remove(state, key) {
    const index = state.queue.findIndex(x => x.key === key);
    if (index >= 0) {
      state.queue.splice(index, 1);
    }
  },
  clear(state) {
    state.queue = [];
  },
};

export const actions = {
  add({ commit }, { color, type, content, timeout, isHtml }) {
    const key = Date.now() + Math.random();
    commit('add', { key, color, type, content, timeout, isHtml });
    setTimeout(() => commit('remove', key), timeout ?? 5000);
  },
  'add-success'({ dispatch }, payload) {
    dispatch('add', { ...payload, type: 'success' });
  },
  'add-info'({ dispatch }, payload) {
    dispatch('add', { ...payload, type: 'info' });
  },
  'add-warning'({ dispatch }, payload) {
    dispatch('add', { ...payload, type: 'warning' });
  },
  'add-error'({ dispatch }, payload) {
    dispatch('add', { ...payload, type: 'error' });
  },
};
