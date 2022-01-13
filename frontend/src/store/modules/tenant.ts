import { defineStore } from 'pinia';
import { GetTennatModel } from '/@/api/tenant/model/tenantModel';
import { getAllTenants } from '/@/api/tenant';
import { store } from '/@/store';

export const useTenantStore = defineStore({
  id: 'app-tenant',
  actions: {
    async getAllTenants(): Promise<GetTennatModel[] | null> { 
      const allTenants = getAllTenants();
      return allTenants;
    }
  }
});

export function useTenantStoreWithOut() {
  return useTenantStore(store);
}
