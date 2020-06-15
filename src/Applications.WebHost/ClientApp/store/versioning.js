export const state = () => ({
  updatable: false,
});

export const mutations = {
  updatable(state, value) {
    state.updatable = value;
  },
};
