import { defHttp } from '/@/utils/http/axios';
import { BasicFetchResult } from '/@/api/model/baseModel';
import { GetUserPageModel, GetUserPositionModel } from './model/userModel';

enum Api {
  GetUserPageList = '/user/page',
  User = '/user',
}

export const getUserPageList = (requestParams) => {
  return defHttp.get<BasicFetchResult<GetUserPageModel>>({
    url: Api.GetUserPageList,
    params: requestParams,
  });
};

export const getOrganizationUserPage = (requestParams) => {
  return defHttp.get<GetUserPositionModel>({
    url: `user/${requestParams.id}/organizationuser/page`,
    params: {
      pageIndex: requestParams.pageIndex,
      pageSize: requestParams.pageSize,
      userName: requestParams.userName,
      realName: requestParams.realName,
    },
  });
};

export const createUser = (requestParams) => {
  return defHttp.post({
    url: Api.User,
    params: requestParams,
  });
};

export const updateUser = (requestParams) => {
  return defHttp.put({
    url: Api.User,
    params: requestParams,
  });
};

export const deleteUser = (id: number) => {
  return defHttp.delete({
    url: `/user/${id}`,
  });
};
