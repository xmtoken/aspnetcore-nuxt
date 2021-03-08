import { Query } from '~/types/vue-router';

export function isRequiedReload(newQuery: Query, oldQuery: Query): boolean {
  const isRoutedByNavigationMenu = Object.keys(newQuery).length === 0;
  return isRoutedByNavigationMenu && Object.values(oldQuery).filter(x => x).length !== 0;
}
