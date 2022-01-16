import { defHttp } from '/@/utils/http/axios';
import { GetOrgizationTreeModel } from './model/organizationModel';

enum Api {
  GetOrganizationTree = '/organization/tree',
}

export const getOrganizationTree = () => {
  return defHttp.get<GetOrgizationTreeModel[]>({ url: Api.GetOrganizationTree });
}
