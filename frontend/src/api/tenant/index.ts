import { defHttp } from '/@/utils/http/axios';

import { GetTennatModel } from './model/tenantModel'

enum Api {
  getAllTenant = '/api/tenant/all',
}

export function getAllTenants() {
  return defHttp.get<GetTennatModel[]>({ url: Api.getAllTenant }, { errorMessageMode: 'none' });
}
