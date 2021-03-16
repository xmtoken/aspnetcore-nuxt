import { MutationTree } from 'vuex';

export const state = () => ({
  updatable: false,
});

export type VersioningState = ReturnType<typeof state>;

export const mutations: MutationTree<VersioningState> = {
  updatablex(state, val: boolean): void {
    state.updatable = val;
  },
};
