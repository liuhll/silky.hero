import { defineStore } from 'pinia';
import { store } from '/@/store';

import { getOrganizationTree } from '/@/api/organization';
import { GetOrgizationTreeModel } from '/@/api/organization/model/organizationModel';

export const useOrganizationStore = defineStore({
  id: 'app-organization',
  actions: {
    async getOrganizationTree(): Promise<GetOrgizationTreeModel[] | null> {
      return await getOrganizationTree();
    },
  },
});

// Need to be used outside the setup
export function useOrganizationStoreWithOut() {
  return useOrganizationStore(store);
}
