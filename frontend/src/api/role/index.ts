import { defHttp } from '/@/utils/http/axios';
import { BasicFetchResult } from '/@/api/model/baseModel';
import { GetRolePageModel } from './model/roleModel';
import { requestParams } from '../../../mock/_util';

enum Api {
  GetRolePageList = '/role/page',
  Role = '/role',
}

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

// export const setRoleStatus = (id: number, status: Status) => {
//   return defHttp.put({
//     url: `/role/status/${id}/${status}`,
//   });
// };
