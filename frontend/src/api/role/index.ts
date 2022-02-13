import { defHttp } from '/@/utils/http/axios';
import { BasicFetchResult } from '/@/api/model/baseModel';
import { GetRoleModel, GetRolePageModel } from './model/roleModel';

enum Api {
  GetRolePageList = '/role/page',
  Role = '/role',
}

export const getRoleById = (id) => {
  return defHttp.get<GetRoleModel>({
    url: `/role/${id}`,
  });
};

export const getRolePageList = (requestParams) => {
  return defHttp.get<BasicFetchResult<GetRolePageModel>>({
    url: Api.GetRolePageList,
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

export const getRoleMenuIds = (id: number) => {
  return defHttp.get({
    url: `/role/menus/${id}`,
  });
};

export const updateRoleMenuIds = (requestParams) => {
  return defHttp.put({
    url: `/role/menus`,
    params: requestParams,
  });
};

export const getRoleDataRange = (id: number) => {
  return defHttp.get({
    url: `/role/datarange/${id}`,
  });
};

export const updateRoleDataRange = (requestParams) => {
  return defHttp.put({
    url: `/role/datarange`,
    params: requestParams,
  });
};

export const getRoleList = (requestParams) => {
  return defHttp.get({
    url: `/role/list`,
    params: requestParams,
  });
};

// export const setRoleStatus = (id: number, status: Status) => {
//   return defHttp.put({
//     url: `/role/status/${id}/${status}`,
//   });
// };
