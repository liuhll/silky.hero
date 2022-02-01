import { defHttp } from '/@/utils/http/axios';
import { BasicFetchResult } from '/@/api/model/baseModel';
import { GetRolePageModel } from './model/roleModel';

enum Api {
  GetUserPageList = '/role/page',
  Role = '/role',
}

export const getRolePageList = (requestParams) => {
  return defHttp.get<BasicFetchResult<GetRolePageModel>>({
    url: Api.GetUserPageList,
    params: requestParams,
  });
};

export const createRole = (requestParams) => {
  return defHttp.post({
    url: Api.Role,
    params: requestParams,
  });
};

export const updateRole = (requestParams) => {
  return defHttp.put({
    url: Api.Role,
    params: requestParams,
  });
};

export const deleteRole = (id: number) => {
  return defHttp.delete({
    url: `/role/${id}`,
  });
};
