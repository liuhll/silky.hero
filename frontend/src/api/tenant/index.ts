import { defHttp } from '/@/utils/http/axios';

import { GetTenantModel, GetTenantPageModel } from './model/tenantModel';
import { BasicFetchResult } from '../model/baseModel';

enum Api {
  getAllTenant = '/tenant/all',
  GetTenantList = '/tenant/list',
  GetTenantPage = '/tenant/page',
  Tenant = '/tenant',
}

export function getAllTenants() {
  return defHttp.get<GetTenantModel[]>({ url: Api.getAllTenant }, { errorMessageMode: 'none' });
}

export const getTenantList = (query: any) => {
  return defHttp.get<GetTenantModel[]>({
    url: Api.GetTenantList,
    params: { name: query.name },
  });
};

export const getTenantPageList = (requestParams: any) => {
  return defHttp.get<BasicFetchResult<GetTenantPageModel>>({
    url: Api.GetTenantPage,
    params: requestParams,
  });
};

export const createTenant = (requestParams: any) => {
  return defHttp.post({ url: Api.Tenant, params: requestParams });
};

export const updateTenant = (requestParams: any) => {
  return defHttp.put({ url: Api.Tenant, params: requestParams });
};

export const deleteTenant = (id: number) => {
  return defHttp.delete({ url: `/tenant/${id}` });
};

export const getTenantById = (id: number) => {
  return defHttp.get({ url: `/tenant/${id}` });
};
