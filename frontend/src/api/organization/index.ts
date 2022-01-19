import { defHttp } from '/@/utils/http/axios';
import { GetOrgizationTreeModel, GetOrgizationUserModel } from './model/organizationModel';
import { BasicPageParams, BasicFetchResult } from '/@/api/model/baseModel';

enum Api {
  GetOrganizationTree = '/organization/tree',
}

export const getOrganizationTree = () => {
  return defHttp.get<GetOrgizationTreeModel[]>({ url: Api.GetOrganizationTree });
}

export const getOrganizationUserPageList = (requestParams) => {
  return defHttp.get<BasicFetchResult<GetOrgizationTreeModel>>({
    url: `/organization/${requestParams.id}/user/page`,
    params: {
      pageIndex: requestParams.pageIndex,
      pageSize: requestParams.pageSize
    }
  });
}
